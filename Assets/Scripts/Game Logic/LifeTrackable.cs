using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTrackable : MonoBehaviour
{
    public event System.Action<GameObject> onDestroy;
    private void OnDestroy()
    {
        onDestroy(gameObject);
    }
}
