using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.Animations;

public class SwarmManager : Singleton<SwarmManager>
{
    public List<BugAI> allBugs;
    public List<Transform> hunters;

    public GameObject hunterUIMarkPrefab;

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
            var followerUI = Instantiate(hunterUIMarkPrefab, hunter.transform.position, Quaternion.identity).GetComponent<PositionConstraint>();
            var source = new ConstraintSource { sourceTransform = hunter.transform, weight = 1f };
            followerUI.AddSource(source);
            followerUI.constraintActive = true;
            hunter.UI_ToDestroy = followerUI.gameObject;
        }
    }
}
