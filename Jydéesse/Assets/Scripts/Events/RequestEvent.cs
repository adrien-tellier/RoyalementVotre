using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RequestEvent : Event
{
    [SerializeField]
    private string m_acceptText;
    [SerializeField]
    private string m_declineText;
    [SerializeField]
    private string m_requestDoneText;

    [SerializeField]
    private Action m_action; 

    [SerializeField]
    private Button m_acceptButton;

    [SerializeField]
    private Button m_declineButton;

    [SerializeField]
    private Effect acceptEffect; 

    [SerializeField]
    private Effect declineEffect; 

    private void Start() 
    {
        m_action.m_requestDone.AddListener(RequestCompleted);
    }

    private void RequestCompleted()
    {
        m_status = EStatus.DONE;
    }

    protected new void OnMouseDown() 
    {   
        // Do nothing if the player is already occupied
        if (m_status == EStatus.DONE)
        {
            StartCoroutine("DisplayCombackDialogueWhenArrived");
            return;
        }

        else if (m_player.getOccupiedStatus() || m_status != EStatus.AVAILABLE)
            return;

        m_player.setOccupiedStatus(true);
        StartCoroutine("BeginDialogueWhenArrived");
    }

    IEnumerator BeginDialogueWhenArrived()
    {
        while (m_player.IsMoving)
        {
            yield return new WaitForSeconds(.01f);
        }
        // Prompt the event and choices texts
        base.OnMouseDown();

        m_acceptButton.gameObject.SetActive(true);
        m_declineButton.gameObject.SetActive(true);

        m_acceptButton.onClick.AddListener(Accepted);

        yield return null;
    }

    IEnumerator DisplayCombackDialogueWhenArrived()
    {
        while (m_player.IsMoving)
        {
            yield return new WaitForSeconds(.01f);
        }
        SetDialogue(m_requestDoneText);
        DisplayDialogue();
        yield return null;
    }

    private void Accepted()
    {
        m_action.m_isAvailable = true;
        SetDialogue(m_acceptText);
        DisplayDialogue();
    }

    private void Declined()
    {
        SetDialogue(m_declineText);
        DisplayDialogue();
        CloseEvent();
        
    }
    private void CloseEvent()
    {
        
        m_acceptButton.gameObject.SetActive(false);
        m_declineButton.gameObject.SetActive(false);

        SetDialogue(m_endDialogue);
        DisplayDialogue();

        // Remove the buttons listeners
        m_acceptButton.onClick.RemoveAllListeners();
        m_declineButton.onClick.RemoveAllListeners();
        
        // Let the player interact again
        m_player.setOccupiedStatus(false);

        m_status = EStatus.DONE;
    }
}
