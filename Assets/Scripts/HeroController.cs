using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed;
    public Transform firePoint;
    public Animator animator;
    public Rigidbody2D rb;

    public Vector2 lastMove;
    private static bool _playerExists;

    private Vector2 movement;

    void Start()
    {
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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.y == 1.0f && movement.x == 0.0f)
        {
            firePoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f); //UP
        }
        else if (movement.y == -1.0f && movement.x == 0.0f)
        {
            firePoint.transform.rotation = Quaternion.Euler(0f, -0f, 180f); //DOWN
        }
        else if (movement.x == 1.0f && movement.y == 0.0f)
        {    
            firePoint.transform.rotation = Quaternion.Euler(0f, 0f, -90f); //RIGHT
        }
        else if (movement.x == -1.0f && movement.y == 0.0f)
        {
            firePoint.transform.rotation =  Quaternion.Euler(0f, 0f, 90f); //RIGHT
        }
        else if (movement.y == 1.0f && movement.x != 0.0f)
        {
            firePoint.transform.rotation= Quaternion.Euler(0f, 0f, 0f); //UP
        }
        else if (movement.y == -1.0f && movement.x != 0.0f)
        {
            firePoint.transform.rotation = Quaternion.Euler(0f, -0f, 180f); //DOWN
        }

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (movementSpeed * Time.fixedDeltaTime));
    }
}