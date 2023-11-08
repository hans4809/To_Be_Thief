using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemButton : UI_Base
{
    public int itemIndex;
    public enum GameObjects
    {
        ItemImage,
        ItemName,
        ItemUse
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemImage).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Images/{Managers.Data.items[itemIndex].itemType}");
        Get<GameObject>((int)GameObjects.ItemName).GetComponent<Text>().text = $"{Managers.Data.items[itemIndex].itemType}";
        Get<GameObject>((int)GameObjects.ItemUse).GetComponent<Text>().text = $"{Managers.Data.items[itemIndex].itemExplain}";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
