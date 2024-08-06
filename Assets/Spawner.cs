using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public float minSpawnRate = 3f - SavePlayerDatas.mainCharacterTMP.dayCount / 20;
    public float maxSpawnRate;
    public float randomSpawnRate;
    public float currentRate = 0;
    [SerializeField] private GameObject enemy;
    public bool isFix = true;
    public Camera cam;

    public void Spawn()
    {
        if (currentRate < randomSpawnRate) currentRate = currentRate + Time.deltaTime;
        else
        {
            currentRate = 0;
            randomSpawnRate = Random.RandomRange(minSpawnRate, maxSpawnRate);
            GameObject gameObjectToSpawn = enemy;
            gameObjectToSpawn = Instantiate(gameObjectToSpawn, transform.position, Quaternion.identity);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //GameObject tmp = Instantiate(enemy, transform.position, Quaternion.identity);
        maxSpawnRate = minSpawnRate * 3;
        randomSpawnRate = Random.RandomRange(minSpawnRate, maxSpawnRate);
    }


    // Update is called once per frame
    void Update()
    {
        Spawn();

        if (!isFix) 
        {
            gameObject.transform.position = new Vector3(cam.transform.position.x + 15, gameObject.transform.position.y);
        } 
    }
}
