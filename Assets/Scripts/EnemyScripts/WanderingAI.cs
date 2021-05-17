using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    // FIXME: any faster causes object to translate past wall
    public float speed = .3f;
    public float obstacleDetectionRange = 5f;

    // state management
    private bool movementEnabled;

    // Start is called before the first frame update
    void Start()
    {
        movementEnabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!movementEnabled) { return; }

        Wander();
    }

    private void Wander()
    {
        // move forward by speed amount
        transform.Translate(0, 0, speed);

        Ray movingDirectionRay = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        // look what's in front of you
        if (Physics.SphereCast(movingDirectionRay, transform.localScale.x, out hit))
        {
            // get an angle to turn at
            float rotateHorizontalAngle = Random.Range(-110, 110);

            // if the thing in front of you is close, turn in another direction
            if (hit.distance < obstacleDetectionRange)
            {
                transform.Rotate(0, rotateHorizontalAngle, 0);
            }
        }
    }

    public void setMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;
    }
}
