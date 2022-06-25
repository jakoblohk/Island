using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyL2_script : MonoBehaviour
{
    // get player instance
    [SerializeField]
    private Player_script _player;

    // set ballonSpeed
    [SerializeField]
    private float _balloonSpeed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        // MOVEMENT
        transform.Translate(Vector3.up * _balloonSpeed * Time.deltaTime);

        // TELEPORT BACK TO START
        if (transform.position.y > 50)
        {
            transform.position = new Vector3(Random.Range(-20f, 20f), -15f, Random.Range(-20f, 20f));
        }
    }
}
