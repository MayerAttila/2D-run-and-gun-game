using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    int maxStamina = SavePlayerDatas.mainCharacterTMP.maxStamina;
    float stamina = SavePlayerDatas.mainCharacterTMP.maxStamina;
    float staminaRegen = SavePlayerDatas.mainCharacterTMP.staminaRegen;
    float staminaConsumtion = SavePlayerDatas.mainCharacterTMP.staminaConsumtion;
    float movSpeed = SavePlayerDatas.mainCharacterTMP.movementSpeed;
    float sprintBoost = SavePlayerDatas.mainCharacterTMP.sprintBoost;
    bool isSprinting = false;
    public Camera cam;

    public Text staminaText;
    public Slider staminaSlider;

    public float horizontal;
    public Rigidbody2D rigidbody;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        staminaSlider.maxValue = maxStamina;

        Debug.Log("Consum   " + staminaConsumtion);
        Debug.Log("Regen   " + staminaRegen);

        RefreshStaminaSlider();
    }

    public Transform ReturnPlayerPosition()
    {
        return this.transform;
    }

    void RefreshStaminaSlider() 
    {
        staminaText.text = maxStamina + "/" + Mathf.RoundToInt(stamina).ToString();
        staminaSlider.value = stamina;
    }


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        cam.transform.position = new Vector3(this.transform.position.x, cam.transform.position.y, -10);

        if (Input.GetKeyDown(KeyCode.LeftShift)) isSprinting = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isSprinting = false;

        if (!gameObject.GetComponent<itemPickUp>().hidden && !gameObject.GetComponent<itemPickUp>().isPicklocking && !gameObject.GetComponent<itemPickUp>().isLooting && !gameObject.GetComponent<playerHealth>().isBandaging) horizontalMovment();

    }

    void horizontalMovment()
    {
        float speed = movSpeed;
        if (isSprinting)
        {
            speed = movSpeed * sprintBoost;
            if (stamina > -1) stamina = stamina - staminaConsumtion;
            RefreshStaminaSlider();
        }
        if (!isSprinting && stamina < maxStamina) 
        {
            stamina = stamina + staminaRegen;
            RefreshStaminaSlider();
        } 

        rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y);
    }

}
