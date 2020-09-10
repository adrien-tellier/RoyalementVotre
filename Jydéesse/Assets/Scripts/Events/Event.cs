using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Text m_dialogueBoxText = null;
    

    protected EStatus m_status = EStatus.AVAILABLE;

    [SerializeField]
    protected Player m_player = null;

    // When the player arrives next to the character, displays the startDialogue
    protected void OnMouseDown() 
    {
        m_dialogueBoxText.text = m_startDialogue;    
    }

    // Write the given text in the dialogue box
    protected void DisplayDialogue(string text)
    {
        m_dialogueBoxText.text = text;
    }

    

}
