using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.U2D;
using System.Linq;

[ExecuteAlways]
public class StarVertexesManager : MonoBehaviour
{
    [SerializeField] SpriteSkin spriteSkin;
    [SerializeField] GameObject StarPrefab;
    Spline spline;
    [SerializeField]SpriteShapeController spriteShapeController;

    int[] sortedIndexes;
    List<GameObject> stars;

    [MenuItem("Stars/Create Stars")]
    public static void CreateVertexes()
    {
        var manager = Selection.activeTransform.gameObject.GetComponent<StarVertexesManager>();
        if (manager == null)
            throw new UnityException("Please sellect object with StarVertexesManager Component");

        manager.CreateVertexesForThis();
    }

    void sortVerticesIndexes(Vector3[] originalArray)
    {
        int length = originalArray.Length;
        var sortedArray = originalArray.OrderBy(i => Mathf.Atan2(i.y, i.x)).ToArray();
        sortedIndexes = new int[length];

        for(int i = 0; i < length; i++)
        {
            sortedIndexes[i] = System.Array.FindIndex(sortedArray,v => v.Equals(originalArray[i]));
        }
    }

    public void CreateVertexesForThis()
    {
        

        //sortedIndexes = Enumerable.Range(0, originalPositions.Length)
        //    .OrderBy(i => Mathf.Atan2(originalPositions[i].y, originalPositions[i].x))
        //    .ToArray();

        if (stars == null)
        {
            stars = new List<GameObject>();
        }
        foreach (var star in stars)
        {
            DestroyImmediate(star);
        }
        stars.Clear();
        spline = spriteShapeController.spline;
        spline.Clear();

        var originalVerticesOrder = spriteSkin.GetDeformedVertexPositionData().ToArray();

        sortVerticesIndexes(originalVerticesOrder);
        for (int i = 0; i < originalVerticesOrder.Length; i++)
        {
            spline.InsertPointAt(i, Vector3.right * i + Vector3.up * 100f);
            var star = Instantiate(StarPrefab, transform.position, Quaternion.identity, transform);
            stars.Add(star);
        }

        for(int i = 0; i < originalVerticesOrder.Length; i++)
        {
            var pos = spriteSkin.transform.TransformPoint(originalVerticesOrder[i]);
            spline.SetPosition(sortedIndexes[i], pos);
            stars[sortedIndexes[i]].transform.position = pos;
        }
    }

    void Update()
    {
        
        if (spriteSkin.HasCurrentDeformedVertices())
        {
            var originalVerticesOrder = spriteSkin.GetDeformedVertexPositionData().ToArray();
            for(int i = 0; i < originalVerticesOrder.Length; i++)
            {
                var pos = spriteSkin.transform.TransformPoint(originalVerticesOrder[i]);
                stars[sortedIndexes[i]].transform.position = pos;
                spline.SetPosition(sortedIndexes[i], pos);
            }
        }
    }
}
