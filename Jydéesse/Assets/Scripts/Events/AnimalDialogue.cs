using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDialogue : Event
{
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
        
        base.OnMouseDown();
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
        base.OnMouseDown();

        ChangeSpeaker("Le Roi");
        DisplayDialogue("Je dois trouver " + m_player.m_actionTargetName);
        m_player.setOccupiedStatus(false);

        yield return null;
    }
}
