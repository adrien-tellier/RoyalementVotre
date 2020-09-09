using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Action : MonoBehaviour
{
    public bool m_isAvailable = false;

    [SerializeField]
    private Player m_player = null;

    public UnityEvent m_requestDone = null;

    [SerializeField]
    public string m_onClickDialogue = "";

    private void OnMouseDown() 
    {
        if (m_isAvailable)
        {
            m_player.Destination = transform.position;
            StartCoroutine("CompleteTaskWhenArrived");
        }
    }

    IEnumerator CompleteTaskWhenArrived()
    {
        while (m_player.IsMoving)
        {
            yield return new WaitForSeconds(.01f);
        }
        m_requestDone.Invoke();
        yield return null;
    }
}
