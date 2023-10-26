using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public ObjectManager ObjectManager;
    public GameManager GameManager;
    public int score;
    Rigidbody2D rigid;
    Animator anim;

    int patternY = 15;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Player Animation
        if (Input.GetMouseButton(0))
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

        //Player Score
        //GameManager.Score = (int)transform.position.y / 2;
        score = (int)transform.position.y / 2;
        //DeletePattern
        StartCoroutine(DeletePattern());

    }

    private void FixedUpdate()
    {
        //Player Move
        if (!Input.GetMouseButton(0))
            transform.position = transform.position + Vector3.up * 1.5f * Time.deltaTime;
    }

    //Make pattern
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pattern")
        {
            GameObject newPattern = ObjectManager.MakeObj(Random.Range(0, 3));
            newPattern.transform.position = new Vector3(0, patternY, 0);
            patternY += 6;
        }
    }

    //Coroutine Delete pattern
    IEnumerator DeletePattern()
    {
        GameObject[] patterns = GameObject.FindGameObjectsWithTag("pattern");
        for (int i = 0; i < patterns.Length; i++)
        {
            if (gameObject.transform.position.y - patterns[i].transform.position.y > 15)
                patterns[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
    }
    
    //GameOver
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
            Debug.Log("�׾����ϴ�.");
    }

}
