using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillpointCounter : MonoBehaviour
{
    public int skillPoints = 10;

    public List<Slider> sliders = new List<Slider>();
    public List<int> playerStarts = new List<int>();

    public Button startBtn;


    public void RefreshText()
    {
        gameObject.GetComponent<Text>().text = "Skillpoints: " + skillPoints.ToString();
    }

    public void SavePlayerStart() 
    {
        for (int i = 0; i < 4; i++)
        {
            playerStarts.Add((int)sliders[i].value);

            Debug.Log(playerStarts[i]);
        }
    }

    private void Update()
    {
        if (skillPoints == 0) startBtn.gameObject.SetActive(true);
        else startBtn.gameObject.SetActive(false);
    }
}
