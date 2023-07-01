using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.AI;

public class BugAI : MonoBehaviour
{
    [SerializeField]
    float maxVelocity, minVelocity;
    [SerializeField]
    float width, heigh;
    Vector3 currentTarget;

    Rigidbody rb;
    NavMeshAgent navMeshAgent;


    float currentVelocity;
    System.IDisposable timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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


        navMeshAgent.speed = currentVelocity;

        debug_rmb();
        //rb.velocity = dist.normalized * currentVelocity;
    }

    void debug_rmb()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var pos = Input.mousePosition;
            var point = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 3f));

            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(point, out navMeshHit, 1f, 1);
            navMeshAgent.SetDestination(navMeshHit.position);
        }
    }

    private void OnDestroy()
    {
        timer.Dispose();
    }

    void StartNewJourney()
    {
        currentTarget = chooseNewPointRandomly();
        navMeshAgent.SetDestination(currentTarget);
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
        var rand = new Vector3(Random.Range(-width, width), 0f, Random.Range(-width, width));
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(rand, out navMeshHit, 1f, 1);
        return navMeshHit.position;
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
