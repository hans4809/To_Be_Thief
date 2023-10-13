using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float SpikeTime;
    void Start()
    {
        SpikeTime = 1.5f;
        InvokeRepeating("Appear", 0, SpikeTime);
    }

    void Appear()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
