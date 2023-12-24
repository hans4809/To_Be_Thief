using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public GameObject[] Prefabs;

    List<GameObject> mapCCTV = new List<GameObject>();
    List<GameObject> mapRock = new List<GameObject>();
    List<GameObject> mapSpike = new List<GameObject>();
    List<GameObject> Break = new List<GameObject>();
    void Awake()
    {
        Generate();
    }

    void Generate()
    {
        for(int i = 0; i < 8; i++)
        {
            mapCCTV.Add(Instantiate(Prefabs[0],transform));
            mapCCTV[i].SetActive(false);
            mapRock.Add(Instantiate(Prefabs[1],transform));
            mapRock[i].SetActive(false);
            mapSpike.Add(Instantiate(Prefabs[2], transform));
            mapSpike[i].SetActive(false);
            Break.Add(Instantiate(Prefabs[3], transform));
            Break[i].SetActive(false);
        }
    }
    public void Clear()
    {
        foreach (GameObject obj in mapCCTV) { obj.SetActive(false); }
        foreach (GameObject obj in mapRock) { obj.SetActive(false); }
        foreach (GameObject obj in mapSpike) { obj.SetActive(false); }
        foreach (GameObject obj in Break) { obj.SetActive(false); }
    }

    public GameObject MakeObj(int type)
    {
        if (type == 3)
        {
            for (int i = 0; i<mapCCTV.Count; i++)
            {
                if (!mapCCTV[i].activeSelf)
                {
                    mapCCTV[i].SetActive(true);
                    return mapCCTV[i];
                }
            }
            mapCCTV.Add(Instantiate(Prefabs[0], transform));
            return mapCCTV.Last();
        }

        if (type == 1)
        {
            for (int i = 0; i < mapRock.Count; i++)
            {
                if (!mapRock[i].activeSelf)
                {
                    mapRock[i].SetActive(true);
                    return mapRock[i];
                }
            }
            mapRock.Add(Instantiate(Prefabs[1], transform));
            return mapRock.Last();
        }

        if (type == 2)
        {
            for (int i = 0; i < mapSpike.Count; i++)
            {
                if (!mapSpike[i].activeSelf)
                {
                    mapSpike[i].SetActive(true);
                    return mapSpike[i];
                }
            }
            mapSpike.Add(Instantiate(Prefabs[2], transform));
            return mapSpike.Last();
        }

        if(type==0)
        {
            for (int i = 0; i < Break.Count; i++)
            {
                if (!Break[i].activeSelf)
                {
                    Break[i].SetActive(true);
                    return Break[i];
                }
            }
            Break.Add(Instantiate(Prefabs[3], transform));
            return Break.Last();
        }

        return null;
    }
}
