using System;
using System.Collections.Generic;
using UnityEngine;

public class StarNode : MonoBehaviour, TriggerRedirectable
{
    LinkedListNode<StarNode> node;
    public int index;

    private void Start()
    {
        
    }

    private void Update()
    {
    }

    public event Action<StarNode> onStarPressed;
    public event Action<StarNode, Collider> onStarTouched;

    public void OnTriggerEnter(Collider other)
    {
        if (onStarTouched != null)
            onStarTouched(this, other);
    }

    public StarConstellation constellation;
    
    public StarNode nextNode, previousNode;
}
