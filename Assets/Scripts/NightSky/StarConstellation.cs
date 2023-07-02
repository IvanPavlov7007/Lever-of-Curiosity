using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.U2D;
using UnityEngine.Events;

public class StarConstellation : MonoBehaviour
{
    public LinkedList<StarNode> orderedStars;
    public List<StarNode> allStars;
    public SpriteShapeController spriteShapeController;
    public Spline spline;
    public UnityEvent onComplete;

    Transform tipTransform;

    private void Start()
    {
        spline = spriteShapeController.spline;
        spline.Clear();
        allStars = new List<StarNode>(GetComponentsInChildren<StarNode>());
        foreach(var star in allStars)
        {
            star.onStarPressed += HandleStarPressed;
            star.onStarTouched += HandleStarTouched;
        }
    }

    private void Update()
    {
        if(spline.isOpenEnded && tipTransform != null)
        {
            spline.SetPosition(spline.GetPointCount() - 1, worldToConstellationPosition(tipTransform.position));
        }

        if (!spline.isOpenEnded)
        {
            LinkedListNode<StarNode> starNode = orderedStars.First;
            for (int i = 0; i < orderedStars.Count; i++)
            {
                spline.SetPosition(i, starNode.Value.transform.localPosition);
                starNode = starNode.Next;
            }
        }
    }

    void HandleStarTouched(StarNode star, Collider collider)
    {
        if (orderedStars == null)
            orderedStars = new LinkedList<StarNode>();
        tipTransform = collider.transform;
        updateSpline(star);

    }

    void HandleStarPressed(StarNode star)
    {
        //if (orderedStars == null)
        //    orderedStars = new LinkedList<StarNode>();
        //updateSpline(star);
    }

    void updateSpline(StarNode star)
    {
        if (!spline.isOpenEnded)
            return;

        if(orderedStars.Contains(star))
        {
            if (orderedStars.First.Value == star)
            {
                spline.RemovePointAt(spline.GetPointCount() - 1); // remove the tip
                spline.isOpenEnded = false;
                if (onComplete != null)
                    onComplete.Invoke();
            }
            return;
        }

        orderedStars.AddLast(star);
        int index = spline.GetPointCount();
        if (index == 0)
        {
            spline.InsertPointAt(index, star.transform.localPosition);
            spline.SetHeight(index++, 0.1f);
            spline.InsertPointAt(index, worldToConstellationPosition(tipTransform.position));
            spline.SetHeight(index, 0.1f);
        }
        else
        {
            index--;
            spline.SetPosition(index, star.transform.localPosition);
            spline.SetHeight(index++, 0.1f);
            spline.InsertPointAt(index, worldToConstellationPosition(tipTransform.position));
            spline.SetHeight(index, 0.1f);
        }
    }

    Vector3 worldToConstellationPosition(Vector3 worldPosition)
    {

        var normalizedPos = transform.InverseTransformPoint(worldPosition);
        Debug.DrawLine(Vector3.zero, worldPosition);
        Debug.DrawLine(Vector3.zero, normalizedPos,Color.red);
        return normalizedPos;
    }
}
