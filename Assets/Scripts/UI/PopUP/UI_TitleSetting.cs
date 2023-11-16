using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TitleSetting : UI_Popup
{
    public Slider volumeSlider;
    public enum GameObjects
    {
        CloseButton,
        VolumeSlider
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
        Get<GameObject>((int)GameObjects.CloseButton).AddUIEvent(CloseButtonClicked);
        volumeSlider = Get<GameObject>((int)GameObjects.VolumeSlider).GetComponent<Slider>();
        volumeSlider.gameObject.AddUIEvent(AdjustVolume, Define.UIEvent.Drag);
    }
    public void AdjustVolume(PointerEventData data)
    {
        if (volumeSlider.value < -40f)
        {
            Managers.Sound.audioMixer.SetFloat("Master", -80);
        }
        Managers.Sound.audioMixer.SetFloat("Master", Mathf.Log10(volumeSlider.value) * 20);
    }
    public void CloseButtonClicked(PointerEventData data)
    {
        ClosePopUPUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
