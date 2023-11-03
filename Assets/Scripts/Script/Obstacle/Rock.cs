using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float RockSpeed;
    float originx;

    public float Rocksize_level ;  // 크기 변수

    void Start()
    {
        Rocksize_level = 0.75f; //  값의 변화를 조절 
        transform.localScale = new Vector3(Rocksize_level, Rocksize_level, 3f); // 크기 설정
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
