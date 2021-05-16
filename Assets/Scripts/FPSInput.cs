using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundHeavy;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Movement Script/FPS Input")]

public class FPSInput : MonoBehaviour
{
    public bool movementEnabled = true;
    public float movementSpeed = 3f;
    public float gravity = -9.8f;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        this.characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementEnabled) { return; }
        
        float movementDeltaHor = Input.GetAxis(InputKey.MovementHorizontal) * movementSpeed;
        float movementDeltaVer = Input.GetAxis(InputKey.MovementVertical) * movementSpeed;

        Vector3 movement = new Vector3(movementDeltaHor, 0, movementDeltaVer);

        // limit movement speed to same speed as moving along an axis. The speed of
        // the diagnol movement would be greater. Think of the hypotenuse on a triangle.
        movement = Vector3.ClampMagnitude(movement, movementSpeed);

        // Keeps the player on the ground.
        movement.y += gravity;

        // adjusts the movement speed to be frame rate independent.
        movement *= Time.deltaTime;

        // converts the local vector to the same direction using global coordinates since the
        // charCont.Move methods needs a global vector.
        movement = transform.TransformDirection(movement);

        // allows movement of character with collisions without dealing with rigidBody
        this.characterController.Move(movement);
    }
}
