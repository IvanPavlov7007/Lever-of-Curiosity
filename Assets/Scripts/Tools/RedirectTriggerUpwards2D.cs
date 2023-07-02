using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectTriggerUpwards2D : MonoBehaviour
{
    TriggerRedirectable2D triggerRedirectable;
    private void Start()
    {
        triggerRedirectable = GetComponentInParent<TriggerRedirectable2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggerRedirectable.OnTriggerEnter2D(other);
    }
}

public interface TriggerRedirectable2D
{
    public void OnTriggerEnter2D(Collider2D other);
}