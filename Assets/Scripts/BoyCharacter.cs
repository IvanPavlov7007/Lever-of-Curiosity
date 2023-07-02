using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class BoyCharacter : Singleton<BoyCharacter>
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAlive()
    {
        anim.enabled = true;
    }
}
