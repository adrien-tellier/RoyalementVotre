using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EStatus
{
    NOT_AVAILABLE,
    AVAILABLE,
    ON_REQUEST,
    REQUEST_DONE,
    DONE
}

public class Event : MonoBehaviour
{
    // How the event is displayed
    [SerializeField]
    protected string m_startDialogue = ""; // what appears when the event is launched

    protected string m_comebackDialogue = ""; // what appears when the player come back to the character

    [SerializeField]
    private TextMeshProUGUI m_mainTextBox = null;

    [SerializeField]
    private TextMeshProUGUI m_speakerTextBox = null;

    [SerializeField]
    private TextMeshProUGUI m_speakerTextBox2 = null; // les deux point 
    
    [SerializeField]
    protected string m_eventHolderSpeakerName = "";

    protected EStatus m_status = EStatus.AVAILABLE;

    [SerializeField]
    protected Player m_player = null;

    [SerializeField]
    protected bool m_canComeAgain;

    [SerializeField]
    protected Character m_chara;

    [SerializeField]
    protected Bubble m_bubble;

    protected EventSoundManager m_eventSoundManager;

    protected void Start() 
    {
        m_eventSoundManager = GetComponentInParent<EventSoundManager>();
    }

    // When the player arrives next to the character, displays the startDialogue
    protected void OnMouseDown() 
    {
        ChangeSpeaker(m_eventHolderSpeakerName);
        m_mainTextBox.text = m_startDialogue;    
    }

    // Write the given text in the dialogue box
    protected void DisplayDialogue(string text)
    {
        m_mainTextBox.text = text;
    }

    protected void ChangeSpeaker(string name)
    {
        m_speakerTextBox.text = name;
        m_speakerTextBox2.gameObject.SetActive(true);
    }

    private void FixedUpdate() {
        if (m_status == EStatus.AVAILABLE && (!m_player.getOccupiedStatus() || (m_player.IsMoving && !m_player.IsOnQuest)))
            m_bubble.gameObject.SetActive(true);
        else
            m_bubble.gameObject.SetActive(false);
    }

    private void OnMouseOver() 
    {
        m_bubble.SwitchToTimeSprite();
    }

    private void OnMouseExit()
    {
        m_bubble.SwitchToTypeSprite();
    }


}
