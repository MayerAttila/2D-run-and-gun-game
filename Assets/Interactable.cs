using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public List<int> allLoot = new List<int>();

    public bool isHideable;
    public bool isLooted;
    public bool isLocked = false;
    public bool isLockedRandom;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLooted)
        {
            allLoot.Add(Random.Range(0 + SavePlayerDatas.mainCharacterTMP.lootBoost, 2 + SavePlayerDatas.mainCharacterTMP.lootBoost));
            allLoot.Add(Random.Range(0 + SavePlayerDatas.mainCharacterTMP.lootBoost, 2 + SavePlayerDatas.mainCharacterTMP.lootBoost));
        }
        if (isLockedRandom) isLocked = Random.Range(0, 2) == 0;

        
    }

    public void GetData() 
    {
        Debug.Log(isHideable + " " +isLocked + " " + isLooted);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
