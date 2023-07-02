using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarStickTarget : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = calculateMousePosition().y;
        transform.position = pos;
    }

    public Vector3 calculateMousePosition()
    {
        var pos = Input.mousePosition;
        return cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 3f));
    }
}
