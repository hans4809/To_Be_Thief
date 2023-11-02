using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTest : BaseScene
{
    // Start is called before the first frame update
    [SerializeField]
    List<Define.Items> items = new List<Define.Items>();
    void Start()
    {
        
    }
    protected override void Init()
    {
        base.Init();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
