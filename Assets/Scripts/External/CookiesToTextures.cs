using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimateCookieTexture))]
[ExecuteInEditMode]
public class CookiesToTextures : MonoBehaviour
{
    AnimateCookieTexture animateCookieTexture;

    private void OnEnable()
    {
        if (Application.isPlaying)
            return;
        animateCookieTexture = GetComponent<AnimateCookieTexture>();
        var array = Resources.LoadAll<Texture2D>("Textures/cookie12");
        animateCookieTexture.textures = array;
    }
}
