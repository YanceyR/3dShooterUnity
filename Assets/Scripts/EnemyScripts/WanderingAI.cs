using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public bool detectionSightEnabled = true;
    public bool detectionListeningEnabled = true;

    // FIXME: any faster causes object to translate past wall
    [SerializeField] private float speedWandering = .05f;

    // Speed mulitplier when ai is locked on player.
    [SerializeField] private float speedTowardPlayerMultiplier = 3f;
    [SerializeField] private float obstacleDetectionRange = 5f;
    [SerializeField] private float playerDetectionSightRange = 4f;
    [SerializeField] private float playerDetectionListeningRange = 2f;

    public GameObject player;

    // state management
    private bool movementEnabled;
    private bool foundPlayer;

    public void setMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        movementEnabled = true;
        foundPlayer = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!movementEnabled) { return; }

        if (detectionSightEnabled) {
            //avoidObstacles();
            lookForPlayer();
            //avoidObstacles();
        }

        if (!foundPlayer)
        { 
            if (detectionListeningEnabled) { listenForPlayer(); }

            avoidObstacles();
        }
    }

    private void LateUpdate()
    {
        if (foundPlayer)
        {
            run();
        } else
        {
            walk();
        }
    }

    private void walk()
    {
        transform.Translate(0, 0, speedWandering);
    }

    private void run()
    {
        transform.Translate(0, 0, speedWandering * speedTowardPlayerMultiplier);
    }

    private void lookForPlayer()
    {
        RaycastHit hitPlayer;
        Vector3 boxRaySize = new Vector3(transform.localScale.y, transform.localScale.y, 1);
        bool castToSearchPlayer = Physics.BoxCast(transform.position, boxRaySize, transform.forward, out hitPlayer, Quaternion.LookRotation(transform.forward), playerDetectionSightRange);

       if (castToSearchPlayer)
        {
            CharacterController player = hitPlayer.transform.GetComponent<CharacterController>();
            if (player)
            {
                 foundPlayer = true;

                //find the vector pointing from our position to the target
                Vector3 _direction = (player.gameObject.transform.position - transform.position).normalized;

                _direction.y = 0;

                //create the rotation we need to be in to look at the target
                Quaternion _lookRotation = Quaternion.LookRotation(_direction);

                //rotate us over time according to speed until we are in the required rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, .3f);
            } else {
                foundPlayer = false;
            }
        }
    }

    private void listenForPlayer()
    {
        if (player)
        {
            //find the vector pointing from our position to the target
            Vector3 direction = (player.gameObject.transform.position - transform.position);

            if (direction.z < playerDetectionListeningRange)
            {
                foundPlayer = true;

                direction.Normalize();
                direction.y = 0;

                //create the rotation we need to be in to look at the target
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                //rotate us over time according to speed until we are in the required rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.2f);
            }
        }
    }

    private void avoidObstacles()
    {
        RaycastHit hit;
        Ray rayWallSearch = new Ray(transform.position, transform.forward);
        bool castToSearchWall = Physics.SphereCast(rayWallSearch, transform.localScale.x/2, out hit, obstacleDetectionRange);

        if (castToSearchWall)
        {
            CharacterController player = hit.transform.GetComponent<CharacterController>();
            if (!player)
            {
                foundPlayer = false;

                float rotateHorizontalAngle = Random.Range(-150, 150);

                Quaternion rotationDelta = Quaternion.Euler(0, rotateHorizontalAngle, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotationDelta, 1f);
            }
        }
    }
}
