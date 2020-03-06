using UnityEngine;
using System.Collections;

public class WeaponDisplay : MonoBehaviour {
	GameObject weapon;

	float origonalY;
	public float floatStrength = 0.3f;
	public Animator animator;
	public Animator Character;

	//Animation states
	float mX,mY;


	void Start () {
		animator = GetComponent<Animator> ();
		Character = transform.parent.GetComponentInParent<Animator> ();
		//animator.SetFloat ("MoveY", 1.0f);
		//this.origonalY = this.transform.position.y;
	}
	

	void Update () {
		animator.SetFloat("MoveX", Character.GetFloat ("MoveX"));
		animator.SetFloat ("MoveY", Character.GetFloat ("MoveY"));
		animator.SetBool("Walking", Character.GetBool("Walking"));
		animator.SetBool ("Rolling", Character.GetBool ("Rolling"));
		animator.SetBool ("Attacking", Character.GetBool ("Attacking"));
		animator.SetBool ("Running", Character.GetBool ("Running"));
		animator.SetInteger("Throwing", Character.GetInteger("Throwing"));
		//Bob up and down
		//transform.position = new Vector3 (transform.position.x, origonalY + ((float)Mathf.Sin (Time.time) * floatStrength), transform.position.z);
	}


	//void onTriggerEnter2D (Collider2D other){
		//if (other.tag == "Player") {
			//other.transform.Find ("WeaponSlot").GetComponent<WeaponManager> ().UpdateWeapon (weapon);
		//}
	//}
}
	