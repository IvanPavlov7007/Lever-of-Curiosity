using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[ExecuteAlways]
public class VertexPosChecker : MonoBehaviour
{
    public SpriteSkin spriteSkin;

    private void Update()
    {
        if (spriteSkin.HasCurrentDeformedVertices())
        {
            foreach(var pos in spriteSkin.GetDeformedVertexPositionData())
            {
                transform.position = spriteSkin.transform.TransformPoint(pos);
                break;
            }
        }
    }
}
