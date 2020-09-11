using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RequestEvent : Event
{
    // Display thingies
    [SerializeField]
    private string m_acceptText = "";
    [SerializeField]
    private string m_declineText = "";
    [SerializeField]
    private string m_thanksText = "";
    [SerializeField]
    private string m_gotDeclinedText = "";
    [SerializeField]
    private string m_requestDoneText = "";

    [SerializeField]
    private Button m_acceptButton = null;

    [SerializeField]
    private Button m_declineButton = null;

    // The trigger attached to the request (where the player should go)
    [SerializeField]
    private Action m_action = null; 
    
    // Effect of accepting/denying
    [SerializeField]
    private Effect m_acceptEffect = null; 

    [SerializeField]
    private Effect m_declineEffect = null; 


    private void Start() 
    {
        m_action.m_requestDone.AddListener(RequestCompleted);
    }

    // Called when the player went to the action position
    private void RequestCompleted()
    {
        m_comebackDialogue = m_thanksText;
        m_status = EStatus.REQUEST_DONE;

        DisplayDialogue("J'ai trouvé " + m_action.m_actionTargetName);
    }

    // Called when the player clicks on the character
    protected new void OnMouseDown() 
    {   
        // The player got back from request
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
        {
            if (m_player.IsOnQuest)
                StartCoroutine("OnQuestDialogueWhenArrived");
            return;
        }

        // The player came back to the character
        else if (m_status == EStatus.DONE)
        {
            StartCoroutine("DisplayCombackDialogueWhenArrived");
        }

        // Starts the event
        else if (m_status == EStatus.AVAILABLE)
        {
            m_player.setOccupiedStatus(true);
            StartCoroutine("BeginDialogueWhenArrived");
        }
    }

    IEnumerator OnQuestDialogueWhenArrived()
    {
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
        {
            yield return new WaitForSeconds(.01f);
        }
        ChangeSpeaker("Le Roi");
        DisplayDialogue("Je dois trouver " + m_player.m_actionTargetName);
        yield return null;
    }

    // Start the event once the player is next to the character
    IEnumerator BeginDialogueWhenArrived()
    {
        // Wait for the player to be next to the character
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
        {
            yield return new WaitForSeconds(.01f);
        }

        // Prompt the event and the buttons
        base.OnMouseDown();

        m_acceptButton.gameObject.SetActive(true);
        m_declineButton.gameObject.SetActive(true);

        m_acceptButton.onClick.AddListener(Accepted);
        m_declineButton.onClick.AddListener(Declined);

        yield return null;
    }

    // The player came back to the character
    IEnumerator DisplayCombackDialogueWhenArrived()
    {
        // Wait for the player to be next to the character
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
        {
            yield return new WaitForSeconds(.01f);
        }

        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(m_comebackDialogue);
        yield return null;
    }

    // The player finnished the request and came back
    IEnumerator DisplayRequestDoneDialogueWhenArrived()
    {
        // Wait for the player to be next to the character
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
        {
            yield return new WaitForSeconds(.01f);
        }

        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(m_requestDoneText);

        // Reward the player
        if (m_acceptEffect.m_player == null)
                m_acceptEffect.m_player = m_player;
                
        m_acceptEffect.affectPlayer();

        yield return null;
    }

    // Called if the player accept the request
    private void Accepted()
    {
        m_action.IsAvailable = true;
        m_player.IsOnQuest = true;
        m_player.m_actionTargetName = m_action.m_actionTargetName;
        m_status = EStatus.ON_REQUEST;

        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(m_acceptText);

        // Hide the buttons
        m_acceptButton.gameObject.SetActive(false);
        m_declineButton.gameObject.SetActive(false);
    }

    // Called if the player declines the request
    private void Declined()
    {
        m_comebackDialogue = m_gotDeclinedText;

        // Punish the player
        if (m_declineEffect.m_player == null)
                m_declineEffect.m_player = m_player;
        m_declineEffect.affectPlayer();
        
        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(m_declineText);
        CloseEvent();
        if (!m_canComeAgain)
            m_status = EStatus.DONE;
        
    }
    private void CloseEvent()
    {
        // Hides the buttons
        m_acceptButton.gameObject.SetActive(false);
        m_declineButton.gameObject.SetActive(false);

        // Remove the buttons listeners
        m_acceptButton.onClick.RemoveListener(Accepted);
        m_declineButton.onClick.RemoveListener(Declined);
        
        // Let the player interact again
        m_player.setOccupiedStatus(false);
    }
}
