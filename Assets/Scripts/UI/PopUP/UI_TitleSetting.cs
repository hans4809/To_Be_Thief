using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TitleSetting : UI_Popup
{
    Slider masterSlider;
    Slider bgmSlider;
    Slider sfxSlider;
    Image masterVolumeImgae;
    Image bgmVolumeImgae;
    Image sfxVolumeImgae;
    public enum GameObjects
    {
        CloseButton,
        CreditButton,
        MasterVolumeImage,
        BGMVolumeImage,
        SFXVolumeImage,
        MasterSlider,
        BGMSlider,
        SFXSlider
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
        Get<GameObject>((int)GameObjects.CreditButton).AddUIEvent(CreditButtonClicked);
        masterSlider = Get<GameObject>((int)GameObjects.MasterSlider).GetComponent<Slider>();
        bgmSlider = Get<GameObject>((int)GameObjects.BGMSlider).GetComponent<Slider>();
        sfxSlider = Get<GameObject>((int)GameObjects.SFXSlider).GetComponent<Slider>();
        masterVolumeImgae = Get<GameObject>((int)GameObjects.MasterVolumeImage).GetComponent<Image>();
        bgmVolumeImgae = Get<GameObject>((int)GameObjects.BGMVolumeImage).GetComponent<Image>();
        sfxVolumeImgae = Get<GameObject>((int)GameObjects.SFXVolumeImage).GetComponent<Image>();

        masterSlider.value = Managers.Data.volumeData.masterVolume;
        bgmSlider.value = Managers.Data.volumeData.bgmVolume;
        sfxSlider.value = Managers.Data.volumeData.sfxVolume;

        masterSlider.gameObject.AddUIEvent(AdjustMasterVolume, Define.UIEvent.Drag);
        bgmSlider.gameObject.AddUIEvent(AdjustBGMVolume, Define.UIEvent.Drag);
        sfxSlider.gameObject.AddUIEvent(AdjustSFXVolume, Define.UIEvent.Drag);
    }
    public void AdjustMasterVolume(PointerEventData data)
    {
        Managers.Data.volumeData.masterVolume = masterSlider.value;
        SetVolumeImage(masterSlider, masterVolumeImgae);
        if (masterSlider.value <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("Master", -80);
        }
        Managers.Sound.audioMixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20);
    }
    public void AdjustBGMVolume(PointerEventData data)
    {
        Managers.Data.volumeData.bgmVolume = bgmSlider.value;
        SetVolumeImage(bgmSlider, bgmVolumeImgae);
        if (bgmSlider.value <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("BGM", -80);
        }
        Managers.Sound.audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
    }
    public void AdjustSFXVolume(PointerEventData data)
    {
        Managers.Data.volumeData.sfxVolume = sfxSlider.value;
        SetVolumeImage(sfxSlider, sfxVolumeImgae);
        if (sfxSlider.value <= -40f)
        {
            Managers.Sound.audioMixer.SetFloat("SFX", -80);
        }
        Managers.Sound.audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
    }
    public void SetVolumeImage(Slider volumeSlider, Image volumeImage)
    {
        if (0 <= volumeSlider.value && volumeSlider.value < 0.33)
        {
            volumeImage.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Speaker1");
        }
        if (0.33 <= volumeSlider.value && volumeSlider.value < 0.66)
        {
            volumeImage.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Speaker2");
        }
        if (0.66 <= volumeSlider.value && volumeSlider.value <= 1)
        {
            volumeImage.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Speaker3");
        }
    }
    public void CloseButtonClicked(PointerEventData data)
    {
        ClosePopUPUI();
    }
    // Update is called once per frame
    public void CreditButtonClicked(PointerEventData data)
    {
        Managers.UI.ShowPopUpUI<UI_Credit>();
    }
    void Update()
    {
        
    }
}
