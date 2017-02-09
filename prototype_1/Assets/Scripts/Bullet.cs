using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	AudioSource bang;

	// Use this for initialization
	void Start () {
		bang = GetComponent<AudioSource> ();
		bang.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Wall") {
			Destroy (gameObject);
		}
	}


}
