using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generetePointToMove : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject mainCharacter;
    GameObject enemy;
    public Vector3 position;

    void Start()
    {
        mainCharacter = GameObject.Find("Main Character");
        enemy = gameObject.transform.parent.gameObject;
        gameObject.transform.position = mainCharacter.transform.position;
         
    }


    void GeneratePositionToMove() 
    {
        if (Random.Range(0, 2) == 1) position = new Vector3(gameObject.transform.position.x - Random.Range(0,5), gameObject.transform.position.y, 0);
        else position = new Vector3(gameObject.transform.position.x + Random.Range(0, 5), gameObject.transform.position.y, 0);
        gameObject.transform.position = position;
    }


    // Update is called once per frame
    void Update()
    {
        if (!mainCharacter.GetComponent<itemPickUp>().hidden) position = mainCharacter.transform.position;

        
        if (Mathf.Round(enemy.transform.position.x * 10f) / 10f == Mathf.Round(position.x * 10f) / 10f) GeneratePositionToMove();


        gameObject.transform.position = position;

    }

    

}
