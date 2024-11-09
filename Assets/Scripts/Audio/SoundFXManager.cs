using EditorAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundFXManager : Singleton<SoundFXManager>
{

    [SerializeField] private AudioSource soundFXObject;

    private AudioSource selfSource;
    
    protected override void OnAwake()
    {
        selfSource = GetComponent<AudioSource>();
        if (doRandomAmbience) randomSoundCoroutine = StartCoroutine(RandomSoundsIE());
    }

    protected override void OnDestroyed()
    {
        StopCoroutine(randomSoundCoroutine);
    }

    public void PlaySoundFXClip(AudioClip clip, Vector3 position, float volume = 1)
    {
        AudioSource audioSource = Instantiate(soundFXObject, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    [SerializeField, ToggleGroup("Random Sounds", nameof(delayTime), nameof(randomSounds))] bool doRandomAmbience;
    [SerializeField, HideInInspector] Vector2 delayTime;
    [SerializeField, HideProperty] RandomizedAudio randomSounds;

    private Coroutine randomSoundCoroutine;

    private IEnumerator RandomSoundsIE()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(delayTime.x, delayTime.y));
            selfSource.PlayOneShot(randomSounds);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        if(!selfSource) selfSource = GetComponent<AudioSource>();
        selfSource.PlayOneShot(clip, volume);
    } 




}

[System.Serializable]
public class RandomizedAudio
{
    public AudioClip[] clips;
    public static implicit operator AudioClip(RandomizedAudio S) => S.clips[Random.Range(0, S.clips.Length)];
}