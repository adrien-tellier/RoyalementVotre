
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceEvent : Event
{
    [SerializeField]
    Choice[] choices = new Choice[3];

    [SerializeField]
    public Button m_option1Button;
    [SerializeField]
    public Button m_option2Button;
    [SerializeField]
    public Button m_declineButton;

    private void Start() 
    {
        // Setup choices
        foreach (Choice c in choices)
        {
            c.m_player = m_player;
            c.m_choiceMade.AddListener(CloseEvent);
        }
    }

    private void CloseEvent()
    {
        // deactivate the choices
        foreach (Choice c in choices)
        {
            c.setActive(false);
        }

        // Empty the text boxes
        m_option1Button.GetComponentInChildren<Text>().text = "";
        m_option1Button.gameObject.SetActive(false);
        m_option2Button.GetComponentInChildren<Text>().text = "";
        m_option2Button.gameObject.SetActive(false);
        m_declineButton.GetComponentInChildren<Text>().text = "";
        m_declineButton.gameObject.SetActive(false);

        SetDialogue(m_endDialogue);
        DisplayDialogue();

        // Remove the buttons listeners
        m_option1Button.onClick.RemoveAllListeners();
        m_option2Button.onClick.RemoveAllListeners();
        m_declineButton.onClick.RemoveAllListeners();
        
        // Let the player interact again
        m_player.setOccupiedStatus(false);

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

        m_option1Button.gameObject.SetActive(true);
        m_option1Button.GetComponentInChildren<Text>().text = choices[0].m_prompt;
        m_option2Button.gameObject.SetActive(true);
        m_option2Button.GetComponentInChildren<Text>().text = choices[1].m_prompt;
        m_declineButton.gameObject.SetActive(true);
        m_declineButton.GetComponentInChildren<Text>().text = choices[2].m_prompt;

        // Link button to choice's effect
        m_option1Button.onClick.AddListener(opt1Chosen);
        m_option1Button.onClick.AddListener(choices[0].affectPlayer);
        m_option2Button.onClick.AddListener(opt2Chosen);
        m_option2Button.onClick.AddListener(choices[1].affectPlayer);
        m_declineButton.onClick.AddListener(declineChosen);
        m_declineButton.onClick.AddListener(choices[2].affectPlayer);

        // Active choices
        foreach (Choice c in choices)
        {
            c.setActive(true);
        }

        yield return null;
    }

    IEnumerator DisplayCombackDialogueWhenArrived()
    {
        while (m_player.IsMoving)
        {
            yield return new WaitForSeconds(.01f);
        }
        SetDialogue(m_comebackDialogue);
        DisplayDialogue();
        yield return null;
    }

    // Je sais c'est moche mais y pas le temps
    private void opt1Chosen()
    {
        m_endDialogue = choices[0].m_endPrompt;
        m_comebackDialogue = choices[0].m_comebackMessage;
    }
    private void opt2Chosen()
    {
        m_endDialogue = choices[1].m_endPrompt;
        m_comebackDialogue = choices[1].m_comebackMessage;
    }
    private void declineChosen()
    {
        m_endDialogue = choices[2].m_endPrompt;
        m_comebackDialogue = choices[2].m_comebackMessage;
    }

}

