using UnityEngine;

public class RotateAround : MonoBehaviour
{
	[Tooltip("Object that the script's game object will rotate around")]
	public Transform target; // the object to rotate around
	[Tooltip("Speed at which the object rotates")]
	public int speed; // the speed of rotation

	void Start()
	{
		if (target == null)
		{
			target = this.gameObject.transform;
			Debug.Log("RotateAround target not specified. Defaulting to this GameObject");
		}
	}


	// Update is called once per frame
	void FixedUpdate()
	{
		transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
	}
}
