using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Energy_refill
 * 
 * The object to which this script is attached can refuel the players energy by 20% at a time.
 */
public class Energy_refill : MonoBehaviour
{
    // get Player 
    [SerializeField]
    private Player_script _player;

    // when colliding with player, set energy in player +20 and destroy object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.energy += 20f;
            Destroy(this.gameObject);
        }

        if (_player.energy > 100) {

            _player.energy = 100f;
        }
    }
}
