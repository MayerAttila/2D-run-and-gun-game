using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenereateMap : MonoBehaviour
{

    public List<GameObject> interactables = new List<GameObject>();
    public GameObject enemySpawner;
    public GameObject sceeneSwitch;
    public float spawnableDistanceX;
    GameObject spawnable;

    public void SpawnInteractables()
    {
        for (int i = 0; i < spawnableDistanceX; i += 4)
        {
            Debug.Log(Random.Range(0, 2));
            if (Random.Range(0, 2) == 0)
            {
                Debug.Log("true");
                spawnable = interactables[Random.Range(0, interactables.Count)];
                spawnable = Instantiate(spawnable, new Vector3(i, spawnable.transform.position.y), Quaternion.identity);
            }
        }
    }


    void Start()
    {
        gameObject.transform.localScale = new Vector3(100 + SavePlayerDatas.mainCharacterTMP.dayCount * 3, 3);
        gameObject.transform.position = new Vector3(gameObject.transform.localScale.x / 2 - 15, -4);
        GameObject leftSpawner = Instantiate(enemySpawner, new Vector3(-15, -1.5f), Quaternion.identity);
        GameObject rightSpawner = Instantiate(enemySpawner, new Vector3(gameObject.transform.localScale.x - 15, -1.5f), Quaternion.identity);
        rightSpawner.GetComponent<Spawner>().isFix = false;
        sceeneSwitch.transform.position = new Vector3(gameObject.transform.localScale.x - 25, -3);
        float spawnableDistanceX = gameObject.transform.localScale.x - 30;


        //SpawnInteractables();
        for (int i = 0; i < spawnableDistanceX; i += 4)
        {
            if (Random.Range(0, 2) == 0)
            {
                spawnable = interactables[Random.Range(0, interactables.Count)];
                spawnable = Instantiate(spawnable, new Vector3(i, spawnable.transform.position.y), Quaternion.identity);
            }
        }
    }

    


    void Update()
    {
        
        
    }
}
