using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Player_script : MonoBehaviour
{
    // TEXT fields for UI
    [SerializeField]
    private TextMeshProUGUI energyText;

    [SerializeField]
    private TextMeshProUGUI coinText;

    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private TextMeshProUGUI contextText;

    private string _context;

    // VARIABLES for coins, health, energy ...
    private int _health = 100;
    public float energy = 100f;
    public int coins = 0;
    // how much energy per time unit is lost
    private float _energyDecreaseTimeOffset = 0.5f;

    [SerializeField]
    public float _moveSpeed = 2f;

    [SerializeField]
    public float _turnSpeed = 25f;

    [SerializeField]
    private Rigidbody RB;

    [SerializeField]
    private float _jumpingSpeed = 1f;

    // time delay while jumping
    private float _nextJumpTime = 0f;
    private float _coolDownTime = 1f;


    [SerializeField]
    private GameObject _bulletPrefab;

    // cool down for bullet firing
    private float _firingRate = 0f;
    private float _fireCoolDownTime = 0.5f;

    private float _timer = 0;

    // animation for walking
    //private Animator animator;


    void Start()
    {
        _context = "Hello SUPER CAT! \nToday your mission is to eat vegan burgers, dodge chicken and collect coins!";
  
        // sets first position
        transform.position = new Vector3(-4.876f, 0.512f, -0.959f);

        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer++;
        Death();
        UserInterface();
        PlayerMovement();
        Spawning();

    }

    // additional methods ----------------------------------------------------

    public void UserInterface()
    {
        if (_timer == 600)
        {
            _context = "";
        }
            // Energy level decrease over time
            if (energy > 0)
        {
            energy -= _energyDecreaseTimeOffset * Time.deltaTime;
        }
        energyText.SetText("Energy: " + Math.Round(energy) + "%");

        // Health Bar
        healthText.SetText("HP: " + _health);
        // Coins    
        coinText.SetText("Coins: " + coins + "/15");
        // Information
        contextText.SetText(_context);
        
    }
    // Death condition, either no hp or no energy
    void Death()
    {
        if (_health <= 0 || energy <= 0)
        {
            Destroy(this.gameObject);
            // Tell User he died
            _context = "Oh no, you failed my expectations. What a disappointment. Your parents warned me";
        }
    }
    // Coin counter
    public int Coins()
    {
        coins++;

        return coins;
    }

    public void Damage()
    {
        _health = _health - 7;
    }

    //  player movement 
    void PlayerMovement()
    {
        // horizontal player movement: rotates the player on the y-axis
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0)
        {
            transform.Rotate(0, horizontal * _turnSpeed, 0);
        }
        else if (horizontal < 0)
        {
            transform.Rotate(0, horizontal, 0);
        }

        // vertical player movement: moves player forward or back
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }
        else if (vertical < 0)
        {
            transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
        }


        // jumping when space is pressed with a certain time delay berween each jump
        if (Input.GetKeyDown("space") && _nextJumpTime < Time.time)
        {
            Debug.Log("jump");
            RB.velocity += new Vector3(0f, _jumpingSpeed, 0f);
            _nextJumpTime = Time.time + _coolDownTime;
        }

        // TELEPORT - if player is falling teleport him back to a certain position
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(-4.876f, 0.512f, -0.959f);
        }

        // create movementDirection to check if player isMoving
        //Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        /*if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            Debug.Log("not moving");
            animator.SetBool("isMoving", false);
        }*/
    }

    // // Bullet spwaning
    public void Spawning()
    {
        if (Input.GetKeyDown(KeyCode.E) && _firingRate < Time.time)
        {
            Instantiate(_bulletPrefab, transform.position + new Vector3(0f, 0.7f, 0f), Quaternion.identity);
            _firingRate = Time.time + _fireCoolDownTime;
        }
    }

}
