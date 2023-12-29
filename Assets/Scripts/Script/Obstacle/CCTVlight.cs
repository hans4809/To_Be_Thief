using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVlight : MonoBehaviour
{
    public GameObject cctv_light;
    public float CCTVDuration;
    float timer = 0.0f;
    void Start()
    {
        int flag = Random.Range(0, 2);
        if (flag == 1)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1); //랜덤하게 오브젝트 좌우반전 시키기
        CCTVDuration = Managers.Data.currentStat[2];
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 6)
        {
            cctv_light.SetActive(true);
            if (timer >= CCTVDuration + 6)
            {
                cctv_light.SetActive(false);
                timer = 0;
            }
        }
    }
    private void OnEnable()
    {
        CCTVDuration = Managers.Data.currentStat[2];
    }
}
