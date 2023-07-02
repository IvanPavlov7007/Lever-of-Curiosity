using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeCap : MonoBehaviour
{
    public float maxLifeTime = 10;

    float lifeTime = 0f;
    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime)
            Destroy(gameObject);
    }
}
