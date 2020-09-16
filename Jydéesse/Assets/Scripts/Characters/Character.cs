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

[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    [SerializeField]
    private Player m_player = null;

    [SerializeField]
    public ECharacterType m_type;

    private CharacterPool m_pool;

    private void Start() 
    {
        m_pool = GetComponentInParent<CharacterPool>();   
    }

    private Animator m_animator = null;

    private void Awake()
    {
        m_animator = gameObject.GetComponent<Animator>();
    }

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
            m_animator.SetBool("IsRight", true);
        else
            m_animator.SetBool("IsRight", false);
    }

    public void PlaySatisfiedSound()
    {
        m_pool.PlaySatisfiedSound(m_type);
    }

    public void PlayDisatisfiedSound()
    {
        m_pool.PlayDisatisfiedSound(m_type);
    }

    public void PlayHappySound()
    {
        m_pool.PlayHappySound(m_type);
    }
}
