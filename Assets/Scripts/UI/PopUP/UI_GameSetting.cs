using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameSetting : UI_Popup
{
    Slider volumeSlider;
    public enum GameObjects
    {
        VolumeSlider,
        ReplayButton,
        ContinueButton
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
        volumeSlider = Get<GameObject>((int)GameObjects.VolumeSlider).GetComponent<Slider>();
        Get<GameObject>((int)GameObjects.ReplayButton).AddUIEvent(ReplayButtonClicked);
        Get<GameObject>((int)GameObjects.ContinueButton).AddUIEvent(ContinueClicked);
    }
    public void AdjustVolume(PointerEventData data)
    {
        if (volumeSlider.value < -40f)
        {
            Managers.Sound.audioMixer.SetFloat("Master", -80);
        }
        Managers.Sound.audioMixer.SetFloat("Master", Mathf.Log10(volumeSlider.value) * 20);
    }
    public void ReplayButtonClicked(PointerEventData data)
    {
        Managers.Game.GameStart();
    }
    public void ContinueClicked(PointerEventData data)
    {
        ClosePopUPUI();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
