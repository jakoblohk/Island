using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_script : MonoBehaviour
{
    private float _bulletLifetime = 3f;

    private void Awake()
    {
        Destroy(gameObject, _bulletLifetime);
    }
}
