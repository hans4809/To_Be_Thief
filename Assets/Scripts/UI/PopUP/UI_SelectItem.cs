using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectItem : UI_Popup
{
    public int firstItemIndex;
    public int secondItemIndex;
    public int thirdItemIndex;
    public UI_ItemButton firstItemButton = new UI_ItemButton();
    public UI_ItemButton secondItemButton = new UI_ItemButton();
    public UI_ItemButton thirdItemButton = new UI_ItemButton();

    public enum GameObjects
    {
        GridPanel
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        do {
            do { firstItemIndex = Random.Range(0, Managers.Data.items.Count); } while (!CheckItemLevel(firstItemIndex));
            do { secondItemIndex = Random.Range(0, Managers.Data.items.Count); } while (!CheckItemLevel(secondItemIndex));
            do { thirdItemIndex = Random.Range(0, Managers.Data.items.Count); } while (!CheckItemLevel(thirdItemIndex));
        } while (firstItemIndex == secondItemIndex || secondItemIndex == thirdItemIndex || thirdItemIndex == firstItemIndex);
        ItemButtonInit(firstItemButton, firstItemIndex);
        ItemButtonInit(secondItemButton, secondItemIndex);
        ItemButtonInit(thirdItemButton, thirdItemIndex);
    }

    bool CheckItemLevel(int index)
    {
        if (Managers.Data.CurrentLevel[Managers.Data.items[index].itemType] == 3) return false;
        return true;
    }

    UI_ItemButton ItemButtonInit(UI_ItemButton uI_ItemButton, int itemButtonIndex)
    {
        uI_ItemButton = Managers.UI.ShowAnyUI<UI_ItemButton>();
        uI_ItemButton.transform.SetParent(Get<GameObject>((int)GameObjects.GridPanel).transform);
        uI_ItemButton.transform.localScale = new Vector3(1, 1, 1);
        uI_ItemButton.itemIndex = itemButtonIndex; 
        return uI_ItemButton;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
