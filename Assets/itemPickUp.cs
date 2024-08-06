using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemPickUp : MonoBehaviour
{

    public List<GameObject> pickUpableArrows = new List<GameObject>();

    Interactable interactable;
    Interactable lastInteractable;

    public shop.Vender vender;

    public shooting shootingScript;

    public bool hidden = false;
    public bool isPicklocking = false;
    public bool isLooting = false;

    public int currenMoney = SavePlayerDatas.mainCharacterTMP.money;
    public Text moneyText;

    public Text interactionText;
    public Slider interactionSlider;
    public float interactionValue;


    public float picklockTimeOfMelee;
    public float playerLootTime;

    

    void Start()
    {
        currenMoney = SavePlayerDatas.mainCharacterTMP.money;
        RefreshMoneyText();
        picklockTimeOfMelee = gameObject.GetComponent<shooting>().melee.GetComponent<MeleeWeapon>().picklockTime;
        playerLootTime = SavePlayerDatas.mainCharacterTMP.lootTime;
    }

    public void RefreshMoneyText() 
    {
        moneyText.text = currenMoney.ToString();
    }

    public void RefreshInteractionSlider(float maxVal, float currentVal, string interactionText) 
    {
        if(interactionSlider.maxValue != maxVal) interactionSlider.maxValue = maxVal;
        interactionSlider.value = currentVal;
        this.interactionText.text = interactionText;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow") pickUpableArrows.Add(collision.gameObject);
        
        else if (collision.gameObject.tag == "Interactable") 
        {
            interactable = collision.gameObject.GetComponent<Interactable>();
        } 

        else if (collision.gameObject.tag == "Vender")
        {
            List<GameObject> venderSquares = collision.gameObject.GetComponentInParent<shop>().squres;
            for (int i = 0; i < venderSquares.Count; i++)
            {
                if (venderSquares[i] == collision.gameObject) vender = collision.gameObject.GetComponentInParent<shop>().Venders[i];
            }
            Debug.Log(vender.product);
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow") pickUpableArrows.Remove(collision.gameObject);
        else interactable = null;
    }

    void PickupArrow() 
    {
        Destroy(pickUpableArrows[0]);
        shootingScript.arrowCount++;
        shootingScript.RefreshArrowCount();
        
    }

    void Picklocking()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        isPicklocking = true;
        if (interactionValue < picklockTimeOfMelee) 
        {
            interactionValue = interactionValue + Time.deltaTime;
            RefreshInteractionSlider(picklockTimeOfMelee, interactionValue, "Picklocking");
        } 
        else
        {
            interactable.isLocked = false;
            interactionValue = 0;
        }
    }


    void Looting()
    {
        Interactable currentInteractable = interactable;
        if (hidden) currentInteractable = lastInteractable;

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        isLooting = true;
        if (interactionValue < playerLootTime) 
        {
            interactionValue = interactionValue + Time.deltaTime;
            RefreshInteractionSlider(playerLootTime, interactionValue, "Looting");
        } 
        else
        {
            currenMoney += currentInteractable.allLoot[0];
            RefreshMoneyText();

            if (shootingScript.arrowCount + currentInteractable.allLoot[1] > shootingScript.maxArrowCount) shootingScript.arrowCount = shootingScript.maxArrowCount;
            else shootingScript.arrowCount += currentInteractable.allLoot[1];
            shootingScript.RefreshArrowCount();

            currentInteractable.isLooted = true;
        }
    }


    void Hiding()
    {
        if (!hidden) 
        {
            lastInteractable = interactable;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
            gameObject.GetComponent<shooting>().HideWeapons(new Color(1, 1, 1, 0.5f));
            gameObject.GetComponent<shooting>().bow.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
            hidden = true;
        }
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickUpableArrows.Count > 0 && shootingScript.arrowCount < shootingScript.maxArrowCount) PickupArrow();
            
            else if (interactable != null) 
            {
                interactionValue = 0;
                if (interactable.isLocked) RefreshInteractionSlider(0, 0, "Locked");
            }

            else if (vender != null) vender.Purchace(currenMoney, gameObject);
        }

        if (Input.GetKey(KeyCode.E)) 
        {
            if (interactable != null) 
            {
                if (interactable.isHideable) 
                {
                    Hiding();
                    if (!interactable.isLocked && !interactable.isLooted || !lastInteractable.isLooted) Looting();
                } 

                if (!interactable.isLocked) 
                {
                    if((!interactable.isLooted)) Looting();

                } 
                
                else if(gameObject.GetComponent<shooting>().activeWeapon.name == "melee") Picklocking(); 
            }
        }
        
        if (Input.GetKeyUp(KeyCode.E)) 
        {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
            lastInteractable = null;
            hidden = false;
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            gameObject.GetComponent<shooting>().HideWeapons(new Color(1, 1, 1, 1));
            isLooting = false;
            isPicklocking = false;
        }


        
    }
}
