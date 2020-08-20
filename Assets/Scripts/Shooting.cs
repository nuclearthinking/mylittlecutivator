using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform firePoint;
    public GameObject arrowPrefab;
    public float arrowForce = 20f;
    public Animator animator;
    public float AttackRate = 1.0f;
    public Button attackButton;

    private float nextFireTime;


    private void Start()
    {
        nextFireTime = Time.time;
        attackButton.onClick.AddListener(Shoot);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + AttackRate;
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Attack");
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * arrowForce, ForceMode2D.Impulse);
    }
}