using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodObserver : MonoBehaviour
{
    [SerializeField]
    private Player m_player = null;

    [SerializeField]
    private Text m_infoText = null;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
        m_player.e_foodChanged.AddListener(UpdateDisplay);
    }

    // Update is called once per frame
    void UpdateDisplay()
    {
        m_infoText.text = "Food : \n"+m_player.getFood();
    }
}
