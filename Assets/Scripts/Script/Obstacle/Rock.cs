using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float RockSpeed_level; //이동속도 변수
    public float Rockspawn_level; // 스폰시간 변수
    public float Rocksize_level;  // 크기 변수

    float originx;
    float timer;
    float rotationTime;

    public int isReturn;
    public int isFlip;

    SpriteRenderer rend;

    Vector3 rotation;
    private void Awake()
    {
        originx = transform.position.x;
    }
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Rocksize_level = Managers.Data.currentStat[6]; //  값의 변화를 조절 , Sprite에 이미지 적용으로 값 조절
        RockSpeed_level = Managers.Data.currentStat[5];
        Rockspawn_level = Managers.Data.currentStat[4];
    }
    void Update()
    {
        timer += Time.deltaTime;
        rotationTime += Time.deltaTime;
    }

    void FixedUpdate() 
    {
        if(timer > Rockspawn_level) //스폰시간 이후 움직임
        {
            transform.position = transform.position + Vector3.right * RockSpeed_level * Time.deltaTime;
            rotation = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 0, -359), rotationTime);
            transform.localEulerAngles = rotation;
        }

        if (RockSpeed_level < 0) rend.flipX = true;
        else rend.flipX = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "RReturn" && (isReturn == 1 && isFlip == 0)) //왼->오->왼 왕복
        {
            RockSpeed_level = -RockSpeed_level;
        }
        if (collision.gameObject.name == "LReturn" && (isReturn == 1 && isFlip == 1)) //오->왼->오 왕복
        {
            RockSpeed_level = -RockSpeed_level;
        }
        if (collision.gameObject.tag == "Border")
        {
            transform.position = new Vector3(originx, transform.position.y, 0); 
            timer = 0; //화면 밖으로 나가면 timer 초기화
            rotationTime = 0;
            if(isReturn == 1) //1회 왕복 후 화면 밖으로 나갔을 때
            {
                RockSpeed_level = -RockSpeed_level;
            }
        }
        
    }
    private void OnEnable()
    {
        Rockspawn_level = Managers.Data.currentStat[4];
        RockSpeed_level = Managers.Data.currentStat[5];
        Rocksize_level = Managers.Data.currentStat[6];
        transform.localScale = new Vector3(Rocksize_level, Rocksize_level * 3.5f, 1);
        timer = 0.0f;
        rotationTime = 0.0f;
        isFlip = Random.Range(0, 2);
        isReturn = Random.Range(0, 2);
        if (isFlip == 1) // isFlip 1이면 오->왼
        {
            RockSpeed_level = -RockSpeed_level;
            transform.position = new Vector3(-originx, transform.position.y, 0);
        }
        else
        {
            transform.position = new Vector3(originx, transform.position.y, 0);
        }
    }

    public void UpdateStat()
    {
        Rockspawn_level = Managers.Data.currentStat[4];
        RockSpeed_level = Managers.Data.currentStat[5];
        Rocksize_level = Managers.Data.currentStat[6];
        transform.localScale = new Vector3(Rocksize_level, Rocksize_level * 3.5f, 1);
        if (isFlip == 1){   RockSpeed_level = -RockSpeed_level; }
    }
}
