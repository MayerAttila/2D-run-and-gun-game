using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int stage = SavePlayerDatas.mainCharacterTMP.meleeUpgradeStage;
    public float picklockTime = 3f - 0.2f * SavePlayerDatas.mainCharacterTMP.meleeUpgradeStage;


    // Start is called before the first frame update
    void Start()
    {
        stage = SavePlayerDatas.mainCharacterTMP.meleeUpgradeStage;
        picklockTime = 3f - 0.2f * SavePlayerDatas.mainCharacterTMP.meleeUpgradeStage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
