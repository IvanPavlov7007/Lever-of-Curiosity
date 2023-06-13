using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Killable : MonoBehaviour
{
    public UnityEvent onDeath;

    public void kill()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }
}
