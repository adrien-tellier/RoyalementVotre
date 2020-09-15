using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_menuMusic = null;
    [SerializeField]
    private AudioSource m_gameMusic = null;
    [SerializeField]
    private AudioSource m_winMusic = null;
    [SerializeField]
    private AudioSource m_loseMusic = null;
    [SerializeField]
    private AudioSource m_ambientMusic = null;

    // Start is called before the first frame update
    void Start()
    {
        PlaySceneMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        PlaySceneMusic();
    }

    private void PlaySceneMusic()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            m_menuMusic.Stop();
            m_ambientMusic.Play();
            m_gameMusic.Play();
        }
        else if (SceneManager.GetActiveScene().name == "WinMenu")
        {
            m_gameMusic.Stop();
            m_ambientMusic.Stop();
            m_winMusic.Play();
        }
        else if (SceneManager.GetActiveScene().name == "LoseMenu")
        {
            m_gameMusic.Stop();
            m_ambientMusic.Stop();
            m_loseMusic.Play();
        }
        else if (!m_menuMusic.isPlaying)
        {
            m_gameMusic.Stop();
            m_ambientMusic.Stop();
            m_menuMusic.Play();
        }
    }
}
