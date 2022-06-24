using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_refill : MonoBehaviour
{

    [SerializeField]
    private Player_script _player;

    // energy refill to 100% if player is in start zone
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
