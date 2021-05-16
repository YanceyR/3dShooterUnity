using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundHeavy;

public class MouseLook : MonoBehaviour
{
    // public variables, configurable within the unity editor
    public bool lookEnabled = true;

    public RotationAxes axes = RotationAxes.RotationAxisBoth;
    public float sensitivityHorizontal = 3.0f;
    public float sensitivityVertical = 3.0f;

    public bool invertedYAxis = false;
    public float rotationVerticalMinimum = -45f;
    public float rotationVerticalMaximum = 45f;

    public enum RotationAxes
    {
        RotationAxisBoth = 0,
        RotationAxisHorizontal = 1,
        RotationAxisVertical = 2
    }

    // private variables
    private float rotationVertical = 0;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if(body)
        {
            // The affects of game physics should not affect the players body. Such as objects
            // Tumbling on a tilted boat.
            body.freezeRotation = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.lookEnabled) { return; }

        LookWithMouseDirection();
    }

    private void LookWithMouseDirection()
    {
        switch (axes)
        {
            case RotationAxes.RotationAxisHorizontal:
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, GetHorizontalEulerAngle(), 0);
                break;
            case RotationAxes.RotationAxisVertical:
                transform.localEulerAngles = new Vector3(GetVerticalEulerAngle(), transform.localEulerAngles.y, 0);
                break;
            case RotationAxes.RotationAxisBoth:
                transform.localEulerAngles = new Vector3(GetVerticalEulerAngle(), GetHorizontalEulerAngle(), 0);
                break;
        }
    }

    private float GetVerticalEulerAngle()
    {
        // get the amount of rotation from the mouse input and sensitivity
        this.rotationVertical += Input.GetAxis(InputKey.MouseVertical) * sensitivityVertical;

        // The vertical rotation needs to be clamped to prevent the player from
        // rotating upside down.
        this.rotationVertical = Mathf.Clamp(rotationVertical, rotationVerticalMinimum, rotationVerticalMaximum);

        // Flip the angle of the axis if invertedYAxis is disabled.
        return invertedYAxis ? rotationVertical : -rotationVertical;
    }

    private float GetHorizontalEulerAngle()
    {
        float inputHorizontalDelta = Input.GetAxis(InputKey.MouseHorizontal) * sensitivityHorizontal;
        return transform.localEulerAngles.y + inputHorizontalDelta;
    }
}
