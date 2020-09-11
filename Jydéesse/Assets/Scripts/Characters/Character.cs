﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Player m_player = null;

    [SerializeField]
    private Sprite m_spriteLookLeft = null;

    [SerializeField]
    private Sprite m_spriteLookRght = null;

    private void OnMouseDown()
    {
        if (Time.timeScale != 0)
            gameObject.GetComponentInParent<CharacterPool>().SendPositionToPlayer(transform.position);
    }

    private void FixedUpdate() 
    {
        if (m_player.IsMoving)
            AdaptSide();
    }

    private void AdaptSide()
    {
        if (m_player.transform.position.x > transform.position.x)
            gameObject.GetComponent<SpriteRenderer>().sprite = m_spriteLookRght;

        if (m_player.transform.position.x < transform.position.x)
            gameObject.GetComponent<SpriteRenderer>().sprite = m_spriteLookLeft;
    }
}
