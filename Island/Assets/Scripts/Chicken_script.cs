using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_script : MonoBehaviour
{
    private float _moveSpeedChicken = 0.1f;
    private float _rotationSpeedChicken = 100f;

    private bool _isWandering = false;
    private bool _isRotatingLeft = false;
    private bool _isRotatingRight = false;
    private bool _isWalking = false;

    [SerializeField]
    private Rigidbody RB_Chicken;

    [SerializeField]
    private Player_script _player;

    [SerializeField]
    private Bullet_script _bullet;

    private void Start()
    {

    }

    private void Update()
    {
        if (_isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (_isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * _rotationSpeedChicken);
        }
        if (_isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -_rotationSpeedChicken);
        }
        if (_isWalking == true)
        {
            RB_Chicken.transform.position += transform.forward * _moveSpeedChicken;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            _player.Damage();
        }
        /*
        if (other.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }*/
    }

    IEnumerator Wander()
    {
        int rottime = Random.Range(1, 3);
        int rotwait = Random.Range(1, 3);
        int rotatelorR = Random.Range(1, 3);
        int walkwait = Random.Range(1, 3);
        int walktime = Random.Range(1, 3);


        _isWandering = true;

        yield return new WaitForSeconds(walkwait);
        _isWalking = true;
        yield return new WaitForSeconds(walktime);
        _isWalking = false;
        yield return new WaitForSeconds(rotwait);
        if (rotatelorR == 1)
        {
            _isRotatingRight = true;
            yield return new WaitForSeconds(rottime);
            _isRotatingRight = false;
        }
        if (rotatelorR == 2)
        {
            _isRotatingLeft = true;
            yield return new WaitForSeconds(rottime);
            _isRotatingLeft = false;
        }
        _isWandering = false;
    }
}
