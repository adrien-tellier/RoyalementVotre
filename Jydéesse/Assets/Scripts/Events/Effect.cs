using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Effect", order = 1)]
public class Effect : ScriptableObject
{
    // How the event affect the player's stats
    public float m_effectOnMoney = 0f;
    public float m_effectOnSatifaction = 0f;
    public float m_effectOnFood = 0f;
    public float m_effectOnTime = 0f;

    public Player m_player = null;

    public void affectPlayer() 
    {
        m_player.addMoney(m_effectOnMoney);
        m_player.addFood(m_effectOnFood);
        m_player.addSatifaction(m_effectOnSatifaction);
        m_player.addTime(m_effectOnTime);
    }
}
