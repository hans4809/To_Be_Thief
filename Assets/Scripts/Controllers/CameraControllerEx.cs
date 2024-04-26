using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerEx : MonoBehaviour
{
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player").transform;
        transform.position = new Vector3(Target.position.x, Target.position.y + 4.875f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Target.position.x, Target.position.y + 4.875f, transform.position.z);
    }
}
