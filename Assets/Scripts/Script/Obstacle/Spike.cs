using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Animator anim;
    BoxCollider2D box;
    public float SpikeSpawn;
    float timer = 0.0f;
    void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        SpikeSpawn = Managers.Data.currentStat[3];
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= SpikeSpawn)
        {
            anim.SetBool("SpikeUp", true);
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("SpikeUp") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
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
    public void UpdateStat()
    {
        SpikeSpawn = Managers.Data.currentStat[3];
    }
}
