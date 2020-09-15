using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EAnimalType 
{
    SHEEP,
    COW
}

public class AnimalDialogue : MonoBehaviour
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
    private EAnimalType m_type;

    private AnimalSound m_animalSound;

    private void Start() 
    {
        m_animalSound = GetComponentInParent<AnimalSound>();
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

    protected void OnMouseDown() 
    {   
        if (Time.timeScale == 0)
            return;
        Vector3 position = transform.position;

        if (position.y < m_player.m_minY)
                position.y = m_player.m_minY;

        if (position.y > m_player.m_maxY)
                position.y = m_player.m_maxY;

        m_player.Destination = position;

        // Do nothing if the player is already occupied
        if (m_player.getOccupiedStatus())
        {
            if (m_player.IsOnQuest)
                StartCoroutine("OnQuestDialogueWhenArrived");
            return;
        }

        // Starts the event
        else
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
        

        m_animalSound.PlayAnimalSound(m_type);
        ChangeSpeaker(m_eventHolderSpeakerName);
        m_mainTextBox.text = m_startDialogue;   
        m_player.setOccupiedStatus(false);

        yield return null;
    }

    // Start the event once the player is next to the character
    IEnumerator OnQuestDialogueWhenArrived()
    {
        // Wait for the player to be next to the character
        while (m_player.IsMoving || Vector3.Distance(m_player.transform.position, transform.position) >= 5)
        {
            yield return new WaitForSeconds(.01f);
        }

        // Prompt the event and the buttons
        ChangeSpeaker(m_eventHolderSpeakerName);
        m_mainTextBox.text = m_startDialogue;   

        ChangeSpeaker("Le Roi");
        DisplayDialogue("Je dois trouver " + m_player.m_actionTargetName);
        m_player.setOccupiedStatus(false);

        yield return null;
    }
}
