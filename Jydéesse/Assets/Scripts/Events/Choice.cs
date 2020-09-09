using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Choice", menuName = "ScriptableObjects/Choice", order = 1)]
public class Choice : ScriptableObject
{
    public string m_prompt = "";

    public string m_endPrompt = "";

    public string m_comebackMessage = "";

    public Player m_player = null;
    
    private bool isActive = false;
    public UnityEvent m_choiceMade;

    public Effect m_effect;


    // How the event affect the player's stats
    

    public void affectPlayer() 
    {
        if (isActive)
        {
            if (m_effect.m_player == null)
                m_effect.m_player = m_player;
            m_effect.affectPlayer();
        }
        m_choiceMade.Invoke();
    }

    public void setActive(bool b)
    {
        isActive = b;
    }
}
