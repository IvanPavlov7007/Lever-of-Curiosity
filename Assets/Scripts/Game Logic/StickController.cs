using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class StickController : MonoBehaviour
{
    Camera cam;
    Transform angleController, body;

    [SerializeField]
    float groundOffset = 0.10f;
    [SerializeField]
    float stickLenght = 1f;

    [SerializeField]
    LayerMask layerMask;

    Vector3 initModelLocalPos;

    void Start()
    {
        cam = Camera.main;
        angleController = transform.GetChild(0);
        body = angleController.transform.GetChild(0);
        initModelLocalPos = body.transform.localPosition;
    }

    public Vector3 calculateMousePosition()
    {
        var pos = Input.mousePosition;
        return cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, cam.transform.position.y));
    }

    bool mousePressed;

    private void Update()
    {
        Vector3 stickDirection = calculateMousePosition() - transform.position;
        RaycastHit hit;
        if(Physics.Raycast(new Ray(transform.position, stickDirection), out hit, 3f, layerMask))
            angleController.localPosition = new Vector3(0f, stickLenght - hit.distance + groundOffset, 0f);

        transform.up = -stickDirection;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            mousePressed = true;
            Tween.LocalPosition(body, initModelLocalPos + Vector3.down * groundOffset, 0.3f, 0f, Tween.EaseIn);
            Tween.LocalPosition(body, initModelLocalPos, 0.3f, 0.3f, Tween.EaseIn);
        }
    }
}
