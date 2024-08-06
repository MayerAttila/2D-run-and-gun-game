using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceeneChange : MonoBehaviour
{
    static public bool isFullScrean;
    static public int rezIndex = -1;
    bool isTimerStarted = false;
    float timer = 0;
    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SceenSwitch") 
        {


            if (SceneManager.GetActiveScene().name == "Shop")
            {
                SavePlayerDatas.SavePlayerData(gameObject.GetComponent<shooting>().arrowCount, gameObject.GetComponent<playerHealth>().health, gameObject.GetComponent<itemPickUp>().currenMoney, gameObject.GetComponent<playerHealth>().bandages.Count, true);
                SceneChange("DayCount");

            }
            else if (SceneManager.GetActiveScene().name == "Tutorial") SceneChange("MainMenu");
            else 
            {
                SavePlayerDatas.SavePlayerData(gameObject.GetComponent<shooting>().arrowCount, gameObject.GetComponent<playerHealth>().health, gameObject.GetComponent<itemPickUp>().currenMoney, gameObject.GetComponent<playerHealth>().bandages.Count, false);
                GameSave.SaveGame(SavePlayerDatas.mainCharacterTMP);
                SceneChange("Shop");
            } 

        }
    }



    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "DayCount") 
        {
            Debug.Log(SavePlayerDatas.mainCharacterTMP.currentHp);
            if (SavePlayerDatas.mainCharacterTMP.currentHp > 0)
            {
                gameObject.GetComponentInChildren<Text>().text = "Day: " + SavePlayerDatas.mainCharacterTMP.dayCount;
                isTimerStarted = true;
            }
            else 
            {
                gameObject.GetComponentInChildren<Text>().text = "U have DIED at day " + SavePlayerDatas.mainCharacterTMP.dayCount;
                GameSave.SaveGame(SavePlayerDatas.mainCharacterTMP);
                isTimerStarted = true;
            }
            
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (isTimerStarted) 
        {
            timer += Time.deltaTime;
            if (timer > 2f) 
            {
                if(SavePlayerDatas.mainCharacterTMP.currentHp > 0) SceneChange("Game");
                else SceneChange("MainMenu");
            } 
            
        }
    }
}
