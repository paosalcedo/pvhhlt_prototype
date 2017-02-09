using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject enemyPrefab;
	private Vector2 rightPos;
	private Vector2 leftPos;
	private Vector2 midPos;
	public bool ifLeft;
	public bool ifMid; //added mid bool
	public float spawnTime;
	private const float SPAWNTIME_MAX = 5f;
	private const float SPAWNTIME_MIN = 0f;

	// Use this for initialization
	void Start () {
		rightPos = new Vector2 (4.81f, 5.94f);
		leftPos = new Vector2 (-4.81f, 5.94f);
		midPos = new Vector2 (0f, 5.94f);
		//SpawnEnemy ();
	}
	
	// Update is called once per frame
	void Update () {
		spawnTime -= Time.deltaTime;
		if (spawnTime <= SPAWNTIME_MIN) {
			SpawnEnemy ();
			spawnTime = SPAWNTIME_MAX;
		}
	}

	public void SpawnEnemy(){
		Debug.Log ("spawn enemy!");
		float rand = Random.Range (0.0f, 1f);
		Vector2 nowPos;
		if (rand < 0.33f) {
			nowPos = rightPos;
			ifLeft = false;
		} else if (rand >= 0.33f && rand < 0.67f) {
			nowPos = midPos;
			ifMid = true;
		} else {
			nowPos = leftPos;
			ifLeft = true;
		}
		//Clone of the bullet
		GameObject Clone;
		//spawning the bullet at position
		Clone = (Instantiate(enemyPrefab, nowPos,transform.rotation)) as GameObject;

		if (ifLeft) {
			Clone.transform.localRotation = Quaternion.Euler (0, 180, 0);
		}
	}
}
