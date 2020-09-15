using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPool : MonoBehaviour
{
    [SerializeField]
    private Player m_player = null;

    private Character[] m_characters;

    [SerializeField]
    private AudioSource m_soldierSatisfied;
    [SerializeField]
    private AudioSource m_ladySatisfied;
    [SerializeField]
    private AudioSource m_peasantSatisfied;
    [SerializeField]
    private AudioSource m_catSatisfied;
    [SerializeField]
    private AudioSource m_soldierHappy;
    [SerializeField]
    private AudioSource m_ladyHappy;
    [SerializeField]
    private AudioSource m_peasantHappy;
    [SerializeField]
    private AudioSource m_catHappy;
    [SerializeField]
    private AudioSource m_soldierDisatisfied;
    [SerializeField]
    private AudioSource m_ladyDisatisfied;
    [SerializeField]
    private AudioSource m_peasantDisatisfied;
    [SerializeField]
    private AudioSource m_catDisatisfied;

    // Start is called before the first frame update
    void Start()
    {
        m_characters = gameObject.GetComponentsInChildren<Character>();
    }

    public void SendPositionToPlayer(Vector3 position)
    {
        if (!m_player.getOccupiedStatus() || m_player.IsOnQuest)
        { 
            if (position.y < m_player.m_minY)
                position.y = m_player.m_minY;

            if (position.y > m_player.m_maxY)
                position.y = m_player.m_maxY;

            m_player.Destination = position;
        }
    }

    public void PlaySatisfiedSound(ECharacterType type)
    {
        switch (type)
        {
            case ECharacterType.CAT : m_catSatisfied.Play(); break;
            case ECharacterType.LADY : m_ladySatisfied.Play(); break;
            case ECharacterType.SOLDIER : m_soldierSatisfied.Play(); break;
            case ECharacterType.PEASANT : m_peasantSatisfied.Play(); break;

            default : break;
        }
    }

    public void PlayHappySound(ECharacterType type)
    {
        switch (type)
        {
            case ECharacterType.CAT : m_catHappy.Play(); break;
            case ECharacterType.LADY : m_ladyHappy.Play(); break;
            case ECharacterType.SOLDIER : m_soldierHappy.Play(); break;
            case ECharacterType.PEASANT : m_peasantHappy.Play(); break;

            default : break;
        }
    }

    public void PlayDisatisfiedSound(ECharacterType type)
    {
        switch (type)
        {
            case ECharacterType.CAT : m_catDisatisfied.Play(); break;
            case ECharacterType.LADY : m_ladyDisatisfied.Play(); break;
            case ECharacterType.SOLDIER : m_soldierDisatisfied.Play(); break;
            case ECharacterType.PEASANT : m_peasantDisatisfied.Play(); break;

            default : break;
        }
    }
}
