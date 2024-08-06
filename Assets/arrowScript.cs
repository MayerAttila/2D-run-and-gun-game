using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowScript : MonoBehaviour
{
    public int arrowStage = SavePlayerDatas.mainCharacterTMP.arrowUpgradeStage;
    public int brakeChance;
    public int maxDamege;
    public float shotPower;
    public int damege;
    //public GameObject gameObject;
    public Rigidbody2D rigidbody;
    public BoxCollider2D boxCollider;

    public bool hasHit = false;
    //public arrow arr;

    float diferenceOnX = 0;
    Transform enemysPos;

    GameObject hitedCharacter;

    public GameObject arrowObject;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Random.Range(0, 101) < brakeChance) Destroy(gameObject);
        else 
        {
            rigidbody.velocity = Vector2.zero;
            boxCollider.isTrigger = true;
            rigidbody.isKinematic = true;


            hasHit = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.tag == "Enemy" && hasHit == false)
        {
            if (Random.Range(0, 101) < brakeChance) Destroy(gameObject);
            else 
            {
                rigidbody.velocity = Vector2.zero;
                boxCollider.isTrigger = true;
                rigidbody.isKinematic = true;

                hitedCharacter = collision.gameObject;
                enemysPos = hitedCharacter.transform;
                diferenceOnX = this.transform.position.x - enemysPos.position.x;
            }
            
        }
        
    }

    void MoveArrowWhithEnemy() 
    {
        if (this.transform.position.x - enemysPos.position.x != diferenceOnX) this.transform.position =  new Vector3(enemysPos.position.x + diferenceOnX, this.transform.position.y,0);
    }


    // Start is called before the first frame update
    void Start()
    {
        arrowStage = SavePlayerDatas.mainCharacterTMP.arrowUpgradeStage;
        brakeChance = 50 - 5 * arrowStage;
        maxDamege = 40 + 3 * arrowStage;

        GameObject g = GameObject.Find("Main Character");

        shotPower = g.GetComponent<shooting>().shotPower; 
        damege = (int)(maxDamege * shotPower / 100);

    }

    void RotationChangeWhileFlying()
    {
        float angle = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (diferenceOnX != 0)
        {
            if (hitedCharacter.GetComponent<enemy>().isAlive) MoveArrowWhithEnemy();
            else diferenceOnX = 0;
        }

        if (hasHit == false) 
        {
            RotationChangeWhileFlying();
        }
    }
}
