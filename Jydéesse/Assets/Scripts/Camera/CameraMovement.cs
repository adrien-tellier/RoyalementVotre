using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float m_transitionDuration = 1f;

    [SerializeField]
    private Vector3[] m_positions = null;

    [SerializeField]
    private uint m_startPosition = 1;

    [SerializeField]
    private Player m_player;

    private uint m_currentPosition = 1;
    private uint m_previousPosition = 1;
    private Coroutine m_transition = null;
    private bool m_isTransitioning = false;

    public UnityEvent e_goRight;
    public UnityEvent e_goLeft;

    private void Awake()
    {
        transform.position = m_positions[m_startPosition];
        m_currentPosition = m_startPosition;
    }

    public void MoveLeft()
    {
        if (m_currentPosition > 0 && !m_isTransitioning && (!m_player.getOccupiedStatus() && !m_player.IsMoving) || m_player.IsOnQuest)
        {
            m_previousPosition = m_currentPosition;
            m_currentPosition--;
            if (m_transition != null)
                StopCoroutine(m_transition);
            m_transition = StartCoroutine(CameraPositionTransition());
            e_goLeft.Invoke();
        }
    }

    public void MoveRight()
    {
        if (m_currentPosition < m_positions.Length - 1 && !m_isTransitioning && (!m_player.getOccupiedStatus() && !m_player.IsMoving) || m_player.IsOnQuest)
        {
            m_previousPosition = m_currentPosition;
            m_currentPosition++;
            if (m_transition != null)
                StopCoroutine(m_transition);
            m_transition = StartCoroutine(CameraPositionTransition());
            e_goRight.Invoke();
        }
    }

    IEnumerator CameraPositionTransition()
    {
        m_isTransitioning = true;
        float duration = 0f;
        while(Vector3.Distance(transform.position, m_positions[m_currentPosition]) > 0.1f)
        {
            transform.position = Vector3.Slerp(m_positions[m_previousPosition], m_positions[m_currentPosition], duration / m_transitionDuration);
            duration += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.position = m_positions[m_currentPosition];
        m_isTransitioning = false;
    }
}
