
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ChoiceEvent : Event
{
    [SerializeField]
    protected Choice[] choices = new Choice[3];

    [SerializeField]
    public Button m_foodButton = null;
    [SerializeField]
    public Button m_moneyButton = null;
    [SerializeField]
    public Button m_declineButton = null;

    protected void Start() 
    {
        // Setup choices
        foreach (Choice c in choices)
        {
            c.m_player = m_player;
        }
    }

    private void CloseEvent()
    {
        // deactivate the choices
        foreach (Choice c in choices)
        {
            c.IsActive = false;
        }

        // Empty the text boxes
        m_foodButton.GetComponentInChildren<Text>().text = "";
        m_foodButton.gameObject.SetActive(false);
        m_moneyButton.GetComponentInChildren<Text>().text = "";
        m_moneyButton.gameObject.SetActive(false);
        m_declineButton.GetComponentInChildren<Text>().text = "";
        m_declineButton.gameObject.SetActive(false);

        // Remove the buttons listeners
        m_foodButton.onClick.RemoveListener(choices[0].affectPlayer);
        m_foodButton.onClick.RemoveListener(FoodOptionChosen);
        m_moneyButton.onClick.RemoveListener(choices[1].affectPlayer);
        m_moneyButton.onClick.RemoveListener(MoneyOptionChosen);
        m_declineButton.onClick.RemoveListener(choices[2].affectPlayer);
        m_declineButton.onClick.RemoveListener(DeclineChosen);
        
        // Let the player interact again
        m_player.setOccupiedStatus(false);
    }

    protected new void OnMouseDown() 
    {   
        // Do nothing if the player is already occupied
        if (m_player.getOccupiedStatus())
        {
            if (m_player.IsOnQuest)
                StartCoroutine("OnQuestDialogueWhenArrived");

            return;
        }

        if (m_status == EStatus.DONE)
            StartCoroutine("DisplayCombackDialogueWhenArrived");

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
        m_chara.PlaySatisfiedSound();

        m_foodButton.gameObject.SetActive(true);
        m_foodButton.GetComponentInChildren<Text>().text = choices[0].m_prompt;
        m_moneyButton.gameObject.SetActive(true);
        m_moneyButton.GetComponentInChildren<Text>().text = choices[1].m_prompt;
        m_declineButton.gameObject.SetActive(true);
        m_declineButton.GetComponentInChildren<Text>().text = choices[2].m_prompt;

        // Link button to choice's effect
        m_foodButton.onClick.AddListener(choices[0].affectPlayer);
        m_foodButton.onClick.AddListener(FoodOptionChosen);
        m_moneyButton.onClick.AddListener(choices[1].affectPlayer);
        m_moneyButton.onClick.AddListener(MoneyOptionChosen);
        m_declineButton.onClick.AddListener(choices[2].affectPlayer);
        m_declineButton.onClick.AddListener(DeclineChosen);

        // Active choices
        foreach (Choice c in choices)
            c.IsActive = true;

        yield return null;
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

    IEnumerator DisplayCombackDialogueWhenArrived()
    {
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
        {
            yield return new WaitForSeconds(.01f);
        }
        
        m_chara.PlaySatisfiedSound();
        
        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(m_comebackDialogue);
        yield return null;
    }

    // Je sais c'est moche mais y pas le temps
    private void FoodOptionChosen()
    {
        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(choices[0].m_endPrompt);
        m_chara.PlayHappySound();

        m_comebackDialogue = choices[0].m_comebackMessage;
        CloseEvent();
        m_status = EStatus.DONE;
    }
    private void MoneyOptionChosen()
    {
        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(choices[1].m_endPrompt);
        m_chara.PlayHappySound();

        m_comebackDialogue = choices[1].m_comebackMessage;
        CloseEvent();
        m_status = EStatus.DONE;
    }
    private void DeclineChosen()
    {
        ChangeSpeaker(m_eventHolderSpeakerName);
        DisplayDialogue(choices[2].m_endPrompt);

        m_chara.PlayDisatisfiedSound();

        m_comebackDialogue = choices[2].m_comebackMessage;

        CloseEvent();
        if (!m_canComeAgain)
            m_status = EStatus.DONE;
    }

}

