using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shop : MonoBehaviour
{
    public class Vender 
    {
        public bool rebuyable;
        public int price;
        public GameObject gObj;
        public TextMeshPro text;
        public string product;

        public Vender(string product, bool rebuyable, int price, GameObject gObj) 
        {
            this.rebuyable = rebuyable;
            this.price = price;
            this.gObj = gObj;
            text = gObj.GetComponentInChildren<TextMeshPro>();
            text.text = product + "\n" + price.ToString();

            this.product = product;
        }


        public void Purchace(int currentMoney, GameObject playerGameObject) 
        {
            bool bought = false;
            int futureMoney = currentMoney - price;
            if (futureMoney >= 0) 
            {
                if (product == "Arrow" && playerGameObject.GetComponent<shooting>().arrowCount < playerGameObject.GetComponent<shooting>().maxArrowCount)
                {
                    playerGameObject.GetComponent<shooting>().arrowCount++;
                    playerGameObject.GetComponent<shooting>().RefreshArrowCount();
                    bought = true;
                }
                else if (product == "Heal")
                {
                    playerGameObject.GetComponent<playerHealth>().Heal(100);
                    bought = true;
                }
                else if (product == "Bandage" && playerGameObject.GetComponent<playerHealth>().bandages.Count < playerGameObject.GetComponent<playerHealth>().maxBandageCount)
                {
                    playerGameObject.GetComponent<playerHealth>().bandages.Add(new playerHealth.Bandage(15 + SavePlayerDatas.mainCharacterTMP.bandageHealBoost, 2));
                    bought = true;
                }
                else if (product == "Arrow Upgrade" && playerGameObject.GetComponentInChildren<arrowScript>().arrowStage < 10) 
                {

                    SavePlayerDatas.mainCharacterTMP.arrowUpgradeStage++;
                    bought = true;
                }
                else if (product == "Melee Upgrade" && playerGameObject.GetComponentInChildren<arrowScript>().arrowStage < 10)
                {

                    SavePlayerDatas.mainCharacterTMP.meleeUpgradeStage++;
                    bought = true;
                }

                if (bought) 
                {
                    playerGameObject.GetComponent<itemPickUp>().currenMoney = futureMoney;
                    playerGameObject.GetComponent<itemPickUp>().RefreshMoneyText();
                }

                
            } 
        }


    }
    public List<GameObject> squres = new List<GameObject>();
    public List<Vender> Venders = new List<Vender>();
    

    // Start is called before the first frame update
    void Start()
    {
        Venders.Add(new Vender("Arrow" ,true, 2, squres[0]));
        Venders.Add(new Vender("Heal", false, 20, squres[1]));
        Venders.Add(new Vender("Bandage", true, 10, squres[2]));
        Venders.Add(new Vender("Arrow Upgrade", true, 50 * (SavePlayerDatas.mainCharacterTMP.arrowUpgradeStage + 1), squres[3]));
        Venders.Add(new Vender("Melee Upgrade", true, 50 * (SavePlayerDatas.mainCharacterTMP.meleeUpgradeStage + 1), squres[4]));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
