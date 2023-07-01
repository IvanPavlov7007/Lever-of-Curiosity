using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.U2D;

public class StarConstellation : MonoBehaviour
{
    public LinkedList<StarNode> orderedStars;
    public List<StarNode> allStars;
    public SpriteShapeController spriteShapeController;
    public Spline spline;

    private void Start()
    {
        spline = spriteShapeController.spline;
        spline.Clear();
        allStars = new List<StarNode>(GetComponentsInChildren<StarNode>());
        foreach(var star in allStars)
        {
            star.onStarPressed += HandleStarPressed;
        }
    }

    void HandleStarPressed(StarNode star)
    {
        if (orderedStars == null)
            orderedStars = new LinkedList<StarNode>();
        updateSpline(star);
    }

    void updateSpline(StarNode star)
    {
        if (!spline.isOpenEnded)
            return;

        if(orderedStars.Contains(star))
        {
            if (orderedStars.First.Value == star)
                spline.isOpenEnded = false;
            return;
        }

        orderedStars.AddLast(star);
        int index = spline.GetPointCount();
        spline.InsertPointAt(index, star.transform.localPosition);
        spline.SetHeight(index, 0.1f);
    }
}
