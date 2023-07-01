using System;
using System.Collections.Generic;
using UnityEngine;

public class StarNode : MonoBehaviour
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

    public void OnCollisionEnter(Collision collision)
    {
        
    }
    public StarConstellation constellation;
    
    public StarNode nextNode, previousNode;
}
