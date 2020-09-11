
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class MenuManager : MonoBehaviour
{
    private Canvas m_canvas = null;

    private void Awake()
    {
        m_canvas = gameObject.GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveAllButons(bool active)
    {
        Button[] buttons = m_canvas.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
            b.interactable = active;
        }
    }
}
