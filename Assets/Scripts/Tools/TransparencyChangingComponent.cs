using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ! Just a script that Ivan used to use from time to time
/// Simply an Interface for MonoBehaviours that adds Visibility Property (from 0 to 1)
/// </summary>
public abstract class TransparencyChangingComponent : MonoBehaviour{


    public abstract float Visibility
    {
        get;
        set;
    }

    void setVisibility(float n)
    {
        Visibility = n;
    }

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        if (!gameObject.activeInHierarchy)
        {
            Visibility = 0f;
        }
    }
}
