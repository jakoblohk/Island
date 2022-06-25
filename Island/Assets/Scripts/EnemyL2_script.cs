using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyL2_script : MonoBehaviour
{
    [SerializeField]
    private Player_script _player;

    [SerializeField]
    private Bullet_script _bullet;

    [SerializeField]
    private float _balloonSpeed = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // MOVEMENT
        transform.Translate(Vector3.up * _balloonSpeed * Time.deltaTime);

        // TELEPORT BACK TO START
        if (transform.position.y > 30)
        {
            transform.position = new Vector3(Random.Range(-20f, 20f), -15f, Random.Range(-20f, 20f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            _player.Damage();
        }
        

        if (other.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
