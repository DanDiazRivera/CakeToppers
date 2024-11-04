using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class OptionsMenu : Singleton<OptionsMenu>
{
    public GameObject holder;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider ambVolumeSlider;

    public AudioMixer mixer;

    float currentMasterVolume = 1;
    float currentMusicVolume = 1;
    float currentSFXVolume = 1;
    float currentAMBVolume = 1;

    OptionsSaveData save;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);

        OptionsSaveData.Get(ref save);
        masterVolumeSlider.onValueChanged.AddListener(OnMasterChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXChanged);
        ambVolumeSlider.onValueChanged.AddListener(OnAMBChanged);
    }

    private void Start() => LoadData();

    public static void MakeAppear() => Get().holder.SetActive(true);

    void LoadData()
    {
        save.Load();
        currentMasterVolume = save.masterVolume;
        currentMusicVolume = save.musicVolume;
        currentSFXVolume = save.sfxVolume;
        currentAMBVolume = save.ambVolume;

        masterVolumeSlider.value = currentMasterVolume;
        musicVolumeSlider.value = currentMusicVolume;
        sfxVolumeSlider.value = currentSFXVolume;
        ambVolumeSlider.value = currentAMBVolume;


        mixer.SetFloat("MasterVolume", ValueToVolume(currentMasterVolume));
        mixer.SetFloat("MusicVolume", ValueToVolume(currentMusicVolume));
        mixer.SetFloat("SFXVolume", ValueToVolume(currentSFXVolume));
        mixer.SetFloat("AMBVolume", ValueToVolume(currentAMBVolume));
    }

    public void ApplyChanges()
    {
        save.masterVolume = currentMasterVolume;
        save.musicVolume = currentMusicVolume;
        save.sfxVolume = currentSFXVolume;
        save.ambVolume = currentAMBVolume;
        save.Save();
    }
    public void CancelChanges() => LoadData();

    public void ResetMixer()
    {
        currentMasterVolume = 1;
        currentMusicVolume = 1;
        currentSFXVolume = 1;
        currentAMBVolume = 1;

        masterVolumeSlider.value = 1;
        musicVolumeSlider.value = 1;
        sfxVolumeSlider.value = 1;
        ambVolumeSlider.value = 1;

        mixer.SetFloat("MasterVolume", ValueToVolume(1));
        mixer.SetFloat("MusicVolume", ValueToVolume(1));
        mixer.SetFloat("SFXVolume", ValueToVolume(1));
        mixer.SetFloat("AMBVolume", ValueToVolume(1));

    }

    private void OnMasterChanged(float value)
    {
        currentMasterVolume = value;
        mixer.SetFloat("MasterVolume", ValueToVolume(value));
    }
    private void OnMusicChanged(float value)
    {
        currentMusicVolume = value;
        mixer.SetFloat("MusicVolume", ValueToVolume(value));
    }
    private void OnSFXChanged(float value)
    {
        currentSFXVolume = value;
        mixer.SetFloat("SFXVolume", ValueToVolume(value));
    }
    private void OnAMBChanged(float value)
    {
        currentAMBVolume = value;
        mixer.SetFloat("AMBVolume", ValueToVolume(value));
    }

    private float ValueToVolume(float value) => value switch
        {
            0 => -144f,
            > 1 => Mathf.Log10(1 + ((value - 1) * 20)) * 20f,
            _ => Mathf.Log10(value)* 20f
        };

}

public class OptionsSaveData : SaveData<OptionsSaveData>
{
    protected override string FileName() => "Settings";

    public float masterVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;
    public float ambVolume = 1;
}