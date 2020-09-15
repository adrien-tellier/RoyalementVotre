using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpecialRequestEvent : Event
{
    // Display thingies
    [SerializeField]
    private string m_acceptText = "";
    [SerializeField]
    private string m_declineText = "";
    [SerializeField]
    private string m_acceptComebackText = "";
    [SerializeField]
    private string m_declineComebackText = "";

    [SerializeField]
    private Button m_acceptButton = null;

    [SerializeField]
    private Button m_declineButton = null;
    
    // Effect of accepting/denying
    [SerializeField]
    private Effect m_acceptEffect = null; 

    [SerializeField]
    private Effect m_declineEffect = null; 

    [SerializeField]
    private bool m_isRequestCoin;


    // Called when the player clicks on the character
    protected new void OnMouseDown() 
    {   
        if (Time.timeScale == 0)
            return;
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
        m_chara.PlaySatisfiedSound();
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

        m_chara.PlaySatisfiedSound();

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

        m_chara.PlaySatisfiedSound();

        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(m_comebackDialogue);
        yield return null;
    }



    // Called if the player accept the request
    private void Accepted()
    {
        m_comebackDialogue = m_acceptComebackText;


        if (m_acceptEffect.m_player == null)
                m_acceptEffect.m_player = m_player;
        m_acceptEffect.affectPlayer();

        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(m_acceptText);

        if (m_isRequestCoin)
            m_eventSoundManager.PlayCoinLoss();
        else
            m_eventSoundManager.PlayFoodLoss();
            
        m_chara.PlayHappySound();

        // Hide the buttons
        m_acceptButton.gameObject.SetActive(false);
        m_declineButton.gameObject.SetActive(false);

        CloseEvent();

        m_status = EStatus.DONE;
    }

    // Called if the player declines the request
    private void Declined()
    {
        m_comebackDialogue = m_declineComebackText;

        // Punish the player
        if (m_declineEffect.m_player == null)
                m_declineEffect.m_player = m_player;
        m_declineEffect.affectPlayer();

        m_chara.PlayDisatisfiedSound();
        
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
        m_acceptButton.onClick.RemoveAllListeners();
        m_declineButton.onClick.RemoveAllListeners();
        
        // Let the player interact again
        m_player.setOccupiedStatus(false);
    }
}
