using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TitleSetting : UI_Popup
{
    public Slider volumeSlider;
    public enum Sliders
    {
        VolumeSlider
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Init()
    {
        base.Init();
        Bind<Slider>(typeof(Sliders));
        volumeSlider = GetSlider((int)Sliders.VolumeSlider);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
