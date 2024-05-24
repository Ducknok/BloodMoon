using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer mainAudioMixer;
    public void ChangeGraphicsQUality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
    public void changeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }
    public void changeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }
    public void changeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", sfxVol.value);
    }
}
