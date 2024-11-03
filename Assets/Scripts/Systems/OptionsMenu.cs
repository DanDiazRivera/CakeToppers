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

    public AudioMixer mixer;

    float currentMasterVolume = 1;
    float currentMusicVolume = 1;

    OptionsSaveData save;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);

        OptionsSaveData.Get(ref save);
        masterVolumeSlider.onValueChanged.AddListener(OnMasterChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicChanged);
        LoadData();
    }

    public static void MakeAppear() => Get().holder.SetActive(true);

    void LoadData()
    {
        save.Load();
        currentMasterVolume = save.masterVolume;
        currentMusicVolume = save.musicVolume;
        masterVolumeSlider.value = currentMasterVolume;
        musicVolumeSlider.value = currentMusicVolume;
        mixer.SetFloat("MasterVolume", ValueToVolume(currentMasterVolume));
        mixer.SetFloat("MusicVolume", ValueToVolume(currentMusicVolume));
    }

    public void ApplyChanges()
    {
        save.masterVolume = currentMasterVolume;
        save.musicVolume = currentMusicVolume;
        save.Save();
    }
    public void CancelChanges() => LoadData();

    public void ResetMixer()
    {
        currentMasterVolume = 1;
        currentMusicVolume = 1;
        masterVolumeSlider.value = 1;
        musicVolumeSlider.value = 1;
        mixer.SetFloat("MasterVolume", ValueToVolume(1));
        mixer.SetFloat("MusicVolume", ValueToVolume(1));

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
}