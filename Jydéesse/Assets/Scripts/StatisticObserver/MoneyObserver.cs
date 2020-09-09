using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyObserver : MonoBehaviour
{
    [SerializeField]
    private Player m_player;

    [SerializeField]
    private Text m_infoText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
        m_player.e_moneyChanged.AddListener(UpdateDisplay);
    }

    // Update is called once per frame
    void UpdateDisplay()
    {
        m_infoText.text = "Money : \n"+m_player.getMoney();
    }
}
