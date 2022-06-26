using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

public class Player_script : MonoBehaviour
{
    // TEXT fields for UI
    // Energylevel
    [SerializeField]
    private TextMeshProUGUI energyText;
    // number of coins
    [SerializeField]
    private TextMeshProUGUI coinText;
    // hp bar
    [SerializeField]
    private TextMeshProUGUI healthText;
    // context sensitive player information
    [SerializeField]
    private TextMeshProUGUI contextText;
    // string variable to change according to context
    public string context;

    // VARIABLES for coins, health, energy
    private int _health = 100;
    public float energy = 100f;
    public int coins = 0;
    // how much energy per time unit is lost
    private float _energyDecreaseTimeOffset = 0.5f;

    // PLAYER MOVEMENT
    // variables for player movement
    [SerializeField]
    public float _moveSpeed = 2f;

    [SerializeField]
    public float _turnSpeed = 70f;

    [SerializeField]
    private float _jumpingSpeed = 1f;

    // time delay while jumping
    private float _nextJumpTime = 0f;
    private float _coolDownTime = 1f;

    // rigid body of the player
    [SerializeField]
    private Rigidbody RB;
    
    // FURBALL aka BULLET
    // spawnpoint for Furballs
    public Transform bulletSpawnPoint;
    [SerializeField]
    private GameObject _bulletPrefab;
    // speed, how  fast the furballs fly 
    private float _bulletSpeed = 5f;

    // cool down for furball firing
    private float _firingRate = 0f;
    private float _fireCoolDownTime = 0.5f;

    private float _timer = 0;
    private float _timerCoins = 0;

    // animation for walking
    private Animator animator;

    // store scene to check where player is 
    Scene scene;

    // Instantiate Player
    void Start()
    {
        // set scene
        scene = SceneManager.GetActiveScene();

        // check for which level to give out appropriate information
        if (scene.name == "Level-1")
        {
            context = "Hello SUPER CAT! \nToday your mission is to eat vegan burgers, dodge chicken and collect coins!";
        } else if (scene.name == "Level-2")
        {
            context = "Glad you made it! Now climb as far as you can! \n HINT: If you don't reach the coins, use 'E' to shoot them with a FURBALL";
        }
        // sets first position
        transform.position = new Vector3(-4.876f, 0.512f, -0.959f);

        // instantiate animator
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        // increase timer for UI decay
        _timer++;

        // call methods
        Death();
        UserInterface();
        PlayerMovement();
        PlayerAnimation();
        Spawning();

    }

    // additional methods ----------------------------------------------------


    /* 
     * UserInterface()
     * Creates and updates the UserInterface (UI) to show how much energy is left,
     * how many coins have been collected and how many health points are left. 
     */
    public void UserInterface()
    {   
        // after 600 frames empty context
        if (_timer == 550)
        {
            context = "";
        }
        // Set Context Information
        contextText.SetText(context);

        // if the player has 15 coins in Level 1 (or 20 coins in Level 2) tell them to find the red star (300 frames)
        if (coins == 15 && scene.name == "Level-1")
        {
            _timerCoins++;
            if (_timerCoins <= 300)
            {
                contextText.SetText("Well done, you have 15 coins! Now find the red star!");
            } else if(_timerCoins > 300)
            {
                context = "";
            }
        } else if (coins == 20 && scene.name == "Level-2")
        {
            _timerCoins++;
            if (_timerCoins <= 300)
            {
                contextText.SetText("Well done, you have 20 coins! Now find the red star!");
            } else if (_timerCoins > 300)
            {
                context = "";
            }
        }


        // Energy level decrease over time
        if (energy > 0)
        {
            energy -= _energyDecreaseTimeOffset * Time.deltaTime;
        }
        // Set energy text
        energyText.SetText("Energy: " + Math.Round(energy) + "%");

        // Set Health Bar text
        healthText.SetText("HP: " + _health);

        // Set Coins text, for each level respectively
        if (scene.name == "Level-1")
        {
            coinText.SetText("Coins: " + coins + "/15");
        }
        else
        {
            coinText.SetText("Coins: " + coins + "/20");
        }        
    }

    /* 
     * Death()
     * If the player runs out of energy or HP the game is over.
     */
    void Death()
    {
        if (_health <= 0 || energy <= 0)
        {
            Destroy(this.gameObject);
            // Tell User they died
            // context = "GAME OVER";
            SceneManager.LoadScene(4);
        }
    }

    /*
     * Coins()
     * Counts the collected coins, and if called returns current number of coins.
     */
    public int Coins()
    {
        coins++;
        return coins;
    }

    /*
     * Damage()
     * If the player takes damage from chicken, decrease HP by 7.
     */
    public void Damage()
    {
        _health = _health - 7;
    }

    /*
     * PlayerMovement()
     * The player can move forward and backwards, rotate to change direction (w,a,s,d, or arrow keys)
     * and jump with 'spacebar'. If the player is falling, e.g. level 2, return to start position and substract 11 hp.
     */
    void PlayerMovement()
    {
        //  ROTATION - horizontal player movement: rotates the player on the y-axis
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0)
        {
            transform.Rotate(0, horizontal * _turnSpeed, 0);
        }
        else if (horizontal < 0)
        {
            transform.Rotate(0, horizontal, 0);
        }

        // MOVEMENT - vertical player movement: moves player forward or back
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }
        else if (vertical < 0)
        {
            transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
        }

        // JUMPING - when space is pressed with a certain time delay berween each jump
        if (Input.GetKeyDown("space") && _nextJumpTime < Time.time)
        {
            RB.velocity += new Vector3(0f, _jumpingSpeed, 0f);
            _nextJumpTime = Time.time + _coolDownTime;
        }

        // TELEPORT - if player is falling teleport them back to a certain position and decrease HP by 11
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(-4.876f, 0.512f, -0.959f);
            _health = _health - 11;

        }
    }

    /*
     * Spawning()
     * Spawns a Furball that shoots (pressing E) in the direction of where the cat is facing. It has a limited firing rate.
     * 
     */
    public void Spawning()
    {
        // if E is pressed and firing rate is over, shoot another bullet
        if (Input.GetKeyDown(KeyCode.E) && _firingRate < Time.time)
        {
            var bullet = Instantiate(_bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * _bulletSpeed;

            _firingRate = Time.time + _fireCoolDownTime;
        }
    }

    /*
     * PlayerAnimation()
     * Sets a boolean for isMoving or isMovingBackwards to either true or false, 
     * determined by which key is pressed, as well as if the player is standing still
     */
    void PlayerAnimation()
    {
        // 
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            Debug.Log("not moving");
            animator.SetBool("isMoving", false);
        }

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            animator.SetBool("isMovingBackwards", true);
        }
        else
        {
            Debug.Log("not moving");
            animator.SetBool("isMovingBackwards", false);
        }
    }

}
