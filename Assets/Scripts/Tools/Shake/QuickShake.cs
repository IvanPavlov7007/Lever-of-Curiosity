using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

[RequireComponent(typeof(Shaker))]
public class QuickShake : MonoBehaviour
{
    [SerializeField]
    private ShakePreset shakePreset;
    [SerializeField]
    private ShakeParameters shakeParams;
    Shaker shaker;

    void Start()
    {
        shaker = GetComponent<Shaker>();
    }

    public void Shake()
    {
        StartCoroutine(turnOffShake());
    }

    IEnumerator turnOffShake()
    {
        if (shakePreset != null)
            shaker.Shake(shakePreset);
        else
            shaker.Shake(shakeParams);
        yield return new WaitForSeconds(2f);
        shaker.enabled = false;

    }
}
