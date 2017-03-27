using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {

	public int hitPoints = 2;
	public Sprite damagedSprite;
	public float damageImpactSpeed;

	private int currentHitPoints;
	private float damageImpactSpeedSqr;
	private SpriteRenderer spriteRenderer;

	public ParticleSystem particleDeadPuff;
	private AudioSource killSound;

	public GameManager gameManager;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		currentHitPoints = hitPoints;
		damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
		killSound = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.tag != "Damager") {
			return;
		}
		if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr) {
			return;
		}

		spriteRenderer.sprite = damagedSprite;
		currentHitPoints--;

		if (currentHitPoints <= 0) {
			Kill ();
		}
	}

	void Kill(){
		killSound.Play ();
		particleDeadPuff.Play ();
		particleDeadPuff.transform.SetParent (null);
		spriteRenderer.enabled = false;
		GetComponent<Collider2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().isKinematic = true;
		gameManager.BirdKilled ();
	}

	// Update is called once per frame
	void Update () {
		
	}


}
