using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.AI;

public class BugAI : MonoBehaviour
{
    [SerializeField]
    float maxVelocity, minVelocity;
    //Vector3 currentTarget;

    Rigidbody rb;
    NavMeshAgent navMeshAgent;
    Material material;
    SwarmManager swarmManager;

    public BugState currentState { get; private set; }

    WanderState wanderState;
    SeekState seekState;

    float currentSpeed { set { navMeshAgent.speed = value; } get { return navMeshAgent.speed; } }
    System.IDisposable timer;

    private void Start()
    {
        material = GetComponentInChildren<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        swarmManager = SwarmManager.Instance;
        swarmManager.allBugs.Add(this);

        wanderState = new WanderState();
        seekState = new SeekState();
        setState(seekState);


        //timer = Observable.Interval(System.TimeSpan.FromSeconds(3f)).
        //    Subscribe(x =>
        //    {
        //        if (Random.value < 0.1f)
        //            StartNewJourney();
        //        else StartCoroutine(velocityChanger());
        //    });
        //StartNewJourney();
        //StartCoroutine(velocityChanger());
    }
    void Update()
    {
        currentState.Update(this);

        //Vector3 dist = currentTarget - transform.position;

        //if(dist.magnitude < 0.1f)
        //{
        //    StartNewJourney();
        //}


        //navMeshAgent.speed = currentVelocity;

        //debug_rmb();
    }

    private void setState(BugState state)
    {
        currentState = state;
        currentState.SetUpState(this);
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
        SwarmManager.Instance.allBugs.Remove(this);
        //timer.Dispose();
    }

    void StartNewJourney()
    {
        navMeshAgent.SetDestination(chooseNewPointRandomly());
    }

    IEnumerator velocityChanger()
    {
        float targetVelocity = Random.Range(minVelocity, maxVelocity);
        float step = (targetVelocity - currentSpeed) / 2f;
        while (Mathf.Abs(currentSpeed - targetVelocity) > 0.2f)
        {
            currentSpeed += step * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentSpeed = targetVelocity;
    }

    Vector3 chooseNewPointRandomly()
    {
        var rand = new Vector3(Random.Range(-swarmManager.boundsWidth, swarmManager.boundsWidth), 0f, Random.Range(-swarmManager.boundsHight, swarmManager.boundsHight));
        return (samplePosition(rand));
    }

    Vector3 samplePosition(Vector3 target)
    {
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(target, out navMeshHit, 1f, 1);
        return navMeshHit.position;
    }


    public abstract class BugState
    {
        public abstract void Update(BugAI context);
        public abstract void SetUpState(BugAI context);

    }


    //public class HuntState : BugState
    //{

    //}


    BugAI nearestBug(out float distance)
    {
        BugAI nearestBug = null;
        distance = float.MaxValue;
        foreach (var bug in this.swarmManager.allBugs)
        {
            if (bug != this)
            {
                float dist = Vector3.Distance(bug.transform.position, this.transform.position);
                if (dist < distance)
                {
                    distance = dist; nearestBug = bug;
                }
            }
        }
        return nearestBug;
    }

    public class SeekState : BugState
    {
        public override void SetUpState(BugAI context)
        {
            context.material.color = context.swarmManager.seekColor;
        }

        public override void Update(BugAI context)
        {
            if (Random.Range(0f, 1000f) < 2f || (!context.navMeshAgent.pathPending && context.navMeshAgent.remainingDistance < 0.01f))
            {
                context.StartNewJourney();
                context.currentSpeed = Random.Range(context.swarmManager.minMovementSpeed, context.swarmManager.maxMovementSpeed);
            }
            if (Random.Range(0, 100) < 10)
            {
                context.currentSpeed = Random.Range(context.swarmManager.minMovementSpeed, context.swarmManager.maxMovementSpeed);
            }

            if (Random.Range(0f, 100f) < 2f)
            {
                context.setState(context.wanderState);
            }
        }
    }

    public class WanderState : BugState
    {
        public override void SetUpState(BugAI context)
        {
            context.material.color = context.swarmManager.wanderColor;
        }

        public override void Update(BugAI context)
        {
            if(Random.Range(0,100) < 10)
            {
                applyRules(context);
            }

            if(context.navMeshAgent.remainingDistance < 0.01f)
            {
                context.setState(context.seekState);
            }
        }

        void applyRules(BugAI context)
        {
            float nearestDist;
            BugAI nearestBug = context.nearestBug(out nearestDist);

            if (nearestDist < context.swarmManager.neighbourDistance)
            {
                context.navMeshAgent.destination = context.samplePosition(nearestBug.navMeshAgent.destination + context.transform.position - nearestBug.transform.position);
                context.currentSpeed = Mathf.Lerp(context.currentSpeed, nearestBug.currentSpeed, Time.deltaTime);
            }
        }
    }
}
