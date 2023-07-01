using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class StickController : MonoBehaviour
{
    Camera cam;
    Transform distanceController, body;
    Rigidbody rb;

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
        rb = GetComponentInChildren<Rigidbody>();
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

    Vector3 stickDirection;

    private void Update()
    {
        #region inputAndLerpCalculation
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
        lerpPress = 1f - Mathf.InverseLerp(0f, pressTime, timer);
        //body.localPosition = initBodyPos + Vector3.down * groundOffset * Mathf.InverseLerp(0f, pressTime, timer);
        #endregion


        stickDirection = calculateMousePosition() - transform.position;

        
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, stickDirection), out hit, 10f, layerMask))
        {
            //distanceController.localPosition = new Vector3(0f, stickPhysicalHalfLength - hit.distance + groundOffset, 0f);
            //transform.up = -stickDirection;

            Vector3 direction = hit.point - transform.position;
            rb.rotation = Quaternion.LookRotation(direction.normalized, transform.forward);
            rb.position = transform.position + direction.normalized * (direction.magnitude - stickPhysicalHalfLength - groundOffset * lerpPress);
        }
    }
}
