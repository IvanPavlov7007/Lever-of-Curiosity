using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
public class ParticlesManager : Singleton<ParticlesManager>
{
    public GameObject particlesSystemPrefab;
    public void ShowParticles(Transform position)
    {
        Instantiate(particlesSystemPrefab, position.position, Quaternion.identity);
    }
}
