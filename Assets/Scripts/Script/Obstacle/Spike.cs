using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float SpikeSpawn = 4;
    float timer = 0.0f;
    void Start()
    {

        transform.localScale = new Vector3(0.05f,  0.1f, 3f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpikeSpawn)
        {
            gameObject.SetActive(true);
            if (timer >= SpikeSpawn + 1)
            {
                gameObject.SetActive(false);
                timer = 0;
            }
        }
    }
}
