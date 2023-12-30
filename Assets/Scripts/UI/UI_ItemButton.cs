using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemButton : UI_Base
{
    public int itemIndex;
    public bool isDebuff;
    public ObjectManager objectManager;
    public PlayerMove player;
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
        player = FindObjectOfType<PlayerMove>();
        objectManager = FindObjectOfType<ObjectManager>();
        if (isDebuff)
        {
            itemKey = new Define.ItemKey(itemIndex, Managers.Data.currentLevel[itemIndex] + 1, isDebuff);
        }
        else
        {
            itemKey = new Define.ItemKey(itemIndex, 0, isDebuff);
        }
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemImage).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Images/{Managers.GoogleSheet.itemDict[itemKey].itemName}");
        Get<GameObject>((int)GameObjects.ItemName).GetComponent<Text>().text = $"{Managers.GoogleSheet.itemDict[itemKey].itemName}";
        Get<GameObject>((int)GameObjects.ItemUse).GetComponent<Text>().text = $"{Managers.GoogleSheet.itemDict[itemKey].itemExplain}";
        gameObject.AddUIEvent(ButtonClicked);
    }
    
    // Start is called before the first frame update

    public void ButtonClicked(PointerEventData eventData)
    {
        if (isDebuff)
        {
            Managers.Data.currentLevel[itemIndex]++;
            itemKey.level = Managers.Data.currentLevel[itemIndex];
            switch (Managers.Data.currentLevel[itemIndex])
            {
                case 2:
                    Managers.Data.currentStat[itemIndex] = Managers.GoogleSheet.itemDict[itemKey].effect;
                    break;
                case 3:
                    Managers.Data.currentStat[itemIndex] = Managers.GoogleSheet.itemDict[itemKey].effect;
                    break;
            }
        }
        else
        {
            Managers.Data.currentLevel[itemIndex]--;
            itemKey.level = Managers.Data.currentLevel[itemIndex];
            itemKey.isDebuff = true;
            switch (Managers.Data.currentLevel[itemIndex])
            {
                case 1:
                    Managers.Data.currentStat[itemIndex] = Managers.GoogleSheet.itemDict[itemKey].effect;
                    break;
                case 2:
                    Managers.Data.currentStat[itemIndex] = Managers.GoogleSheet.itemDict[itemKey].effect;
                    break;
            }
        }
        Managers.Game.itemSelected++;
        player.UpdateStat();
        objectManager.UpdateStat();
        Managers.UI.CloseAllPopUPUI();
        Time.timeScale = 1;
        Managers.Game.currentState = GameManager.GameState.Playing;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
