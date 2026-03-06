using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class MusicManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioSource musicPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
        }
        Load();
    }
    public void ChangeVolume()
    {
        musicPlayer.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}