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
    public Joystick joystick;
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
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        if (movement.y == 1.0f && movement.x == 0.0f)
        {
            firePoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f); //UP
        }
        else if (movement.y >= -0.3f && movement.x == 0.0f)
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

        if (movement.x > 0.5f || movement.x < -0.5f)
        {
            lastMove = new Vector2(movement.x, 0f);
        }

        if (movement.y > 0.5f || movement.y < -0.5f)
        {
            lastMove = new Vector2(0f, movement.y);
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (movementSpeed * Time.fixedDeltaTime));
    }
}