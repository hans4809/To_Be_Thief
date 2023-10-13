using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float RockSpeed;
    float originx;

    void Start()
    {
        RockSpeed = 5;
        originx = transform.position.x;
    }

    void FixedUpdate()
    {
        transform.position = transform.position + Vector3.right * RockSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            transform.position = new Vector3(originx, transform.position.y, 0);
           
        }
        
    }

}
