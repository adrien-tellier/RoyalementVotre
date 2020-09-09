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
    public string m_startDialogue = "";

    protected string m_endDialogue = "";
    protected string m_comebackDialogue = "";

    // not available, available, done
    protected EStatus m_status = EStatus.AVAILABLE;

    [SerializeField]
    private Text m_dialogueBoxText = null;

    [SerializeField]
    protected Player m_player = null;

    public void SetDialogue(string endDialogue)
    {
        m_endDialogue = endDialogue;
    }

    protected void OnMouseDown() 
    {
        m_dialogueBoxText.text = m_startDialogue;    
    }

    protected void DisplayDialogue()
    {
        m_dialogueBoxText.text = m_endDialogue;
    }

    

}
