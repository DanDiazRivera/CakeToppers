using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string slider;

    public void SetLevel (float slidervalue)
    {
        mixer.SetFloat(slider, Mathf.Log10 (slidervalue) * 20);
    }
}
