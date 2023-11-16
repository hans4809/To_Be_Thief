using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemButton : UI_Base
{
    public int itemIndex;
    public bool isDebuff;
    private Define.ItemType itemType;
    private Define.ItemKey itemKey;
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
        itemType = (Define.ItemType)itemIndex;
        itemKey = new Define.ItemKey(itemType, Managers.Data.currentLevel[itemType], isDebuff);
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemImage).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Images/{Managers.Data.itemDict[itemKey].itemName}");
        Get<GameObject>((int)GameObjects.ItemName).GetComponent<Text>().text = $"{Managers.Data.itemDict[itemKey].itemName}";
        Get<GameObject>((int)GameObjects.ItemUse).GetComponent<Text>().text = $"{Managers.Data.itemDict[itemKey].itemExplain}";
        gameObject.AddUIEvent(ButtonClicked);
    }
    
    // Start is called before the first frame update

    public void ButtonClicked(PointerEventData eventData)
    {
        if (isDebuff)
        {
            Managers.Data.currentLevel[itemType]++;
            itemKey.level = Managers.Data.currentLevel[itemType];
            switch (Managers.Data.currentLevel[itemType])
            {
                case 2:
                    Managers.Data.currentStat[itemType] = Managers.Data.itemDict[itemKey].effect;
                    break;
                case 3:
                    Managers.Data.currentStat[itemType] = Managers.Data.itemDict[itemKey].effect;
                    break;
            }
        }
        else
        {
            Managers.Data.currentLevel[itemType]--;
            itemKey.level = Managers.Data.currentLevel[itemType];
            switch (Managers.Data.currentLevel[itemType])
            {
                case 1:
                    Managers.Data.currentStat[itemType] = Managers.Data.itemDict[itemKey].effect;
                    break;
                case 2:
                    Managers.Data.currentStat[itemType] = Managers.Data.itemDict[itemKey].effect;
                    break;
            }
        }
        Managers.Game.itemSelected++;
        Debug.Log(Managers.Data.currentLevel[itemType]);
        Debug.Log(Managers.Data.currentStat[itemType]);
        Managers.UI.CloseAllPopUPUI();
        Time.timeScale = 1;
        Managers.Game.currentState = GameManager.GameState.Playing;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
