using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float RockSpeed_level; //이동속도 변수
    public float Rockspawn_level; // 스폰시간 변수
    public float Rocksize_level;  // 크기 변수

    float originx;
    float timer;

    void Start()
    {
        Rocksize_level = 0.1f; //  값의 변화를 조절 , Sprite에 이미지 적용으로 값 조절
        transform.localScale = new Vector3(Rocksize_level, Rocksize_level*1.5f, 3f); // 크기 설정

        RockSpeed_level = 5;
        originx = transform.position.x;
        Rockspawn_level = 5;
        timer = 0.0f;

    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if(timer > Rockspawn_level) //스폰시간 이후 움직임
        {
            transform.position = transform.position + Vector3.right * RockSpeed_level * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            transform.position = new Vector3(originx, transform.position.y, 0);
            timer = 0; //화면 밖으로 나가면 timer 초기화
           
        }
        
    }

}
