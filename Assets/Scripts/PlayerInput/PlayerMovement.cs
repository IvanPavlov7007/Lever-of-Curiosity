using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterMovement characterMovement;

    // Singletone pattern
    private static PlayerMovement instance;
    public static PlayerMovement Instance { get { return instance; } }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        characterMovement.direction = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
    }
}
