using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Choice", menuName = "ScriptableObjects/Choice", order = 1)]
public class Choice : ScriptableObject
{
    public Player m_player = null;

    public string m_prompt = "";

    public string m_endPrompt = "";

    public string m_comebackMessage = "";
    
    private bool m_isActive = false;
    public UnityEvent m_choiceMade;

    public Effect m_effect;

    public bool IsActive { get { return m_isActive; } set { m_isActive = value; }}

    public void affectPlayer() 
    {
        if (m_isActive)
        {
            if (m_effect.m_player == null)
                m_effect.m_player = m_player;

            m_effect.affectPlayer();
            m_choiceMade.Invoke();
        }
    }
}
