using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter(Collider other)
    {
        if (!enabled)
            return;
        var hitable = other.GetComponentInParent<Hitable>();
        if (hitable != null)
        {
            hitable.hit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enabled)
            return;
        var hitable = collision.gameObject.GetComponentInParent<Hitable>();
        if (hitable != null)
        {
            hitable.hit();
        }
    }
}
