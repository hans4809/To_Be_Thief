using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVlight : MonoBehaviour
{
    public float CCTVTime;
    void Start()
    {
        CCTVTime = 3f;
        InvokeRepeating("Appear", 0, CCTVTime);
    }

    void Appear()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
