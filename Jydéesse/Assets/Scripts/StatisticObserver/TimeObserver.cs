using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeObserver : MonoBehaviour
{
    [SerializeField]
    private Player m_player = null;

    [SerializeField]
    private Text m_infoText = null;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
        m_player.e_timeChanged.AddListener(UpdateDisplay);
    }

    // Update is called once per frame
    void UpdateDisplay()
    {
        m_infoText.text = "Time : \n"+m_player.getTime();
    }
}
