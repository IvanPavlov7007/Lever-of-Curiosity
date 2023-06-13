using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UniRx;

using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float interval = 5.5f;
    public float probability = 0.6f;

    public List<GameObject> creations { get; private set; }
    public Transform spawnPosition { get; private set; }


    IDisposable timer;

    private void Awake()
    {
        spawnPosition = transform;
        creations = new List<GameObject>();
    }

    private void Start()
    {
        timer = Observable.Interval(TimeSpan.FromSeconds(interval)).Subscribe(x=>
        {
            if (Random.value < probability)
                spawn(prefab);
        }
        );
    }

    public void onCreationDestroyed(GameObject creation)
    {
        creations.Remove(creation);
    }

    void spawn(GameObject prefab)
    {
        var obj = Instantiate(prefab, spawnPosition.position, Quaternion.identity);
        obj.AddComponent<LifeTrackable>().onDestroy += onCreationDestroyed;
        creations.Add(obj);
    }
}
