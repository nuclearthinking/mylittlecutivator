using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TreantController : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public float timeToMove;
    public float timeBetweenMove;
    public HealthBar treantHealthBar;
    private bool _moving;
    private float _timeBetweenMoveCounter;
    private float _timeToMoveCounter;
    private Vector3 _moveDirection;
    private Vector2 _lastMove;

    public float waitToReload;
    private bool reloading;

    private GameObject thePlayer;

    public int hitPoints = 100;
    public GameObject deathEffect;

    private Vector2 movement;

    void Start()
    {
        _timeBetweenMoveCounter = Random.Range(
            timeBetweenMove * 0.75f,
            timeBetweenMove * 1.25f
        );
        _timeToMoveCounter = Random.Range(
            timeToMove * 0.75f,
            timeToMove * 1.25f
        );
        treantHealthBar.SetMaxHealth(hitPoints);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("Moving", _moving);
        animator.SetFloat("LastMoveX", _lastMove.x);
        animator.SetFloat("LastMoveY", _lastMove.y);

        if (reloading)
        {
            waitToReload -= Time.deltaTime;
            if (waitToReload < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                thePlayer.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {    
        if (_moving)
        {
            _timeToMoveCounter -= Time.deltaTime;
            rb.MovePosition(rb.position + movement * (movementSpeed * Time.fixedDeltaTime));
            if (_timeToMoveCounter < 0f)
            {
                _moving = false;
                _timeBetweenMoveCounter = Random.Range(
                    timeBetweenMove * 0.75f,
                    timeBetweenMove * 1.25f
                );
            }
        }
        else
        {
            _timeBetweenMoveCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            if (_timeBetweenMoveCounter < 0f)
            {
                _moving = true;
                _timeToMoveCounter = Random.Range(
                    timeToMove * 0.75f,
                    timeToMove * 1.25f
                );
                movement.x = Random.Range(-1f, 1f);
                movement.y = Random.Range(-1f, 1f);
                _lastMove = movement;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.name == "Hero")
        // {
        //     other.gameObject.SetActive(false);
        //     reloading = true;
        //     thePlayer = other.gameObject;
        // }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        treantHealthBar.SetHealth(hitPoints);

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect, 2f);
    }
}