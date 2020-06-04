using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Slider))]
public class MusicSlider : MonoBehaviour
{
    public string prefName;
    public AudioMixer mixer;
    public string mixerParameterName;

    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        slider.onValueChanged.AddListener(onSlide);
        if (PlayerPrefs.HasKey(prefName))
            slider.value = PlayerPrefs.GetFloat(prefName);
        else
            PlayerPrefs.SetFloat(prefName, 1);
        mixer.SetFloat(mixerParameterName, 20 * Mathf.Log10(slider.value));
    }

    public void onSlide(float value)
    {
        PlayerPrefs.SetFloat(prefName, value);
        mixer.SetFloat(mixerParameterName, 20 * Mathf.Log10(value));
    }
}
