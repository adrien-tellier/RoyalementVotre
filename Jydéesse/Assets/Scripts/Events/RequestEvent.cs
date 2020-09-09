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
    private string m_thanksText;
    [SerializeField]
    private string m_gotDeclinedText;
    [SerializeField]
    private string m_requestDoneText;

    [SerializeField]
    private Action m_action; 

    [SerializeField]
    private Button m_acceptButton;

    [SerializeField]
    private Button m_declineButton;

    [SerializeField]
    private Effect m_acceptEffect; 

    [SerializeField]
    private Effect m_declineEffect; 

    private void Start() 
    {
        m_action.m_requestDone.AddListener(RequestCompleted);
    }

    private void RequestCompleted()
    {
        m_comebackDialogue = m_thanksText;
        SetDialogue(m_action.m_onClickDialogue);
        m_status = EStatus.REQUEST_DONE;
        DisplayDialogue();
    }

    protected new void OnMouseDown() 
    {   
        if (m_status == EStatus.REQUEST_DONE)
        {
            StartCoroutine("DisplayRequestDoneDialogueWhenArrived");
            m_status = EStatus.DONE;
            m_player.IsOnQuest = false;
            CloseEvent();
            return;
        }

        // Do nothing if the player is already occupied
        if (m_player.getOccupiedStatus())
            return;

        else if (m_status == EStatus.DONE)
        {
            StartCoroutine("DisplayCombackDialogueWhenArrived");
        }

        else if (m_status == EStatus.AVAILABLE)
        {
            m_player.setOccupiedStatus(true);
            StartCoroutine("BeginDialogueWhenArrived");
        }
    }

    IEnumerator BeginDialogueWhenArrived()
    {
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
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
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
        {
            yield return new WaitForSeconds(.01f);
        }
        SetDialogue(m_comebackDialogue);
        DisplayDialogue();
        yield return null;
    }
    
    IEnumerator DisplayRequestDoneDialogueWhenArrived()
    {
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
        {
            yield return new WaitForSeconds(.01f);
        }
        SetDialogue(m_requestDoneText);
        DisplayDialogue();
        if (m_acceptEffect.m_player == null)
                m_acceptEffect.m_player = m_player;
                
            m_acceptEffect.affectPlayer();
        yield return null;
    }

    private void Accepted()
    {
        m_action.m_isAvailable = true;
        SetDialogue(m_acceptText);
        DisplayDialogue();
        m_acceptButton.gameObject.SetActive(false);
        m_declineButton.gameObject.SetActive(false);
        m_status = EStatus.ON_REQUEST;
        m_player.IsOnQuest = true;
    }

    private void Declined()
    {
        SetDialogue(m_declineText);
        DisplayDialogue();
        CloseEvent();
        if (m_declineEffect.m_player == null)
                m_declineEffect.m_player = m_player;
        m_declineEffect.affectPlayer();
        m_comebackDialogue = m_gotDeclinedText;
        
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
