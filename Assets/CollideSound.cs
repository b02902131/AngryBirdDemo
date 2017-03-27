using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideSound : MonoBehaviour {

	public AudioSource collideSound;
	private float v_ratio = 2.5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		float magnitude = Mathf.Log10(coll.relativeVelocity.sqrMagnitude);
		collideSound.PlayOneShot (collideSound.clip, Mathf.Clamp(magnitude/v_ratio, 0f, 1f) );
	}
}
