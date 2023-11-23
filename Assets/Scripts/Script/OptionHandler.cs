using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionHandler : MonoBehaviour
{
    public void PopupOption()
    {
        Managers.UI.ShowPopUpUI<UI_Option>();
        Time.timeScale = 0;
    }
}
