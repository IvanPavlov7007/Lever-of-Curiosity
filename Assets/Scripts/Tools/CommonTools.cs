using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ivan's handy lifehack methods for every life situation
/// ...and some comments
/// </summary>
public static class CommonTools
{
    public static float truncateFloat(float orig, float truncate_value = 100f)
    {
        return Mathf.Round(orig * truncate_value) / truncate_value;
    }

    public static bool HitsContainsThisTransform(Transform transform, RaycastHit[] hits, out RaycastHit hit)
    {
        hit = System.Array.Find(hits, obj => obj.transform == transform);
         return hit.transform != null;
    }

    public static Vector3 yPlaneVector(Vector3 vector)
    {
        return new Vector3(vector.x, 0f, vector.z);
    }

    public static Vector3 yPlaneVector(Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 zPlaneVector(Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, 0f);
    }
    public static Vector3 zPlaneVector(Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector3 MaxVector(Vector3 a, Vector3 b)
    {
        if (a.magnitude >= b.magnitude)
            return a;
        return b;
    }

    public static float linearEquation(float x, float k, float b)
    {
        return x * k + b;
    }

    public static float quadraticEquation(float x, float a, float b, float c)
    {
        return x * x * a + x * b + x * c;
    }

    public static Transform[] getChildren(Transform parent)
    {
        int count = parent.childCount;
        Transform[] children = new Transform[count];
        for (int i = 0; i <count; i++)
        {
            children[i] = parent.GetChild(i);
        }
        return children;
    }

    public static Vector3 GetPerpendicularPointFromPointToLine(Vector3 point, Vector3 lineA, Vector3 lineB)
    {
        Vector3 AB = lineB - lineA;
        Vector3 BC = point - lineB;
        float dotB = Vector3.Dot(AB, BC);
        return lineB + AB.normalized * (dotB / AB.magnitude);
    }

    public static float getCurrentAngleRadians(Vector2 direction)
    {
        return Vector2.SignedAngle(Vector3.right, direction) * Mathf.PI * 0.00555f;
    }

    //public static Vector2 GetPerpendicularPointFromPointToLine(Vector2 point, Vector2 lineA, Vector2 lineB)
    //{
    //    Vector2 AB = lineB - lineA;
    //    Vector2 BC = point - lineB;
    //    float dotB = Vector2.Dot(AB, BC);
    //    return lineB + AB.normalized * (dotB / AB.magnitude);
    //}


    public static int RandomIndex(int[] weights, int weightsSum)
    {
        int val = Random.Range(0, weightsSum);
        int j = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            j += weights[i];
            if (val < j)
                return i;
        }
        throw new System.Exception("WeightsSum is not equal to the sum of weights");
    }

    public static int RandomIndex(int[] weights)
    {
        int sum = 0;
        foreach(var weight in weights)
        {
            sum += weight;
        }
        return RandomIndex(weights, sum);
    }

    public static T RandomObject<T>(T[] elements)
    {
        return elements[Random.Range(0, elements.Length)];        
    }

    public static T RandomObject<T>(T[] elements, int[] weights)
    {
        if (weights.Length != elements.Length)
            throw new System.Exception("weights count doesn't match elements count");
        return elements[RandomIndex(weights)];
    }

    public static Vector2 RotateDir(Vector2 dir, float angle)
    {
        float cosA = Mathf.Cos(Mathf.Deg2Rad * angle);
        float sinA = Mathf.Sin(Mathf.Deg2Rad * angle);
        return new Vector2(dir.x * cosA - dir.y * sinA, sinA * dir.x + cosA * dir.y);
    }

    //TODO: improve by finding out b and a:
    /// <summary>
    /// returns smoothed value
    /// </summary>
    /// <param name="x"></param>
    /// <param name="a">The bigger a means the severer curve, especially by 0,5</param>
    /// <returns></returns>
    public static float ClampAtan01(float x, float a)
    {
        return Mathf.Atan((x - 0.5f) * a) * 0.5f / Mathf.Atan(0.5f * a) + 0.5f;
    }


    public static float ClampRepeat(float x,float xMin, float xMax)
    {
        if (x > xMax)
        {
            float span = xMax - xMin;
            x = x - span * Mathf.Floor((x - xMin) / span);
        }
        else if (x < xMin)
        {
            float span = xMax - xMin;
            x = x + span * Mathf.Floor((xMax - x) / span);
        }
        return x;
    }

    //Copy-past this template into your script
    // TO PROVE: NEVER  use static coroutines, because they can't nest 
    static IEnumerator a_to_b_Animation(bool to_b, float speed)
    {
        float n = 0f;
        float result = 0f;
        while (n < 1f)
        {
            n = Mathf.Min(1f, Time.deltaTime * speed + n);
            // Gradually : (int)((to_b ? n : 1f - n) * iterationsCount) / (float)iterationsCount;
            result = (to_b ? n : 1f - n);
            yield return new WaitForEndOfFrame();
        }
    }

    //Changable a_to_b:
    static IEnumerator changable_a_to_b_Animation(bool to_a, float speed)
    {
        // TODO: make to_b instead of to_a
        float n = 0f;
        float result = 0f;
        while ((to_a && n < 1f) || (!to_a && n > 0f))
        {
            if (to_a)
                n = Mathf.Min(1f, n + Time.deltaTime * speed);
            else
                n = Mathf.Max(0f, n - Time.deltaTime * speed);
            result = 1f - n;
            yield return new WaitForEndOfFrame();
        }
    }

    //Rotation around an axis example: 
    //tumblerModelTransform.localRotation = defaultRotation * Quaternion.AngleAxis((rotationAngle *(1f - n)), rotationAxis);
}