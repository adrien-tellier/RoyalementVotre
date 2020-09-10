using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPool : MonoBehaviour
{
    [SerializeField]
    private Player m_player = null;

    private Character[] m_characters;

    // Start is called before the first frame update
    void Start()
    {
        m_characters = gameObject.GetComponentsInChildren<Character>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
