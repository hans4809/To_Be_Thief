using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public List<GameObject> Prefabs;

    List<GameObject> Map_Round_1 = new List<GameObject>();
    List<GameObject> Map_Round_2 = new List<GameObject>();
    List<GameObject> Map_Round_3 = new List<GameObject>();

    void Awake()
    {
        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < 7; i++)
        {
            Map_Round_1.Add(Instantiate(Prefabs[0], transform));
            Map_Round_1[i].SetActive(false);
            Map_Round_2.Add(Instantiate(Prefabs[1], transform));
            Map_Round_2[i].SetActive(false);
            Map_Round_3.Add(Instantiate(Prefabs[2], transform));
            Map_Round_3[i].SetActive(false);
        }
    }

    public void Clear()
    {
        foreach (GameObject obj in Map_Round_1) { obj.SetActive(false); }
        foreach (GameObject obj in Map_Round_2) { obj.SetActive(false); }
        foreach (GameObject obj in Map_Round_3) { obj.SetActive(false); }
    }
    public GameObject MakeMap(int type)
    {
    
        if (type == 0)
        {
            for (int i = 0; i < Map_Round_1.Count; i++)
            {
                if (!Map_Round_1[i].activeSelf)
                {
                    Map_Round_1[i].SetActive(true);
                    return Map_Round_1[i];
                }
            }
            Map_Round_1.Add(Instantiate(Prefabs[0], transform));
            return Map_Round_1.Last();
        }

        if (type == 1)
        {
            for (int i = 0; i < Map_Round_2.Count; i++)
            {
                if (!Map_Round_2[i].activeSelf)
                {
                    Map_Round_2[i].SetActive(true);
                    return Map_Round_2[i];
                }
            }
            Map_Round_2.Add(Instantiate(Prefabs[1], transform));
            return Map_Round_2.Last();
        }

        if (type == 2)
        {
            for (int i = 0; i < Map_Round_3.Count; i++)
            {
                if (!Map_Round_3[i].activeSelf)
                {
                    Map_Round_3[i].SetActive(true);
                    return Map_Round_3[i];
                }
            }
            Map_Round_3.Add(Instantiate(Prefabs[2], transform));
            return Map_Round_3.Last();
        }
        return null;
    }
    
}

