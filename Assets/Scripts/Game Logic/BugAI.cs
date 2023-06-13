using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BugAI : MonoBehaviour
{
    [SerializeField]
    float maxVelocity, minVelocity;
    [SerializeField]
    float width, heigh;
    Vector3 currentTarget;

    Rigidbody rb;
    float currentVelocity;
    System.IDisposable timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = Observable.Interval(System.TimeSpan.FromSeconds(3f)).
            Subscribe(x =>
            {
                if (Random.value < 0.1f)
                    StartNewJourney();
                else StartCoroutine(velocityChanger());
            });
        StartNewJourney();
        StartCoroutine(velocityChanger());
    }

    void Update()
    {
        Vector3 dist = currentTarget - transform.position;

        if(dist.magnitude < 0.1f)
        {
            StartNewJourney();
        }

        

        rb.velocity = dist.normalized * currentVelocity;
    }

    private void OnDestroy()
    {
        timer.Dispose();
    }

    void StartNewJourney()
    {
        currentTarget = chooseNewPointRandomly();
    }

    IEnumerator velocityChanger()
    {
        float targetVelocity = Random.Range(minVelocity, maxVelocity);
        float step = (targetVelocity - currentVelocity) / 2f;
        while (Mathf.Abs(currentVelocity - targetVelocity) > 0.2f)
        {
            currentVelocity += step * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentVelocity = targetVelocity;
    }

    Vector3 chooseNewPointRandomly()
    {
        return new Vector3(Random.Range(-width, width), 0f, Random.Range(-width, width));
    }

    Vector3 chooseNewNearPoint()
    {
        return new Vector3();
    }

    Vector3 chooseNewFarPoint()
    {
        return new Vector3();
    }
}
