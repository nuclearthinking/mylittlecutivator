using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum PlayerDirection
{
    Up,
    Left,
    Right,
    Down,
    Zero,
}
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

    public PlayerDirection playerDirection;
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

        

        playerDirection = GetPlayerDirection(movement.x, movement.y);
        Quaternion firePointRotation;
        if (playerDirection == PlayerDirection.Zero)
        {
            var lastPlayerDirection = GetPlayerDirection(lastMove.x, lastMove.y);
            firePointRotation = GetFirePointRotation(lastPlayerDirection);
        }
        else
        {
            firePointRotation = GetFirePointRotation(playerDirection);
        }

        firePoint.transform.rotation = firePointRotation;


        if (movement.x > 0.1f || movement.x < -0.1f)
        {
            lastMove = new Vector2(movement.x, 0f);
        }

        if (movement.y > 0.1f || movement.y < -0.1f)
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
        
        if (movement.x >= .2f)
        {
            movement.x = movementSpeed;
        } else if (movement.x <= -.2f)
        {
            movement.x = -movementSpeed;
        }
        else
        {
            movement.x = 0f;
        }

        if (movement.y >= .2f)
        {
            movement.y = movementSpeed;
        } else if (movement.y <= -.2f)
        {
            movement.y = -movementSpeed;
        }
        else
        {
            movement.y = 0f;
        }
    }

    PlayerDirection GetPlayerDirection(float joystickX, float joystickY)
    {
        if (joystickX > 0.1f && joystickY < 0.1f && joystickY >= 0.0f)
            return PlayerDirection.Right;
        if (joystickX < -0.1f && joystickY < 0.1f && joystickY >= 0.0f)
            return PlayerDirection.Left;
        if (joystickY > 0.1f && joystickX < 0.1f && joystickX >= 0.0f)
            return PlayerDirection.Up;
        if (joystickY < -0.1f && joystickX < 0.1f && joystickX >= 0.0f)
            return PlayerDirection.Down;

        return PlayerDirection.Zero;
    }

    Quaternion GetFirePointRotation(PlayerDirection playerDirection)
    {
        if (playerDirection == PlayerDirection.Right)
        {
            return Quaternion.Euler(0f, 0f, -90f); // RIGHT
        }

        if (playerDirection == PlayerDirection.Left)
        {
            return Quaternion.Euler(0f, 0f, 90f); //LEFT
        }

        if (playerDirection == PlayerDirection.Up)
        {
            return Quaternion.Euler(0f, 0f, 0f); //UP
        }

        if (playerDirection == PlayerDirection.Down)
        {
            return Quaternion.Euler(0f, -0f, 180f); //DOWN
        }

        return Quaternion.identity;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
}