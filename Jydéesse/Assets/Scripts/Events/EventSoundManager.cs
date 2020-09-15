using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_coinLoss;
    [SerializeField]
    private AudioSource m_foodLoss;
    
    public void PlayCoinLoss()
    {
        m_coinLoss.Play();
    }
    public void PlayFoodLoss()
    {
        m_foodLoss.Play();
    }
}
