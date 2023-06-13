using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Pixelplacement;

[ExecuteAlways]
public class StarVertexesManager : MonoBehaviour
{
    [SerializeField] SpriteSkin spriteSkin;
    [SerializeField] GameObject StarPrefab;
    [SerializeField] Spline spline;


    [SerializeField] LinkedList<SplineAnchor> anchors;

    [MenuItem("Stars/Create Stars")]
    public static void CreateVertexes()
    {
        var manager = Selection.activeTransform.gameObject.GetComponent<StarVertexesManager>();
        if (manager == null)
            throw new UnityException("Please sellect object with StarVertexesManager Component");

        manager.CreateVertexesForThis();
    }    

    public void CreateVertexesForThis()
    {
        anchors = new LinkedList<SplineAnchor>();
        int i = 0;
        foreach (var point in spriteSkin.GetDeformedVertexPositionData())
        {
            var pos = spriteSkin.transform.TransformPoint(point);
            spline.AddAnchors(1);
            var anch = spline.Anchors[i++];
            anch.transform.position = pos;
            var star = Instantiate(StarPrefab, pos, Quaternion.identity, anch.transform);
            anchors.AddLast(anch);
        }
    }

    void Update()
    {
        //if (anchors == null || spriteSkin == null)
        //    return;

        //var node = anchors.First;
        int i = 0;
        spline.enabled = false;
        if(spriteSkin.HasCurrentDeformedVertices())
        foreach (var point in spriteSkin.GetDeformedVertexPositionData())
        {
            spline.Anchors[i++].transform.position = spriteSkin.transform.TransformPoint(point);
            //node.Value.transform.position = spriteSkin.transform.TransformPoint(point);
            //node = node.Next;
        }
        spline.enabled = true;
    }
}
