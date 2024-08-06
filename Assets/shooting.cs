using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{
    public int maxArrowCount = SavePlayerDatas.mainCharacterTMP.maxArrowCount;
    public int arrowCount = SavePlayerDatas.mainCharacterTMP.currentArrowCount;

    public GameObject arrowPrefab;
    public List<GameObject> arrows = new List<GameObject>();

    public int maxShotPower = SavePlayerDatas.mainCharacterTMP.maxShotPower;
    public float shotPower = 0;
    float shotpowerIncreeseVal = 1 + (float)SavePlayerDatas.mainCharacterTMP.strenght / 10;

    private Vector3 targetPos;

    public Slider shotPowerSlider;
    public Text arrowCountText;


    public GameObject tmp;
    public Camera cam;

    public GameObject bow;
    public GameObject melee;
    public GameObject activeWeapon;

    // Start is called before the first frame update

    public void RefreshArrowCount()
    {
        arrowCountText.text = maxArrowCount.ToString() + "/" + arrowCount.ToString();
    }

    public void HideWeapons(Color color) 
    {
        if (bow.GetComponent<Renderer>().material.color != color) 
        {
            bow.GetComponent<Renderer>().material.color = color;
            melee.GetComponent<Renderer>().material.color = color;
        }
        
    }


    void SwitchWeapon() 
    {
        if (activeWeapon == bow)
        {
            activeWeapon = melee;
            arrowCountText.enabled = false;
            shotPowerSlider.gameObject.SetActive(false);
            gameObject.GetComponent<playerHealth>().bandageText.enabled = true;
            gameObject.GetComponent<playerHealth>().bandageSlider.gameObject.SetActive(true);
            bow.SetActive(false);

        }
        else 
        {
            activeWeapon = bow;
            arrowCountText.enabled = true;
            shotPowerSlider.gameObject.SetActive(true);
            gameObject.GetComponent<playerHealth>().bandageText.enabled = false;
            gameObject.GetComponent<playerHealth>().bandageSlider.gameObject.SetActive(false);
            melee.SetActive(false);
        }
        activeWeapon.SetActive(true);
    }



    void Start()
    {

        maxArrowCount = SavePlayerDatas.mainCharacterTMP.maxArrowCount;
        arrowCount = SavePlayerDatas.mainCharacterTMP.currentArrowCount;
        cam = Camera.main;

        shotPowerSlider.maxValue = SavePlayerDatas.mainCharacterTMP.maxShotPower;



        activeWeapon = bow;
        melee.SetActive(false);

        RefreshArrowCount();
    }

    void shootingArrow(Vector2 direction, float rotationZ, float shotPower)
    {

        GameObject arr = Instantiate(arrowPrefab);
        arr.transform.position = bow.gameObject.transform.position;
        arr.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        arr.GetComponent<Rigidbody2D>().velocity = direction * shotPower;
        arrowCount--;
        RefreshArrowCount();
        arrows.Add(arr);
    }

    public void RefreshShotPowerSlider() 
    {
        if (shotPower < shotPowerSlider.maxValue) shotPower = shotPower + shotpowerIncreeseVal;
        shotPowerSlider.value = shotPower;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = cam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector3 diference = targetPos - tmp.transform.position;
        float rotationZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        tmp.transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !gameObject.GetComponent<playerHealth>().isBandaging)
        {
            shotPower = 0;
        }

        if (Input.GetKey(KeyCode.Mouse0) && !gameObject.GetComponent<playerHealth>().isBandaging)
        {
            RefreshShotPowerSlider();

        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && !gameObject.GetComponent<playerHealth>().isBandaging)
        {
            if (activeWeapon == bow)
            {
                if (arrowCount > 0 && !gameObject.GetComponent<itemPickUp>().hidden)
                {
                    float distance = diference.magnitude;
                    Vector2 direction = diference / distance;
                    direction.Normalize();
                    shootingArrow(direction, rotationZ, shotPower / 3);
                }
            }
            else 
            {
                
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            SwitchWeapon();
        }
    }
}
