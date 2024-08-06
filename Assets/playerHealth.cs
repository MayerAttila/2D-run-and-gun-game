using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class playerHealth : MonoBehaviour
{
    public class Bandage
    {
        public bool used;

        public int plussHP;
        public int duration;

        public float inUseValue;

        public Bandage(int plussHP, int duration)
        {
            this.plussHP = plussHP;
            this.duration = duration;

            inUseValue = 0;
            used = false;
        }

        public void UseBandage(GameObject playerGameObject)
        {
            if (!playerGameObject.GetComponent<playerHealth>().isBandaging) 
            {
                playerGameObject.GetComponent<playerHealth>().isBandaging = true;
                inUseValue = 0;
            } 
            playerGameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (inUseValue < duration) 
            {
                inUseValue = inUseValue + Time.deltaTime;
                playerGameObject.GetComponent<playerHealth>().RefreshBandageSlider(inUseValue);
            } 
            else
            {
                playerGameObject.GetComponent<playerHealth>().Heal(plussHP);
                used = true;
                playerGameObject.GetComponent<playerHealth>().RefreshHealthText();
                playerGameObject.GetComponent<playerHealth>().isBandaging = false;
                playerGameObject.GetComponent<playerHealth>().bandages.RemoveAt(0);
                playerGameObject.GetComponent<playerHealth>().RefreshBandageText();
            }
        }
    }



    public int health = SavePlayerDatas.mainCharacterTMP.currentHp;
    public int maxHealth = SavePlayerDatas.mainCharacterTMP.maxHp;
    

    public Slider healthSlider;
    public Text healthText;

    public int maxBandageCount = SavePlayerDatas.mainCharacterTMP.maxBandageCount;
    public List<Bandage> bandages = new List<Bandage>();
    
    public Text bandageText;
    public Slider bandageSlider;
    
    public bool isBandaging = false;

    public int bandageHealBoost = SavePlayerDatas.mainCharacterTMP.bandageHealBoost;

    public bool isAlive = true;
    float timer = 0f;






    // Start is called before the first frame update
    void Start()
    {
        maxHealth = SavePlayerDatas.mainCharacterTMP.maxHp;
        health = SavePlayerDatas.mainCharacterTMP.currentHp;

        healthSlider.maxValue = SavePlayerDatas.mainCharacterTMP.maxHp;
        healthSlider.value = SavePlayerDatas.mainCharacterTMP.currentHp;
        RefreshHealthSlider();
        RefreshHealthText();


        maxBandageCount = SavePlayerDatas.mainCharacterTMP.maxBandageCount;
        for (int i = 0; i < SavePlayerDatas.mainCharacterTMP.currentBandageCount; i++) bandages.Add(new Bandage(15 + bandageHealBoost, 2));
        RefreshBandageText();

        if(bandages.Count > 0) bandageSlider.maxValue = bandages[0].duration;


        bandageText.enabled = false;
        bandageSlider.gameObject.SetActive(false);
    }
    public void RefreshBandageSlider(float val)
    {
        if (val < bandageSlider.maxValue)
        bandageSlider.value = val;
    }

    public void RefreshHealthSlider() 
    {
        healthSlider.value = health;
    }

    public void RefreshHealthText() 
    {
        healthText.text = maxHealth.ToString() + "/" + health.ToString();
    }

    public void RefreshBandageText()
    {
        bandageText.text = maxBandageCount.ToString() + "/" + bandages.Count.ToString();
    }

    public void Heal(int addedHP) 
    {
        int futureHP = health + addedHP;

        if (futureHP < maxHealth) health = futureHP;
        else health = maxHealth;

        RefreshHealthSlider();
    }

    public void PlayerDie() 
    {
        isAlive = false;
        timer += Time.deltaTime;
        if (timer > 1.5f) gameObject.GetComponent<SceeneChange>().SceneChange("MainMenu");
        GameSave.DeleteGameSave();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        { 
            if(bandages.Count > 0 && bandages[0].used == false 
                && gameObject.GetComponent<shooting>().activeWeapon.name == "melee") bandages[0].UseBandage(gameObject);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isBandaging = false;
        }
        if (health <= 0) 
        {
            PlayerDie();
        }
    }
}
