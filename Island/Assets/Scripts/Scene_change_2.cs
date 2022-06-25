using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change_2 : MonoBehaviour
{
    [SerializeField]
    private Player_script _player;

    // level switsch to EndScreen
    private void OnTriggerEnter(Collider other)
    {
        // if in goalstate and meet coin condition, advance to EndScreen
        if (other.CompareTag("Player") && (_player.coins == 20))
        {   
            // change level
            SceneManager.LoadScene(3);
        }
    }
}
