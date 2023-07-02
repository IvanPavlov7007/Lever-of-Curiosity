using System;
using System.Collections.Generic;
using UnityEngine;

public class StarNode : MonoBehaviour, TriggerRedirectable
{
    LinkedListNode<StarNode> node;
    public int number;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(number.ToString()))
        {
            if (onStarPressed != null)
                onStarPressed(this);
        }    
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
