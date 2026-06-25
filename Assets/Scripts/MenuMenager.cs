using UnityEngine;
using UnityEngine.Audio; 
using UnityEngine.UI;    
using System.Collections.Generic; 

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    public class VolumeSliderMapping
    {
        public Slider slider;
        public string mixerParam;
    }

    [SerializeField] private GameObject menuWindow;
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private List<VolumeSliderMapping> volumeSliders;

    void Start()
    {
        if (mainMixer == null) return;

        foreach (var mapping in volumeSliders)
        {
            if (mapping.slider != null && !string.IsNullOrEmpty(mapping.mixerParam))
            {
                if (mainMixer.GetFloat(mapping.mixerParam, out float currentVolume))
                {
                    mapping.slider.value = currentVolume;
                }
                else
                {
                    mapping.slider.value = 0f;
                }

                mapping.slider.onValueChanged.AddListener((value) => SetVolume(mapping.mixerParam, value));
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (menuWindow != null)
        {
            bool isActive = menuWindow.activeSelf;
            menuWindow.SetActive(!isActive);
        }
    }

    private void SetVolume(string paramName, float value)
    {
        if (mainMixer != null)
        {
            if (value <= -39f)
            {
                mainMixer.SetFloat(paramName, -80f);
            }
            else
            {
                mainMixer.SetFloat(paramName, value);
            }
        }
    }
}