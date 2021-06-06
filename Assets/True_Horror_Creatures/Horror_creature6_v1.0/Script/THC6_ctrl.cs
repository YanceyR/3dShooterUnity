using UnityEngine;
using System.Collections;

public class THC6_ctrl : MonoBehaviour {
	
	
	private Animator anim;
	private CharacterController controller;
	private int battle_state = 0;
	public float speed = 6.0f;
	public float runSpeed = 3.0f;
	public float turnSpeed = 60.0f;
	public float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private float w_sp = 0.0f;
	private float r_sp = 0.0f;

	int inBattleHash = Animator.StringToHash("battle");
	int isMovingHash = Animator.StringToHash("moving");


	// Use this for initialization
	void Start () 
	{						
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
		w_sp = speed; //read walk speed
		r_sp = runSpeed; //read run speed
		battle_state = 0;
		runSpeed = 1;

	}
	
	// Update is called once per frame
	void Update () 
	{


		if (Input.GetKey ("1"))  // turn to still state
		{ 		
			anim.SetInteger (inBattleHash, 0);
			battle_state = 0;
			runSpeed = 1;
		}
		if (Input.GetKey ("2")) // turn to battle state
		{ 
			anim.SetInteger (inBattleHash, 1);
			battle_state = 1;
			runSpeed = r_sp;
		}
			
		if (Input.GetKey ("up")) 
		{
			anim.SetInteger (isMovingHash, 1);//walk/run/moving
		}
		else 
			{
				anim.SetInteger (isMovingHash, 0);
			}


		if (Input.GetKey ("down")) //walkback
		{
			anim.SetInteger (isMovingHash, 12);
			runSpeed = 1;
		}
		if (Input.GetKeyUp ("down")) 
		{
			if (battle_state == 0) runSpeed = 1;
			else if (battle_state >0) runSpeed = r_sp;
		}
	
		if (Input.GetMouseButtonDown (0)) { // attack1
			anim.SetInteger (isMovingHash, 2);
		}
		if (Input.GetMouseButtonDown (1)) { // attack2
			anim.SetInteger (isMovingHash, 3);
		}
		if (Input.GetMouseButtonDown (2)) { // attack3
			anim.SetInteger (isMovingHash, 4);
		}
		if (Input.GetKeyDown ("space")) { //jump
			anim.SetInteger (isMovingHash, 6);
		}
		if (Input.GetKeyDown ("c")) { //roar/howl
			anim.SetInteger (isMovingHash, 7);
		}
		if (Input.GetKeyDown ("y")) { //powerhit
			anim.SetInteger (inBattleHash, 1);
			battle_state = 1;
			runSpeed = r_sp;
			anim.SetInteger (isMovingHash, 5);
		}


		
		if (Input.GetKeyDown ("u")) //hit
		{ 			  
			battle_state = 1;
			runSpeed = r_sp;
			anim.SetInteger (inBattleHash, 1);
				
			int n = Random.Range (0, 2);
			if (n == 1) 
				{
					anim.SetInteger (isMovingHash, 8);
				} 
			else 
				{
					anim.SetInteger (isMovingHash, 9);
				}
		}

		if (Input.GetKeyDown ("k")) { //rising
			anim.SetInteger (inBattleHash, 1);
			battle_state = 1;
			runSpeed = r_sp;
			anim.SetInteger (isMovingHash, 15);
		}


		
		if (Input.GetKeyDown ("i")) anim.SetInteger (isMovingHash, 13); //die/fall
		if (Input.GetKeyDown ("o")) anim.SetInteger (isMovingHash, 14); //die2
		

		if (controller.isGrounded) 
		{
			moveDirection=transform.forward * Input.GetAxis ("Vertical") * speed * runSpeed;
			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);						
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
		}
}



