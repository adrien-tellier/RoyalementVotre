using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterType
{
    CAT,
    LADY,
    SOLDIER,
    PEASANT
}

public class Character : MonoBehaviour
{
    [SerializeField]
    private Player m_player = null;

    [SerializeField]
    private Sprite m_spriteLookLeft = null;

    [SerializeField]
    private Sprite m_spriteLookRght = null;

    [SerializeField]
    public ECharacterType m_type;

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

    public void PlaySatisfiedSound()
    {
        switch (m_type)
        {
            case ECharacterType.CAT : m_catSatisfied.Play(); break;
            case ECharacterType.LADY : m_ladySatisfied.Play(); break;
            case ECharacterType.SOLDIER : m_soldierSatisfied.Play(); break;
            case ECharacterType.PEASANT : m_peasantSatisfied.Play(); break;

            default : break;
        }
    }

    public void PlayHappySound()
    {
        switch (m_type)
        {
            case ECharacterType.CAT : m_catHappy.Play(); break;
            case ECharacterType.LADY : m_ladyHappy.Play(); break;
            case ECharacterType.SOLDIER : m_soldierHappy.Play(); break;
            case ECharacterType.PEASANT : m_peasantHappy.Play(); break;

            default : break;
        }
    }

    public void PlayDisatisfiedSound()
    {
        switch (m_type)
        {
            case ECharacterType.CAT : m_catDisatisfied.Play(); break;
            case ECharacterType.LADY : m_ladyDisatisfied.Play(); break;
            case ECharacterType.SOLDIER : m_soldierDisatisfied.Play(); break;
            case ECharacterType.PEASANT : m_peasantDisatisfied.Play(); break;

            default : break;
        }
    }
}
