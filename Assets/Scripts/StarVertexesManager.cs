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

    [SerializeField] int[] sortedIndexes;
    [SerializeField] List<GameObject> stars;

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

    #region spriteSort
    void sortVerticesIndexesBySprite(Vector3[] originalArray, float approximation = 0.2f)
    {
        SpriteRenderer renderer = spriteSkin.GetComponent<SpriteRenderer>();
        var verticies = renderer.sprite.vertices;
        int length = originalArray.Length;
        sortedIndexes = new int[length];

        for (int i = 0; i < length; i++)
        {
            sortedIndexes[i] = System.Array.FindIndex(verticies, v => compareVectors(v, originalArray[i], approximation));
        }
    }

    bool compareVectors(Vector2 a, Vector2 b, float approximation)
    {
        return Mathf.Abs(a.x - b.x) < approximation && Mathf.Abs(a.y - b.y) < approximation;
    }

    #endregion

    #region sortByOutline

    List<int> SortIndexesByOutline(List<Vector3> positions)
    {
        List<int> sortedIndexes = new List<int>();

        // Find the convex hull vertices using the Graham's Scan algorithm
        List<Vector3> convexHullVertices = GrahamScanConvexHull(positions);

        // Map the convex hull vertices back to their original indices
        foreach (var vertex in convexHullVertices)
        {
            int index = positions.IndexOf(vertex);
            sortedIndexes.Add(index);
        }

        // Add the remaining vertices to the sorted indexes list
        for (int i = 0; i < positions.Count; i++)
        {
            if (!convexHullVertices.Contains(positions[i]))
                sortedIndexes.Add(i);
        }

        return sortedIndexes;
    }

    List<Vector3> GrahamScanConvexHull(List<Vector3> positions)
    {
        // Sort the positions by their x-coordinate in ascending order
        positions.Sort((a, b) => a.x.CompareTo(b.x));

        // Initialize the convex hull stack
        Stack<Vector3> convexHull = new Stack<Vector3>();
        convexHull.Push(positions[0]);
        convexHull.Push(positions[1]);

        // Iterate through the remaining positions to find the convex hull vertices
        for (int i = 2; i < positions.Count; i++)
        {
            Vector3 top = convexHull.Pop();
            Vector3 nextToTop = convexHull.Peek();

            while (Orientation(nextToTop, top, positions[i]) <= 0)
            {
                if (convexHull.Count < 2)
                    break;

                top = convexHull.Pop();
                nextToTop = convexHull.Peek();
            }

            convexHull.Push(top);
            convexHull.Push(positions[i]);
        }

        return convexHull.ToList();
    }

    // Helper method to calculate the orientation of three points (clockwise, counterclockwise, or collinear)
    int Orientation(Vector3 p, Vector3 q, Vector3 r)
    {
        float val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);

        if (val == 0f)
            return 0; // Collinear
        else if (val > 0f)
            return 1; // Clockwise orientation
        else
            return -1; // Counterclockwise orientation
    }
    #endregion

    #region trianglesMeshSort

    #endregion

    private void OnEnable()
    {
        spline = spriteShapeController.spline;
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

        //sortedIndexes = SortIndexesByOutline(originalVerticesOrder.ToList()).ToArray();
        sortVerticesIndexes(originalVerticesOrder);
        for (int i = 0; i < originalVerticesOrder.Length; i++)
        {
            spline.InsertPointAt(i, Vector3.right * i + Vector3.up * 100f);
            var star = Instantiate(StarPrefab, transform.position, Quaternion.identity, transform);
            EditorUtility.SetDirty(star);
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
