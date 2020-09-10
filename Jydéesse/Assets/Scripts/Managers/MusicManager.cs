using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private AudioSource m_menuMusic = null;
    private AudioSource m_gameMusic = null;

    private void Awake()
    {
        AudioSource[] musics = GetComponentsInChildren<AudioSource>();
        m_menuMusic = musics[0];
        m_gameMusic = musics[1];
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
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            m_gameMusic.Play();
            m_menuMusic.Stop();
        }
        else if (!m_menuMusic.isPlaying)
        {
            m_menuMusic.Play();
            m_gameMusic.Stop();
        }
    }
}
