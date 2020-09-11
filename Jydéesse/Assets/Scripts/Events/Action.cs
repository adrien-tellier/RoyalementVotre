using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Action : MonoBehaviour
{
    [SerializeField]
    private Player m_player = null;

    [SerializeField]
    public string m_actionTargetName = "";

    private bool m_isAvailable = false;

    // If the target object has to be picked up
    [SerializeField]
    private bool m_shouldDisapear = false;

    public UnityEvent m_requestDone = null;

    public bool IsAvailable { get { return m_isAvailable; } set { m_isAvailable = value; }}

    private void OnMouseDown() 
    {
        if (Time.timeScale == 0)
            return;
            
        if (m_isAvailable)
        {
            Vector3 position = transform.position;

            if (position.y < m_player.m_minY)
                position.y = m_player.m_minY;

            if (position.y > m_player.m_maxY)
                position.y = m_player.m_maxY;

            m_player.Destination = position;
            StartCoroutine("CompleteTaskWhenArrived");
        }
    }

    IEnumerator CompleteTaskWhenArrived()
    {
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
        {
            yield return new WaitForSeconds(.01f);
        }
        m_requestDone.Invoke();

        if (m_shouldDisapear)
            gameObject.SetActive(false);
            
        yield return null;
    }
}
