using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    public Sprite m_typeSprite;

    [SerializeField]
    public Sprite m_timeSprite;

    public void SwitchToTimeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = m_timeSprite;
    }

    public void SwitchToTypeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = m_typeSprite;
    }
}
