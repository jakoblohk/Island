using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_script : MonoBehaviour
{
    // speed, how  fast the bullets fly 
    private float _bulletSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Shooting the bullet
        transform.Translate(Vector3.up *_bulletSpeed * Time.deltaTime);

        // destroy condition, that the bullets get destoied if a certain height is reached 
        if(transform.position.y > 20f)
        {
            Destroy(this.gameObject);
        }
        
    }
}
