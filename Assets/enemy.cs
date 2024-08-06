using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public GameObject player;

    public bool haveEnemyPos;

    public GameObject positionToMove;

    Rigidbody2D rb;

    public float movementSpeed = 1.15f;
    public int health = 100;
    public bool isAlive = true;
    public bool isAttacking = false;
    public float attackSpeed = 1f;
    public float attackTime = 0;
    public int damage = 20;
    public Slider healthSlider;

    public List<GameObject> arrowsInCharacter = new List<GameObject>();



    public void AttackPlayer(GameObject player)
    {
        if (attackTime < attackSpeed) attackTime = attackTime + Time.deltaTime;
        else
        {
            attackTime = 0;
            player.GetComponent<playerHealth>().health -= damage;
            player.GetComponent<playerHealth>().RefreshHealthSlider();
            player.GetComponent<playerHealth>().RefreshHealthText();
        }
    }

    public void RefreshHealthSlider()
    {
        if(healthSlider.enabled == false) healthSlider.gameObject.SetActive(true);
        healthSlider.value = health;
    }

    public void DethOfEnemy()
    {
        isAlive = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;

    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow" && !collision.gameObject.GetComponent<arrowScript>().hasHit)
        {
            GameObject currentArrow = collision.gameObject;
            arrowsInCharacter.Add(currentArrow);
            health -= currentArrow.gameObject.GetComponent<arrowScript>().damege;
            healthSlider.gameObject.SetActive(true);
            RefreshHealthSlider();
            if (health <= 0) DethOfEnemy();
            collision.gameObject.GetComponent<arrowScript>().hasHit = true;
        }
        if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<itemPickUp>().hidden) isAttacking = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") isAttacking = false;
        
    }
    

    

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Main Character");
        healthSlider = gameObject.GetComponentInChildren<Slider>();
        healthSlider.gameObject.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    void MoveEnemy(Vector3 position) 
    {
        if (this.transform.position.x > position.x) this.transform.position = new Vector3(this.transform.position.x - movementSpeed, this.transform.position.y, 0);
        else this.transform.position = new Vector3(this.transform.position.x + movementSpeed, this.transform.position.y, 0);
    }

    void horizontalMovment(Vector3 position)
    {
        int horizontalVal;
        if (this.transform.position.x > position.x) horizontalVal = -1;
        else horizontalVal = 1;
        Debug.Log(movementSpeed);
        rb.velocity = new Vector2(horizontalVal * movementSpeed, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            AttackPlayer(player.gameObject);
        }


        //if (isAlive) MoveEnemy(positionToMove.gameObject.GetComponent<generetePointToMove>().position);
        if (isAlive) horizontalMovment(positionToMove.gameObject.transform.position);
        else
        {
            if (arrowsInCharacter.Count == 0) Destroy(gameObject);
            else
            {
                for (int i = 0; i < arrowsInCharacter.Count; i++)
                {
                    if (arrowsInCharacter[i].gameObject == null) arrowsInCharacter.RemoveAt(i);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
