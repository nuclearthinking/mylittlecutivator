using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class TreantController : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D _myRigidBody;
    private bool _moving;
    public float timeBetweenMove;
    private float _timeBetweenMoveCounter;
    public float timeToMove;
    private float _timeToMoveCounter;
    private Vector3 _moveDirection;

    private Animator _treantAnimator;
    private Vector2 _lastMove;

    public float waitToReload;
    private bool reloading;

    private GameObject thePlayer;

    public int hitPoints = 100;
    public GameObject deathEffect;
    void Start()
    {
        _treantAnimator = GetComponent<Animator>();
        _myRigidBody = GetComponent<Rigidbody2D>();

        _timeBetweenMoveCounter = Random.Range(
            timeBetweenMove * 0.75f,
            timeBetweenMove * 1.25f
        );
        _timeToMoveCounter = Random.Range(
            timeToMove * 0.75f,
            timeToMove * 1.25f
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (_moving)
        {
            _timeToMoveCounter -= Time.deltaTime;
            _myRigidBody.velocity = _moveDirection;

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
            _myRigidBody.velocity = Vector2.zero;
            if (_timeBetweenMoveCounter < 0f)
            {
                _moving = true;
                _timeToMoveCounter = Random.Range(
                    timeToMove * 0.75f,
                    timeToMove * 1.25f
                );
                _moveDirection = new Vector3(
                    Random.Range(-1f, 1f) * movementSpeed,
                    Random.Range(-1f, 1f) * movementSpeed,
                    0f);
                _lastMove = _moveDirection;
            }
        }

        _treantAnimator.SetFloat("MoveX", _myRigidBody.velocity.x);
        _treantAnimator.SetFloat("MoveY", _myRigidBody.velocity.y);
        _treantAnimator.SetBool("Moving", _moving);
        _treantAnimator.SetFloat("LastMoveX", _lastMove.x);
        _treantAnimator.SetFloat("LastMoveY", _lastMove.y);

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            other.gameObject.SetActive(false);
            reloading = true;
            thePlayer = other.gameObject;
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}