using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

	GameObject enemyCtrl;
	GameObject enemyMgr;
	SpriteRenderer enemySprite;
	GameObject deathEffect;
	ParticleSystem deathParticles;

	public GameObject enemyManager;
	public float speed;
	// Use this for initialization
	Rigidbody2D rb;
	bool isGrounded;

	void Start () {
		enemySprite = GetComponent<SpriteRenderer> ();
		deathEffect = GameObject.Find("DeathEffect");
		deathParticles = deathEffect.GetComponent<ParticleSystem> ();

		isGrounded = false;
		enemyCtrl = GameObject.Find ("EnemyManager");
		rb = GetComponent<Rigidbody2D> ();
		deathEffect.SetActive (true);

	}
	
	// Update is called once per frame
	void Update () {

		if (isGrounded) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
		//if enemy landed on right, move to the left.
//		if (enemyCtrl.GetComponent<EnemyController>().ifLeft == false && isGrounded) {
//			//rb.AddForce (Vector2.left * speed * Time.deltaTime);
//			transform.Translate (Vector2.left * speed * Time.deltaTime);
//			Debug.Log ("landed on right");
//		}
//		//if enemy landed on the left, move to the right.
//		if (enemyCtrl.GetComponent<EnemyController>().ifLeft == true && isGrounded) {
//			//rb.AddForce (Vector2.right * speed * Time.deltaTime);
//			transform.Translate (Vector2.right * speed * Time.deltaTime);
//			Debug.Log ("landed on left");
//
//		}
//		//if enemy landed mid, randomly move left or right.
//		if (enemyCtrl.GetComponent<EnemyController>().ifMid == true && isGrounded) {
//			float prob;
//			prob = Random.Range (0f, 1f);
//			if (prob >= 0.51f) {			
//				transform.Translate (Vector2.right * speed * Time.deltaTime);
//				//rb.AddForce (Vector2.right * speed * Time.deltaTime); 
//			} else {
//				transform.Translate (Vector2.left * speed * Time.deltaTime);
//				//rb.AddForce (Vector2.left * speed * Time.deltaTime);
//			}
//			Debug.Log ("landed mid");
//		}

	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Foot" || coll.gameObject.tag == "Head") {
//			Destroy (coll.gameObject);	
			//SceneManager.LoadScene("game");
			//Application.LoadLevel (0);
		}

		if (coll.gameObject.tag == "Bullet") {
			//GameObject.Find("EnemyManager").SendMessage("SpawnEnemy");
			Destroy (coll.gameObject);
			BulletEffect ();
			//Destroy (gameObject);
		}

		//Vivi's original code that kills enemy upon landing
//		if (coll.gameObject.tag == "Wall") {
//			GameObject.Find("EnemyManager").SendMessage("SpawnEnemy");
//			Destroy (gameObject);
//		}

		//trying to move enemy.
		if (coll.gameObject.tag == "Wall") {
			Debug.Log ("Touched ground!");
			isGrounded = true; 
		}

		//kills the enemy when in contact with left or right walls
		if (coll.gameObject.name == "Wall_Left" || coll.gameObject.name == "Wall_Right") {
			speed *= -1f;
			//Destroy (gameObject);
		}
	}

	void BulletEffect()
	{
		enemySprite.enabled = false;
		deathParticles.Play ();
		Invoke ("DelayedDeath", 5f);	
	}

	void DelayedDeath()
	{
		Destroy(gameObject);
	}
}

