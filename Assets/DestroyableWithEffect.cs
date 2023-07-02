using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWithEffect : MonoBehaviour
{
    public void DestroyWithEffect()
    {
        ParticlesManager.Instance.ShowParticles(transform);
        Destroy(gameObject);
    }
}
