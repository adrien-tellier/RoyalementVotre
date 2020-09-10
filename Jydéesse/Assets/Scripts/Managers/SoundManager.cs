using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer m_mixer = null;

    void Start()
    {
		LoadVolumes();
    }

    // Update is called once per frame
    void Update()
    {
	}

	private void OnDestroy()
	{
		SaveVolumes();
	}

	public float GetMasterVolume()
	{
		m_mixer.GetFloat("MasterVolume", out float volume);
		return volume;
	}

	public float GetMusicVolume()
	{
		m_mixer.GetFloat("MusicVolume", out float volume);
		return volume;
	}

	public float GetSFXVolume()
	{
		m_mixer.GetFloat("SFXVolume", out float volume);
		return volume;
	}

	public void SetMasterVolume(float volume)
	{
		if (volume <= -20)
			volume = -80;

		m_mixer.SetFloat("MasterVolume", volume);
	}

	public void SetMusicVolume(float volume)
	{
		if (volume <= -20)
			volume = -80;

		m_mixer.SetFloat("MusicVolume", volume);
	}

	public void SetSFXVolume(float volume)
	{
		if (volume <= -20)
			volume = -80;

		m_mixer.SetFloat("SFXVolume", volume);
	}

	public void SaveVolumes()
	{
		PlayerPrefs.SetFloat("MasterVolume", GetMasterVolume());
		PlayerPrefs.SetFloat("MusicVolume", GetMusicVolume());
		PlayerPrefs.SetFloat("SFXVolume", GetSFXVolume());
	}

	public void LoadVolumes()
	{
		SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
		SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
		SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
	}
}
