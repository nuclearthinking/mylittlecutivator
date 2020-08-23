using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOld : MonoBehaviour
{
    // Start is called before the first frame update

    public int damegeDealth = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            other.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(damegeDealth);
        }
    }
}
