using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public static PlayerController main;

	public Animator animator;
	//Transform player;
	Rigidbody2D rigid;

//Damage States
	public float HP = 1.0f;

//Movement States
	Vector3 movement;
	public float Speed = 5f;
	//float intSpeed = 5f;
	public float runMultiplyer = 1.7f;
	float actionSpeed;


//Rolling States
	float rollTimer;
	Vector2 savedVel;
	float maxRoll = 0.8f;
	enum RollState{
		Ready,
		Rolling,
		Cooldown
	};
	RollState rollState;

//Jumping States

//Animtion States
	enum dir{ North, South, East, West, NW, NE, SW, SE};
	private dir currentDir = dir.South;

	void Awake(){
		main = this;
	}

	void Start(){
		animator = GetComponent<Animator> ();
		//player = GetComponent<Transform> ();
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	//Movement Control Variables
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

	//Action perameters 
		actionSpeed = Speed * runMultiplyer;

	//Player Movement
		Move (h, v);
	//Movement Animation
		Walking (h, v);
	//Rolling
		Roll (h, v);

	//If currently Rolling the player can't move directions
//		if (rollTimer > 0) {
//			return;
//		};

	//Jumping
		Jumping(h, v);


	//Attacking animation States
		if (Input.GetAxis ("Attack") > 0) {
			animator.SetBool ("Attacking", true);
		} else {
			animator.SetBool ("Attacking", false);
		}


		//Directional Animator controller
		if (Input.GetAxis("Vertical") > 0) {
			currentDir = dir.North;
		}

		if (Input.GetAxis("Vertical") < 0) {
			currentDir = dir.South;
		}

		if (Input.GetAxis("Horizontal") > 0) {
			if(currentDir == dir.North){
				currentDir = dir.NE;
			}else if(currentDir == dir.South){
				currentDir = dir.SE;
			}else{
			currentDir = dir.East;
			}
		}

		if (Input.GetAxis("Horizontal") < 0) {
			if(currentDir == dir.North){
				currentDir = dir.NW;
			}else if(currentDir == dir.South){
				currentDir = dir.SW;
			}else{
			currentDir = dir.West;
			}
		}


	
	//Switch Animator States
		switch(currentDir){
	//Backwards
		case dir.North:
			animator.SetFloat("MoveX", 0f);
			animator.SetFloat("MoveY", 1f);
			break;
	//Forward
		case dir.South:
			animator.SetFloat("MoveX", 0f);
			animator.SetFloat("MoveY", -1f);
			break;
	//Left
		case dir.East:
			animator.SetFloat("MoveX", 1f);
			animator.SetFloat("MoveY", 0f);
			break;
	//Right
		case dir.West:
			animator.SetFloat("MoveX", -1f);
			animator.SetFloat("MoveY", 0f);
			break;
	//Diagonal Backward Right
		case dir.NW:
			animator.SetFloat("MoveX", -1f);
			animator.SetFloat("MoveY", 1f);
			break;
	//Diagonal Backward Left
		case dir.NE:
			animator.SetFloat("MoveX", 1f);
			animator.SetFloat("MoveY", 1f);
			break;
	//Diagonal Forward Left 
		case dir.SE:
			animator.SetFloat("MoveX", 1f);
			animator.SetFloat("MoveY", -1f);
			break;
	//Diagonal Forward Right
		case dir.SW:
			animator.SetFloat("MoveX", -1f);
			animator.SetFloat("MoveY", -1f);
			break;
		}
	}

//Player Movement
	void Move ( float h, float v){
		if (Input.GetAxis ("Run") > 0) {
			bool running = (Mathf.Abs (h) + Mathf.Abs (v)) > 0;
			//bool running = h != 0f || v != 0f;

			animator.SetBool ("Running", running);
			//animator.SetBool("Running", true);

			//actionSpeed = Speed * runMultiplyer;
			runMultiplyer = 1.7f;
		} else {
			animator.SetBool("Running", false);
			//actionSpeed = Speed;
			runMultiplyer = 1.0f;
		}

		//rigid.velocity = new Vector2 (h * actionSpeed, v * actionSpeed);
		rigid.position += new Vector2 (h, v).normalized * actionSpeed * Time.deltaTime;
		//transform.position += new Vector3 (h, v, 0).normalized * actionSpeed * Time.deltaTime;


	}

//Walking Animator Controller method
	void Walking( float h , float v){
		bool walking = (Mathf.Abs (h) + Mathf.Abs (v)) > 0;
		//bool walking = h != 0f || v != 0f;
		animator.SetBool ("Walking", walking);
	}
		

//Dodge Rolling controller method
	void Roll(float h, float v){
		switch (rollState) {
		case RollState.Ready:
		//buttonpress
			if (Input.GetAxis("Roll") > 0) {
				animator.SetBool ("Rolling", true);

				savedVel = rigid.velocity;
				actionSpeed = 800f;
				rigid.velocity += new Vector2 (h, v).normalized * actionSpeed * Time.deltaTime;

			//set sate to rolling
				rollState = RollState.Rolling;
				}
			break;

	//If Currently Rolling	
		case RollState.Rolling:
			rollTimer += Time.deltaTime * 3;

			if(rollTimer >= maxRoll){
				rollTimer = maxRoll;
				rigid.velocity = savedVel;
				rollState = RollState.Cooldown;
			}
			break;
		
		//Rolling Cooldown timer
		case RollState.Cooldown:
			animator.SetBool("Rolling", false);
			rollTimer -= Time.deltaTime;
			if(rollTimer <= 0){
				rollTimer = 0;
				rollState = RollState.Ready;
			}
			break;

		}
	}

	//Jumping Method
	void Jumping(float h, float v){
		
	}

	
}
