using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueBtnScript : MonoBehaviour
{
    SavePlayerDatas.player playerFromSave;

    // Start is called before the first frame update
    void Start()
    {
        playerFromSave = GameSave.LoadSave();
        if (playerFromSave == null) gameObject.SetActive(false);
    }

    public void LoadSaveToCharacker() 
    {
        SavePlayerDatas.mainCharacterTMP = playerFromSave;
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
