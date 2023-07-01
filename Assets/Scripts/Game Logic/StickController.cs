using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class StickController : MonoBehaviour
{
    Camera cam;
    Transform distanceController, body;

    [SerializeField]
    float groundOffset = 0.10f;
    [SerializeField]
    float stickPhysicalHalfLength = 1f;

    public float distanceToGround;

    [SerializeField]
    LayerMask layerMask;

    Vector3 initBodyPos;

    void Start()
    {
        cam = Camera.main;
        distanceController = transform.GetChild(0);
        body = distanceController.transform.GetChild(0);
        initBodyPos = body.transform.localPosition;
    }

    public Vector3 calculateMousePosition()
    {
        var pos = Input.mousePosition;
        return cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, distanceToGround));
    }

    bool mousePressed;
    float pressTime = 0.1f;
    float timer;
    float lerpPress;

    private void Update()
    {
        Vector3 stickDirection = calculateMousePosition() - transform.position;
        RaycastHit hit;
        if(Physics.Raycast(new Ray(transform.position, stickDirection), out hit, 3f, layerMask))
            distanceController.localPosition = new Vector3(0f, stickPhysicalHalfLength - hit.distance + groundOffset, 0f);

        transform.up = -stickDirection;

        if (Input.GetMouseButtonDown(0))
        {
            mousePressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
        }

        if (mousePressed)
            timer = Mathf.Min(timer + Time.deltaTime, pressTime);
        else
            timer = Mathf.Max(timer - Time.deltaTime, 0f);

        body.localPosition = initBodyPos + Vector3.down * groundOffset * Mathf.InverseLerp(0f, pressTime, timer);
    }
}
