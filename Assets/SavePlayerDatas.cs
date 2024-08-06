using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerDatas : MonoBehaviour
{
    [System.Serializable]
    public class player 
    {
        public int strenght;
        public int health;
        public int speed;
        public int inteligence;

        public string name;
        public int dayCount;
        public int maxHp;
        public int currentHp;
        public int maxArrowCount;
        public int currentArrowCount;
        public int money;
        public int maxBandageCount;
        public int currentBandageCount;
        public int maxShotPower;
        public int maxStamina;
        public int addedSprintSpeed;
        public float staminaRegen;
        public float staminaConsumtion;
        public float sprintBoost;
        public float movementSpeed;
        public int bandageHealBoost;
        public int lootBoost;
        public int arrowUpgradeStage;
        public int meleeUpgradeStage;
        public float reducedPicklockTime;
        public float lootTime;


        public player(int strenght, int health, int speed, int inteligence) 
        {
            this.strenght = strenght;
            this.health = health;
            this.speed = speed;
            this.inteligence = inteligence;



            maxArrowCount = 10 + strenght * 3;
            maxBandageCount = 3 + strenght;
            maxShotPower = 50 + strenght * 10;

            maxHp = 50 + health * 10;
            maxStamina = 50 + health * 10;
            staminaRegen = 0.015f * health;

            staminaConsumtion = 0.2f - (float)speed / 100;
            sprintBoost = 0.5f * speed;
            movementSpeed = speed;

            lootTime = 2f - 0.2f * inteligence;
            lootBoost = inteligence;
            bandageHealBoost = inteligence * 2;


            dayCount = 0;
            currentHp = maxHp;
            currentArrowCount = maxArrowCount;
            money = 0;
            currentBandageCount = maxBandageCount;
            arrowUpgradeStage = 0;
            meleeUpgradeStage = 0;
            reducedPicklockTime = -0.2f * meleeUpgradeStage;
        }
    }

    public static player mainCharacterTMP = new player(5,5,5,5);

    public void CreatePlayer() 
    {
        List<int> tmp = gameObject.GetComponent<SkillpointCounter>().playerStarts;
        mainCharacterTMP = new player(tmp[0],tmp[1],tmp[2],tmp[3]);
    }

    public static void SavePlayerData(int arrowCount, int health, int currentMoney, int currentBandageCount, bool addToDayCount)
    {
        mainCharacterTMP.currentArrowCount = arrowCount;
        mainCharacterTMP.currentHp = health;
        mainCharacterTMP.money = currentMoney;
        mainCharacterTMP.currentBandageCount = currentBandageCount;
        if (addToDayCount) mainCharacterTMP.dayCount++;
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
