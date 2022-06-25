using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Behavior : MonoBehaviour
{
    // rotation speed of coins
    [SerializeField]
    public int rotateSpeed = 5;
    
    // get player instance 
    [SerializeField]
    private Player_script _player;

    // Update is called once per frame
    void Update()
    {
        // make coin rotate when floating
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

    /*
     * OnTriggerEnter(Collider other)
     * other - object that is colliding with Coin
     * 
     * If colliding with Player or Bullet, destroy coin and (in case) bullet as well.
     */
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            // call Setter for coins in Player_Script and increase them
            _player.Coins();
        }

        if (other.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            _player.Coins();
        }

    }
}
