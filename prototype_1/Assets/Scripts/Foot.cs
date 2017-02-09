using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour {

	private Animator animator;
	private Rigidbody2D rb;
	private BoxCollider2D bc;
	public float Speed = 10f;
	public float MaxJumpTime = 2f;
	public float JumpForce;
	private float move = 0f;
	private float JumpTime = 0f;
	private bool CanJump;
	private bool combined;

	void Start () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		bc = GetComponent<BoxCollider2D> ();
		JumpTime  = MaxJumpTime;
		combined = false;
	}


	void Update ()
	{
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

	void FixedUpdate () {

		if (Input.GetButton ("FootLeft")) {
			rb.velocity = new Vector2 (-1f * Speed, rb.velocity.y);
			transform.localRotation = Quaternion.Euler(0, 180, 0);
			animator.SetTrigger ("footMove");
		}

		if (Input.GetButtonUp ("FootLeft")) {
			rb.velocity = new Vector2 (0,0);
		}

		if (Input.GetButton ("FootRight")) {
			rb.velocity = new Vector2 (1f * Speed, rb.velocity.y);
			transform.localRotation = Quaternion.Euler(0, 0, 0);
			animator.SetTrigger ("footMove");
		}

		if (Input.GetButtonUp ("FootRight")) {
			rb.velocity = new Vector2 (0,0);
		}

		if (Input.GetButton ("FootJump")  && CanJump)
		{
			animator.SetTrigger ("footJump");
			rb.AddForce(Vector2.up * JumpForce);
			CanJump = false;
			JumpTime  = MaxJumpTime;
		}
	}

	public void SetCombined() {
		combined = true;
	}

	public void UnsetCombined() {
		combined = false;
	}


}
