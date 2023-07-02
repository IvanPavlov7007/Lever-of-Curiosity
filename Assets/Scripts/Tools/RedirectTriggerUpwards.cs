using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectTriggerUpwards : MonoBehaviour
{
    TriggerRedirectable triggerRedirectable;
    private void Start()
    {
        triggerRedirectable = GetComponentInParent<TriggerRedirectable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerRedirectable.OnTriggerEnter(other);
    }
}

public interface TriggerRedirectable
{
    public void OnTriggerEnter(Collider other);
}