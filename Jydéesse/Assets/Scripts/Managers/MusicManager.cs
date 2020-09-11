using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private AudioSource m_menuMusic = null;
    private AudioSource m_gameMusic = null;
    private AudioSource m_winMusic = null;
    private AudioSource m_loseMusic = null;

    private void Awake()
    {
        AudioSource[] musics = GetComponentsInChildren<AudioSource>();
        m_menuMusic = musics[0];
        m_gameMusic = musics[1];
        m_winMusic = musics[2];
        m_loseMusic = musics[3];
        PlaySceneMusic();
    }

    // Start is called before the first frame update
    void Start()
    {

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
            m_gameMusic.Play();
        }
        else if (SceneManager.GetActiveScene().name == "WinMenu")
        {
            m_gameMusic.Stop();
            m_winMusic.Play();
        }
        else if (SceneManager.GetActiveScene().name == "LoseMenu")
        {
            m_gameMusic.Stop();
            m_loseMusic.Play();
        }
        else if (!m_menuMusic.isPlaying)
        {
            m_gameMusic.Stop();
            m_menuMusic.Play();
        }
    }
}
