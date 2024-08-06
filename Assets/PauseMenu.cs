using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Text MenuText;
    public GameObject Menu;
    public GameObject player;

    [SerializeField] bool isTimeFrozen = false;

    // Start is called before the first frame update
    void Start()
    {
        Menu.gameObject.SetActive(false);
    }

    void UpdateText() 
    {
        MenuText.text = "Survived days: " + SavePlayerDatas.mainCharacterTMP.dayCount + 
            "\n Current HP: " + player.GetComponent<playerHealth>().health +
            "\n Bandages left: " + player.GetComponent<playerHealth>().bandages.Count +
            "\n Arrows left: " + player.GetComponent<shooting>().arrowCount;
    }

    void freezAndUnfreezeTime() 
    {
        if (Menu.activeInHierarchy)
        {
            Time.timeScale = 0;
            isTimeFrozen = true;
        }
        else 
        {
            Time.timeScale = 1;
            isTimeFrozen = false;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Menu.gameObject.SetActive(!Menu.activeInHierarchy);
            freezAndUnfreezeTime();
            UpdateText();
        } 
        
    }
}
