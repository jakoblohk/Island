using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_script : MonoBehaviour
{
    // determine bulletlifetime 
    private float _bulletLifetime = 3f;

    // when instantiated, destroy after time
    private void Awake()
    {
        Destroy(gameObject, _bulletLifetime);
    }
}
