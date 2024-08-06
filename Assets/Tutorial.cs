using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text TutorialText;
    List<string> TutorialListStrings = new List<string>();
    int tutorialCheckList = 0;
    public List<GameObject> Block;
    public enemy enemy;
    GameObject g;


    

    void UpdateChecklist(int i) 
    {
        tutorialCheckList = i;
        TutorialText.text = TutorialListStrings[tutorialCheckList];
        if (tutorialCheckList == 2) Destroy(Block[0]);
        if (tutorialCheckList == 4) Destroy(Block[1]);
    }

    void Start()
    {
        TutorialListStrings.Add("Press Q to Switch to your secondary weapon");
        TutorialListStrings.Add("When your secondary weapon is active u can use bandages by holding the right click");

        TutorialListStrings.Add("Move to the nearest object! U can move Your charecter with A and D buttons and by holding shift u can sprint");
        TutorialListStrings.Add("By holding E u can interact with buildings. If a building is locked u can picklock it with your secondoary weapon");

        TutorialListStrings.Add("U can shoot a enemy by activating your primary weapon and holding then relesing left click");
        TutorialListStrings.Add("U can pick up your arrows by pressing E near them after u done move to the exit -->");

        TutorialText.text = TutorialListStrings[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        g = collision.gameObject;
        if (collision.tag == "Interactable" && tutorialCheckList < 3) UpdateChecklist(3);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        g = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && tutorialCheckList == 0) UpdateChecklist(1);
        if (SavePlayerDatas.mainCharacterTMP.maxBandageCount > gameObject.GetComponent<playerHealth>().bandages.Count && tutorialCheckList < 2) UpdateChecklist(2);
        if (Input.GetKeyDown(KeyCode.E) && g != null) UpdateChecklist(4);
        if (!enemy.isAlive) UpdateChecklist(5);
    }




}
