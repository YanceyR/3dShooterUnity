using UnityEngine;
using System.Collections;

public class TrackTarget : MonoBehaviour
{

    [Tooltip("This is the object that the script's game object will look at by default")]
    public GameObject target;

    // Use this for initialization
    void Start()
    {
        if(!this.target)
        {
            this.target = this.gameObject;
            Debug.Log("defaultTarget target not specified. Defaulting to parent GameObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
    }
}
