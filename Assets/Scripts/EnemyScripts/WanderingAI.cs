using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Enemy/Wander")]

public class WanderingAI : MonoBehaviour
{
    public enum SearchMethod
    {
        WalkToSearch = 0,
        RotateToSearch = 1
    }

    public SearchMethod searchMethod = SearchMethod.WalkToSearch;
    public bool detectionSightEnabled = true;
    public bool detectionListeningEnabled = true;

    [SerializeField] private float speedSearching = 5f;
    [SerializeField] private float speedPursuing = 8f;
    [SerializeField] private float detectionObstacleRange = 5f;
    [SerializeField] private float detectionSightRange = 4f;
    [SerializeField] private float detectionListeningRange = 2f;

    private GameObject player;

    // state management
    private bool movementEnabled;
    private bool foundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        movementEnabled = true;
        foundPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementEnabled) { return; }

        if (detectionSightEnabled) {
            lookForPlayer();
        }

        if (detectionListeningEnabled) {
            listenForPlayer();
        }

        avoidObstacles();

        if (foundPlayer)
        {
            charge();
        }
        else
        {
            search();
        }
    }

    public void setMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;
    }

    public void setPlayer(GameObject player)
    {
        this.player = player;
    }

    private void search()
    {
        switch (searchMethod)
        {
            case SearchMethod.WalkToSearch:
                transform.Translate(0, 0, speedSearching * Time.deltaTime);
                break;

            case SearchMethod.RotateToSearch:
                transform.Rotate(0, speedSearching * Time.deltaTime, 0);
                break;
        }
    }

    private void charge()
    {
        transform.Translate(0, 0, speedPursuing * Time.deltaTime);
    }

    private void lookForPlayer()
    {
        RaycastHit hitPlayer;
        Vector3 boxRaySize = new Vector3(transform.localScale.y, transform.localScale.y, 1);
        bool castToSearchPlayer = Physics.BoxCast(transform.position, boxRaySize, transform.forward, out hitPlayer, Quaternion.LookRotation(transform.forward), detectionSightRange);

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

            if (direction.z < detectionListeningRange)
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
        bool castToSearchWall = Physics.SphereCast(rayWallSearch, transform.localScale.x, out hit, detectionObstacleRange);

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
