using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class StarRunnerManager : Singleton<StarRunnerManager>
{
    public AudioSource backgroundMusic;
    public BoyCharacter boyCharacter;
    public StickController stickController;
    public StarConstellation starStick;

    public Vector3 origStickTargetLocalPos, origStickTargetLocalRotation, origStickTargetLocalScale;
    public Vector3 starStickLocalPos, starStickLocalRotation;

    public Transform firstStickConstellationTarget;
    public BulletsSpawner bulletsSpawner;

    private void Start()
    {
        
    }

    public void OnPlayerHit()
    {
        bulletsSpawner.PauseSpawn();
        Run.After(5f, bulletsSpawner.StartSpawn);
    }

    public void StarRunner()
    {
        backgroundMusic.Play();
        boyCharacter.SetAlive();
        Run.After(3f, ShowStickConstellation);
    }

    public void ShowStickConstellation()
    {
        Tween.Position(starStick.transform, firstStickConstellationTarget.position, 1f, 0f);
    }

    public void onStickConstellationComplete()
    {
        stickController.enabled = false;
        animateStickTransformation();
        Run.After(3f, () => placeStarStick());
    }

    void placeStarStick()
    {
        Transform arm = boyCharacter.arm;
        starStick.transform.parent = arm;
        float trt = 1f;
        Tween.LocalPosition(starStick.transform, starStickLocalPos, trt, 0f);
        Tween.LocalRotation(starStick.transform, starStickLocalRotation, trt, 0f);
        Run.After(trt + 0.5f, boyCharacter.AllowUseArm);
        Run.After(trt + 4f, bulletsSpawner.StartSpawn);
    }

    void animateStickTransformation()
    {
        stickController.enabled = false;
        Transform orig = stickController.transform;
        orig.parent = starStick.transform;

        float trt = 1f;
        Tween.LocalPosition(orig, origStickTargetLocalPos, trt, 0f);
        Tween.LocalRotation(orig, origStickTargetLocalRotation, trt, 0f);
        Tween.LocalScale(orig, origStickTargetLocalScale, trt, 0f);
        Run.After(trt + 1f,()=> { stickController.gameObject.SetActive(false); });
    }
}
