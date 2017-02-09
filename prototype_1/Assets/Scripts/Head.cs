using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {
	private Animator animator;
	private Rigidbody2D rb;
	private BoxCollider2D bc;
	private GameObject foot;
	public float Speed = 10f;
	public float MaxJumpTime = 2f;
	public float JumpForce;
	private float move = 0f;
	private float JumpTime = 0f;
	private bool CanJump;
	public bool combined;
	private bool touchFoot = false;
	private float dist = 0.19f;
	public GameObject bulletPrefab;
	public ParticleSystem child;


	void Start () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		bc = GetComponent<BoxCollider2D> ();
		JumpTime  = MaxJumpTime;
		combined = false;
		foot = GameObject.FindGameObjectWithTag ("Foot");
		child = GetComponentInChildren<ParticleSystem> ();
		child.Pause ();
	}


	void Update ()
	{
		if (!combined) {
			if (!CanJump) {
				JumpTime  -= Time.deltaTime;
				//			if (rb.velocity.y <= 0f) {
				//				bc.enabled = true;
				//			} else {
				//				bc.enabled = false;
				//			}
			}

			if (JumpTime <= 0)
			{
				CanJump = true;
				JumpTime  = MaxJumpTime;
			}
		}
	}

	void FixedUpdate () {
		if (!combined) {
			if (Input.GetButton ("HeadLeft")) {
				rb.velocity = new Vector2 (-1f * Speed, rb.velocity.y);
				transform.localRotation = Quaternion.Euler (0, 180, 0);
				animator.SetTrigger ("headMove");
			}

			if (Input.GetButtonUp ("HeadLeft")) {
				rb.velocity = new Vector2 (0, 0);
				dist = -0.19f;
			}

			if (Input.GetButton ("HeadRight")) {
				rb.velocity = new Vector2 (1f * Speed, rb.velocity.y);
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				animator.SetTrigger ("headMove");
			}

			if (Input.GetButtonUp ("HeadRight")) {
				rb.velocity = new Vector2 (0, 0);
				dist = 0.19f;
			}

			if (Input.GetButton ("HeadJump") && CanJump) {
				animator.SetTrigger ("headJump");
				rb.AddForce (Vector2.up * JumpForce);
				CanJump = false;
				JumpTime = MaxJumpTime;
			}

			if (Input.GetButtonDown ("HeadCombine") && Input.GetButtonDown ("FootCombine")) {
				if ((transform.position.y > foot.transform.position.y) && Mathf.Abs (transform.position.x - foot.transform.position.x) < 0.3f) {
					combined = true;
					foot.GetComponent<Foot> ().SendMessage ("SetCombined");
					bc.enabled = false;
					rb.Sleep ();
					child.Play ();
				}
			}
		} else {
			transform.position = new Vector2(foot.transform.position.x + dist, foot.transform.position.y + 0.7f);
			if (Input.GetButton ("HeadLeft")) {
				transform.localRotation = Quaternion.Euler (0, 180, 0);
				dist = -0.19f;
			}

			if (Input.GetButton ("HeadRight")) {
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				dist = 0.19f;
			}

			if (Input.GetButtonDown ("HeadShoot")) {
				animator.SetTrigger ("headMove");
				if (Mathf.Abs(transform.localRotation.y) != Mathf.Abs(foot.transform.localRotation.y)) {
					combined = false;
					bc.enabled = true;
					rb.WakeUp ();
					foot.GetComponent<Foot> ().SendMessage ("UnsetCombined");
				} else {
					FireBullet ();
				}
			}
				
		
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Foot") {
			touchFoot = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.tag == "Foot") {
			touchFoot = false;
		}
	}

	void FireBullet(){
		//Clone of the bullet
		GameObject Clone;
		//spawning the bullet at position
		Clone = (Instantiate(bulletPrefab, transform.position,transform.rotation)) as GameObject;
		//add force to the spawned objected
		if (dist == -0.19f) {
			Clone.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * 1000);
		} else {
			Clone.GetComponent<Rigidbody2D> ().AddForce(Vector2.right*1000);
		}
	}


}
