using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Animator anim;
    BoxCollider2D box;
    public float SpikeSpawn = 4;
    float timer = 0.0f;
    void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpikeSpawn)
        {
            anim.SetBool("SpikeUp", true);
            box.enabled = true;
            if (timer >= SpikeSpawn + 1)
            {
                timer = 0;
            }
        }
        else if (timer < SpikeSpawn)
        {
            box.enabled = false;
            anim.SetBool("SpikeUp", false);
        }
    }
}
