using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GuardEvent : Event
{
    // Display thingies
    [SerializeField]
    private string m_overRequiredAmountText = "";

    [SerializeField]
    private string m_comebackLaterText = "";

    [SerializeField]
    private Button m_acceptButton = null;

    [SerializeField]
    private Button m_declineButton = null;

    private void Start() 
    {
        m_player.e_satisfactionChanged.AddListener(AdaptMessageToSatifaction);
    }

    private void AdaptMessageToSatifaction()
    {
        if (m_player.getSatisfaction() > m_player.m_requiredSatisfaction)
            m_startDialogue = m_overRequiredAmountText;
    }

    // Called when the player clicks on the character
    protected new void OnMouseDown() 
    {   
        // Do nothing if the player is already occupied
        if (m_player.getOccupiedStatus())
        {
            if (m_player.IsOnQuest)
                StartCoroutine("OnQuestDialogueWhenArrived");
            return;
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

    // Called if the player accept the request
    private void Accepted()
    {
        // Hide the buttons
        m_acceptButton.gameObject.SetActive(false);
        m_declineButton.gameObject.SetActive(false);

        m_status = EStatus.DONE;
        EndGame();

    }

    // Called if the player declines the request
    private void Declined()
    {
        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(m_comebackLaterText);

        CloseEvent();
        m_chara.PlayHappySound();
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

    private void EndGame()
    {
        if (m_player.getSatisfaction() > m_player.m_requiredSatisfaction)
            SceneManager.LoadScene("WinMenu");
        else
            SceneManager.LoadScene("LoseMenu");
    }
}
