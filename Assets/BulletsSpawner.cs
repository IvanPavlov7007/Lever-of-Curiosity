using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsSpawner : MonoBehaviour
{
    public GameObject BulletPrefab;

    public float minHeight, maxHeight, minTime, maxTime;

    public float speed;
    void SpawnBullet()
    {
         
        Rigidbody2D rb = Instantiate(BulletPrefab, new Vector3(transform.position.x, Random.Range(minHeight, maxHeight), 0f),Quaternion.identity).GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
    }


    public void StartSpawn()
    {
        pause = false;
    }

    public void PauseSpawn()
    {
        pause = true;
    }

    float nextSpawnTime;
    float timer;
    public bool pause = true;
    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            timer += Time.deltaTime;
            if (timer > nextSpawnTime)
            {
                SpawnBullet();
                timer = 0f;
                nextSpawnTime = Random.Range(minTime, maxTime);
            }
        }
    }
}
