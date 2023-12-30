using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameSetting : UI_Popup
{
    Slider masterSlider;
    Slider bgmSlider;
    Slider sfxSlider;
    Image masterVolumeImgae;
    Image bgmVolumeImgae;
    Image sfxVolumeImgae;

    public enum GameObjects
    {
        MasterVolumeImage,
        BGMVolumeImage,
        SFXVolumeImage,
        MasterSlider,
        BGMSlider,
        SFXSlider,
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
        masterSlider = Get<GameObject>((int)GameObjects.MasterSlider).GetComponent<Slider>();
        bgmSlider = Get<GameObject>((int)GameObjects.BGMSlider).GetComponent<Slider>();
        sfxSlider = Get<GameObject>((int) GameObjects.SFXSlider).GetComponent<Slider>();
        masterVolumeImgae = Get<GameObject>((int)GameObjects.MasterVolumeImage).GetComponent<Image>();
        bgmVolumeImgae = Get<GameObject>((int)GameObjects.BGMVolumeImage).GetComponent<Image>();
        sfxVolumeImgae = Get<GameObject>((int)GameObjects.SFXVolumeImage).GetComponent<Image>();

        masterSlider.value = Managers.Data.volumeData.masterVolume;
        bgmSlider.value = Managers.Data.volumeData.bgmVolume;
        sfxSlider.value = Managers.Data.volumeData.sfxVolume;

        Get<GameObject>((int)GameObjects.ReplayButton).AddUIEvent(ReplayButtonClicked);
        Get<GameObject>((int)GameObjects.ContinueButton).AddUIEvent(ContinueClicked);
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
        if(0 <= volumeSlider.value && volumeSlider.value < 0.33)
        {
            volumeImage.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Speaker1");
        }
        if(0.33 <= volumeSlider.value && volumeSlider.value < 0.66)
        {
            volumeImage.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Speaker2");
        }
        if(0.66 <= volumeSlider.value && volumeSlider.value <= 1)
        {
            volumeImage.sprite = Managers.Resource.Load<Sprite>("Sprites/UI/Speaker3");
        }
    }
    public void ReplayButtonClicked(PointerEventData data)
    {
        //오브젝트 초기화 
        // BackGround 초기화
        GameObject[] patterns = GameObject.FindGameObjectsWithTag("pattern");
        for (int i = 0; i < patterns.Length; i++) {
            if (patterns[i].activeSelf)
                patterns[i].SetActive(false);
        }
        //Obastacle 초기화
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < Objects.Length; i++) {if (Objects[i].activeSelf) Objects[i].SetActive(false);}

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
