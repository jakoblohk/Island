using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{
    [SerializeField]
    private Player_script _player;

    // level switsch to Level-2
    private void OnTriggerEnter(Collider other)
    {
        // if in goalstate and meet coin condition, advance to level 2 and reset coin counter
        if (other.CompareTag("Player") && (_player.coins == 15))
        {
            SceneManager.LoadScene(1);
            _player.coins = 0;
            _player.context = "";
        }
    }
}
