using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialChoiceEvent : ChoiceEvent
{
    [SerializeField]
    private GameObject[] m_gameObjectToSpawn = new GameObject[2];

    public int m_triggerChoiceId;

    private new void Start() 
    {
        base.Start();
        choices[m_triggerChoiceId].m_choiceMade.AddListener(DoSpecialEvent);
    }

    private void DoSpecialEvent()
    {
        foreach (GameObject go in m_gameObjectToSpawn)
        {
            go.SetActive(true);
        }
    }
}
