using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRunnerManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public BoyCharacter boyCharacter;

    public void StarRunner()
    {
        backgroundMusic.Play();
        boyCharacter.SetAlive();
    }
}
