using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger2D : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled)
            return;
        var hitable = other.GetComponentInParent<Hitable>();
        if (hitable != null)
        {
            hitable.hit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
