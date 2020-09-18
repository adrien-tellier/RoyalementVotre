using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelTransitionManager))]
public class SplashScreen : MonoBehaviour
{

    [SerializeField]
    private float m_duration = 2f;

    private LevelTransitionManager m_lvlTransitionMgr = null;

    private void Awake()
    {
        m_lvlTransitionMgr = gameObject.GetComponent<LevelTransitionManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= m_duration)
            m_lvlTransitionMgr.ChangeLevel("MainMenu");
    }
}
