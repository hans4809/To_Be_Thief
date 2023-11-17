using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem
{
    public int itemIndex;
    public bool isDebuff;
}

public class UI_SelectItem : UI_Popup
{
    public SelectItem firstItem = new SelectItem();
    public SelectItem secondItem = new SelectItem();
    public SelectItem thirdItem = new SelectItem();

    public int debuff_prob = 95;

    public UI_ItemButton firstItemButton = new UI_ItemButton();
    public UI_ItemButton secondItemButton = new UI_ItemButton();
    public UI_ItemButton thirdItemButton = new UI_ItemButton();

    public List<int> non_Level1Item = new List<int>();

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

        foreach(var item in Managers.Data.currentLevel)
        {
            if (item.Value != 1)
                non_Level1Item.Add((int)item.Key);
        }

        do {
            do { RandomItemIndex(firstItem); } while (!CheckItemLevel(firstItem));
            do { RandomItemIndex(secondItem); } while (!CheckItemLevel(secondItem));
            do { RandomItemIndex(thirdItem); } while (!CheckItemLevel(thirdItem));
        } while (firstItem.itemIndex == secondItem.itemIndex || secondItem.itemIndex == thirdItem.itemIndex || thirdItem.itemIndex == firstItem.itemIndex);
        
        firstItemButton = ItemButtonInit(firstItem);
        secondItemButton = ItemButtonInit(secondItem);
        thirdItemButton = ItemButtonInit(thirdItem);
    }

    bool IsDebuff()
    {
        if (Random.Range(1, 100) <= debuff_prob || non_Level1Item.Count == 0)
            return true;
        else
            return false;
    }

    void RandomItemIndex(SelectItem selectItem)
    {
        selectItem.isDebuff = IsDebuff();

        if (selectItem.isDebuff)
        {
            selectItem.itemIndex = Random.Range(0, Managers.Data.currentLevel.Count);
        }
        else
        {
            selectItem.itemIndex = non_Level1Item[Random.Range(0, non_Level1Item.Count)];
        }
    }
    bool CheckItemLevel(SelectItem selectItem)
    {
        if (Managers.Data.currentLevel[selectItem.itemIndex] == 3 && selectItem.isDebuff) return false;
        return true;
    }

    UI_ItemButton ItemButtonInit(SelectItem selectItem)
    {
        UI_ItemButton uI_ItemButton = Managers.UI.ShowAnyUI<UI_ItemButton>();
        uI_ItemButton.transform.SetParent(Get<GameObject>((int)GameObjects.GridPanel).transform);
        uI_ItemButton.transform.localScale = new Vector3(1, 1, 1);
        uI_ItemButton.itemIndex = selectItem.itemIndex;
        uI_ItemButton.isDebuff = selectItem.isDebuff;
        return uI_ItemButton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
