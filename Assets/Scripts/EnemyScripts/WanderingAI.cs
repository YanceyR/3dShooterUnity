using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
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

        lookForPlayer();

        if(!foundPlayer)
        {
            listenForPlayer();
            avoidObstacles();
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
        Vector3 boxRaySize = new Vector3(transform.localScale.y / 2, transform.localScale.y / 2, playerDetectionSightRange / 2);
        bool castToSearchPlayer = Physics.BoxCast(transform.position, boxRaySize, transform.forward, out hitPlayer);

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
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, 0.7f);

                run();
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
            Vector3 _direction = (player.gameObject.transform.position - transform.position).normalized;

            if (_direction.z < playerDetectionListeningRange)
            {
                //create the rotation we need to be in to look at the target
                Quaternion _lookRotation = Quaternion.LookRotation(_direction);

                //rotate us over time according to speed until we are in the required rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, 0.3f);
            }
        }
    }

    private void avoidObstacles()
    {
        Ray rayWallSearch = new Ray(transform.position, transform.forward);
        bool castToSearchWall = Physics.SphereCast(rayWallSearch, transform.localScale.x, obstacleDetectionRange);

        if (castToSearchWall)
        {
            float rotateHorizontalAngle = Random.Range(-110, 110);
            transform.Rotate(0, rotateHorizontalAngle, 0);
        }

        walk();
    }
}
