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
        Debug.Log(m_characters.Length);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SendPositionToPlayer(Vector3 position)
    {
        Debug.Log(position);
        m_player.Destination = position;
    }
}
