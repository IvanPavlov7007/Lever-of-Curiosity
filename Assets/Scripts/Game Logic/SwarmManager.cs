using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class SwarmManager : Singleton<SwarmManager>
{
    public List<BugAI> allBugs;
    public List<Transform> hunters;

    public float minMovementSpeed, maxMovementSpeed;
    public float huntBoost, fleeBoost;
    public float boundsWidth, boundsHight;
    public float neighbourDistance;
    public float huntDistance, hunterAwareDistance;

    public Color huntColor, wanderColor, seekColor;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            var hunter = allBugs[Random.Range(0, allBugs.Count - 1)];
            hunters.Add(hunter.transform);
            allBugs.Remove(hunter);
            hunter.BecomeHunter();
        }
    }
}
