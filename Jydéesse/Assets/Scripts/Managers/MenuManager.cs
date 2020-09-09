using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        LevelTransitionManager lvlMgr = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelTransitionManager>();
        buttons[0].onClick.AddListener( () => { lvlMgr.ChangeLevel(1); } );
        buttons[3].onClick.AddListener( () => { lvlMgr.QuitGame(); } );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
