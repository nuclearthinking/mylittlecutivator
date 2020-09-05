using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageScrollingText : MonoBehaviour
{
    public float textScrollingSpeed = 0;
    public int damageAmount;
    public Text displayTextElement;

    void Update()
    {    
        displayTextElement.text = Convert.ToString(damageAmount);
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + (textScrollingSpeed * Time.deltaTime),
            transform.position.z
        );
        
        Destroy(gameObject, 1f);
    }
}