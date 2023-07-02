using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.U2D.IK;

public class BoyCharacter : Singleton<BoyCharacter>, TriggerRedirectable2D
{
    Animator anim;
    LimbSolver2D limbSolver;
    public Transform limbTarget;
    public Transform arm;

    int fallAnimTrigger = Animator.StringToHash("fall");

    private void Start()
    {
        anim = GetComponent<Animator>();
        limbSolver = GetComponentInChildren<LimbSolver2D>();
    }

    public void SetAlive()
    {
        anim.enabled = true;
    }

    public void AllowUseArm()
    {
        StartCoroutine(weightBack());
    }

    IEnumerator weightBack()
    {
        float f = 0f;
        while(f < 1f)
        {
            f += Time.deltaTime;
            limbSolver.weight = f;
            yield return new WaitForEndOfFrame();
        }
        limbSolver.weight = 1f;
    }

    public float timeToGetBack = 2f;

    bool alreadyHit;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyHit)
            return;

        DestroyableWithEffect destroyableWithEffect = other.GetComponentInParent<DestroyableWithEffect>();
        if (destroyableWithEffect != null)
            destroyableWithEffect.DestroyWithEffect();
        alreadyHit = true;
        StarRunnerManager.Instance.OnPlayerHit();
        anim.SetTrigger(fallAnimTrigger);
        limbSolver.weight = 0f;
        Run.After(timeToGetBack, ()=> { AllowUseArm(); alreadyHit = false; });
    }
}
