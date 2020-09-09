using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LevelTransitionManager : MonoBehaviour
{
    [SerializeField]
    private float m_transitionDuration = 1;


    private Image m_fadeImage;
    private bool m_isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        m_fadeImage = GetComponentInChildren<Image>();
        m_fadeImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) && !m_isTransitioning)
            StartCoroutine(TransisitionLevelFadeToBlack(1));
        if (Input.GetKeyDown(KeyCode.Keypad0) && !m_isTransitioning)
            StartCoroutine(TransisitionLevelFadeToBlack(0));
    }

    public void ChangeLevel(int sceneID)
    {
        StartCoroutine(TransisitionLevelFadeToBlack(sceneID));
    }

    public void QuitGame()
    {
        StartCoroutine(QuitFadeToBlack());
    }

    IEnumerator TransisitionLevelFadeToBlack(int sceneID)
    {
        m_isTransitioning = true;
        m_fadeImage.enabled = true;
        float duration = 0;
        while (duration < m_transitionDuration * 0.5f)
        {
            m_fadeImage.color = new Color(0, 0, 0, duration / (m_transitionDuration * 0.5f));
            duration += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(sceneID);
        while (duration > 0f)
        {
            m_fadeImage.color = new Color(0, 0, 0, duration / (m_transitionDuration * 0.5f));
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_fadeImage.enabled = false;
        m_isTransitioning = false;
    }

    IEnumerator QuitFadeToBlack()
    {
        m_isTransitioning = true;
        m_fadeImage.enabled = true;
        float duration = 0;
        while (duration < m_transitionDuration * 0.5f)
        {
            m_fadeImage.color = new Color(0, 0, 0, duration / (m_transitionDuration * 0.5f));
            duration += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
