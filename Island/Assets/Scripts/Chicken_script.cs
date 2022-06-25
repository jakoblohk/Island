using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_script : MonoBehaviour
{
    // movement variables
    private float _moveSpeedChicken = 0.1f;
    private float _rotationSpeedChicken = 100f;

    // boolean variables for direction and movement
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

    /*
     * Update()
     * decides on boolean condittions where to rotate and move.
     */
    private void Update()
    {
        // if not wandering, start Wander coroutine
        if (_isWandering == false)
        {
            StartCoroutine(Wander());
        }
        // rotate right 
        if (_isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * _rotationSpeedChicken);
        }
        // rotate left
        if (_isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -_rotationSpeedChicken);
        }
        // walk
        if (_isWalking == true)
        {
            RB_Chicken.transform.position += transform.forward * _moveSpeedChicken;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if colliding with Player, decrease health of player
        if (other.CompareTag("Player"))
        {
            _player.Damage();
        }
    }

    /*
     * Wander()
     * Scripts random movement for chicken.
     */
    IEnumerator Wander()
    {
        // set random integer for time of rotation and walking
        int rottime = Random.Range(1, 3);
        int rotwait = Random.Range(1, 3);
        int rotatelorR = Random.Range(1, 3);
        int walkwait = Random.Range(1, 3);
        int walktime = Random.Range(1, 3);

        // wait, then walk, then rotate, repeat
        _isWandering = true;
        yield return new WaitForSeconds(walkwait);
        _isWalking = true;
        yield return new WaitForSeconds(walktime);
        _isWalking = false;
        yield return new WaitForSeconds(rotwait);

        // rotation cases from random integer above
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
