using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Action : MonoBehaviour
{
    [SerializeField]
    private Player m_player = null;

    [SerializeField]
    public string m_onClickDialogue = "";

    private bool m_isAvailable = false;

    // If the target object has to be picked up
    [SerializeField]
    private bool m_shouldDisapear = false;

    public UnityEvent m_requestDone = null;

    public bool IsAvailable { get { return m_isAvailable; } set { m_isAvailable = value; }}

    private void OnMouseDown() 
    {
        if (m_isAvailable)
        {
            m_player.Destination = transform.position.x;
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
