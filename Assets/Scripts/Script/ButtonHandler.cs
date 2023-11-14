using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public PanelHandler popupWindow;

    public void OnButtonClick_Option()
    {
        popupWindow.Show();
    }

    public void OnButtonClick_Close()
    {
        popupWindow.Close();
    }
}
