using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlidersManager : MonoBehaviour
{
    [SerializeField]
    private Slider m_masterVolumeSlider = null;
    [SerializeField]
    private Slider m_musicVolumeSlider = null;
    [SerializeField]
    private Slider m_sfxVolumeSlider = null;

    [SerializeField]
    private SoundManager m_soundManager = null;


    // Start is called before the first frame update
    void Start()
    {
        m_soundManager.LoadVolumes();

        m_masterVolumeSlider.value = m_soundManager.GetMasterVolume();
        m_musicVolumeSlider.value = m_soundManager.GetMusicVolume();
        m_sfxVolumeSlider.value = m_soundManager.GetSFXVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
