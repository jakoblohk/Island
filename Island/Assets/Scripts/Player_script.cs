using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour
{

    [SerializeField]
    public float _moveSpeed = 1f;

    [SerializeField]
    public float _turnSpeed = 15f;

    [SerializeField]
    private Rigidbody RB;

    private float _jumpingSpeed = 10f;


    // animation for walking
    private Animator animator;


    void Start() {

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        

        // horizontal player movement: rotates the player on the y-axis
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0)
        {
            //transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
            transform.Rotate(0, horizontal * _turnSpeed, 0);
        }

        if (horizontal < 0)
        {
            //transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
            transform.Rotate(0, horizontal, 0);
        }


        // vertical player movement: moves player forward or back
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }

        if (vertical < 0)
        {
            transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
        }

        // create movementDirection to check if player isMoving
        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving",false);
        }

        // player jumps
        if (Input.GetKeyDown("space"))
        {
            RB.velocity += new Vector3(0f, _jumpingSpeed, 0f);
        }

    }

}
