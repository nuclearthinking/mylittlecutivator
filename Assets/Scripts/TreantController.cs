using System.Collections.Generic;
using Core;
using Gameplay;
using Mechanics;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreantController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public float movementSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public float timeToMove;
    public float timeBetweenMove;
    public HealthBar treantHealthBar;
    public DamageScrollingText damageText;
    private bool _moving;
    private float _timeBetweenMoveCounter;
    private float _timeToMoveCounter;
    private Vector3 _moveDirection;
    private Vector2 _lastMove;

    public Model.DropItemChance[] dropList;

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

    void Update()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("Moving", _moving);
        animator.SetFloat("LastMoveX", _lastMove.x);
        animator.SetFloat("LastMoveY", _lastMove.y);
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


    public void TakeDamage(int damage)
    {
        DamageScrollingText damageTextEffect = Instantiate(
            damageText,
            transform.position + new Vector3(0f, 0.5f, 0f),
            Quaternion.identity
        );
        damageTextEffect.damageAmount = damage;

        hitPoints -= damage;
        treantHealthBar.SetHealth(hitPoints);

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        var killEvent = Simulation.Schedule<EnemyKilled>();
        killEvent.treant = this;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        CheckDrop();
        Destroy(gameObject, 5f);
        Destroy(effect, 2f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Simulation.Schedule<EnemySelected>().target = gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Simulation.Schedule<EnemySelected>().target = gameObject;
    }

    void CheckDrop()
    {
        foreach (var drop in dropList)
        {
            var roll = drop.Roll();
            if (roll != null)
            {
                var spawnItemEvent = Simulation.Schedule<SpawnItem>();
                spawnItemEvent.position = gameObject.transform;
                spawnItemEvent.item = roll;
            }
        }
    }
}