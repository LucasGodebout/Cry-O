using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider soundSlider;

    public void Start()
    {
        audioMixer.GetFloat("Music", out float musicVolume);
        musicSlider.value = musicVolume;
        audioMixer.GetFloat("Sound", out float soundVolume);
        soundSlider.value = soundVolume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music",volume);
    }

    public void  SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat("Sound",volume);
    }
}
