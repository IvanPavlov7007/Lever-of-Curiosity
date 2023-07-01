using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class SwarmManager : Singleton<SwarmManager>
{
    public List<BugAI> allBugs;

    public float minMovementSpeed, maxMovementSpeed;
    public float boundsWidth, boundsHight;
    public float neighbourDistance;

    public Color huntColor, wanderColor, seekColor;
}
