using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemImage).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Images/{Managers.Data.items[itemIndex].itemType}");
        Get<GameObject>((int)GameObjects.ItemName).GetComponent<Text>().text = $"{Managers.Data.items[itemIndex].itemType}";
        Get<GameObject>((int)GameObjects.ItemUse).GetComponent<Text>().text = $"{Managers.Data.items[itemIndex].itemExplain}";
        gameObject.AddUIEvent(ButtonClicked);
    }
    
    // Start is called before the first frame update

    public void ButtonClicked(PointerEventData eventData)
    {
        Managers.Data.CurrentLevel[Managers.Data.items[itemIndex].itemType]++;
        switch (Managers.Data.CurrentLevel[Managers.Data.items[itemIndex].itemType])
        {
            case 2:
                Managers.Data.CurrentStat[Managers.Data.items[itemIndex].itemType] = Managers.Data.items[itemIndex].level_2;
                break;
            case 3:
                Managers.Data.CurrentStat[Managers.Data.items[itemIndex].itemType] = Managers.Data.items[itemIndex].level_3;
                break;
        }
        Debug.Log(Managers.Data.CurrentLevel[Managers.Data.items[itemIndex].itemType]);
        Debug.Log(Managers.Data.CurrentStat[Managers.Data.items[itemIndex].itemType]);
        Managers.UI.CloseAllPopUPUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
