using UnityEngine;
using System.Collections;
using SoundHeavy;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            Debug.Log("shot something");
        }
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.rotation = transform.rotation;
        projectile.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
    }
}
