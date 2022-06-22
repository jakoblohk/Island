using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Behavior : MonoBehaviour
{
    [SerializeField]
    public int rotateSpeed = 5;

    [SerializeField]
    private Player_script _player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin collision");
            Destroy(this.gameObject);
            // coins++ in player
            _player.Coins();
        }

    }
}
