using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed;

    private Animator animator;
    private Rigidbody2D _rb;

    private bool _playerMoving;
    public Vector2 lastMove;
    private static bool _playerExists;

    private Vector2 movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        if (!_playerExists)
        {
            _playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb.velocity.magnitude > 0)
        {
            _playerMoving = true;
        }
        else
        {
            _playerMoving = false;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime, 0f, 0f));
            // _rb.velocity =
                // new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, _rb.velocity.y);
            _playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime, 0f));
            // _rb.velocity =
                // new Vector2(_rb.velocity.x, Input.GetAxisRaw("Vertical") * movementSpeed);
            _playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }


        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            // _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        }

        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        animator.SetBool("PlayerMoving", _playerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + movement * (movementSpeed * Time.fixedDeltaTime));
    }
}