using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour {

	public float maxStretch = 3.0f;
	private float maxStretchSqr;
	public LineRenderer lineFront;
	public LineRenderer lineBack;

	private Rigidbody2D rb2d;
	public SpringJoint2D spring;
	private bool isDrag;

	public Transform catapult;
	private Ray rayToMouse;
	private Ray leftCatapultToProjectile;
	private CircleCollider2D circle;
	private float circleRadius;

	private Vector2 preVelocity;
	public AudioSource shootSound;
	public GameManager gameManager;

	private bool isShot = false;

	// Use this for initialization
	void Start () {
		maxStretchSqr = maxStretch * maxStretch;
		rb2d = GetComponent<Rigidbody2D> ();
		rayToMouse = new Ray (catapult.position, Vector3.zero);
		leftCatapultToProjectile = new Ray (lineFront.transform.position, Vector3.zero);
		circle = GetComponent<CircleCollider2D> ();
		circleRadius = circle.radius;
		LineRendererSetup ();
		shootSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDrag){
			Dragging ();
		}
		if (spring != null) {
			if (!rb2d.isKinematic && preVelocity.sqrMagnitude > rb2d.velocity.sqrMagnitude){
				Destroy (spring);
				rb2d.velocity = preVelocity;
				//shot
				PlayShootSound();
				gameManager.ballShoot ();
				isShot = true;
				lineFront.enabled = false;
				lineBack.enabled = false;
			}
			if (!isDrag) {
				preVelocity = rb2d.velocity;
			}
			LineRendererUpdate();
		} else {
		}

		if (isShot) {
			CheckOver ();
		}
	}

	void PlayShootSound(){
		shootSound.Play ();
	}

	void LineRendererSetup(){
		lineFront.SetPosition (0, lineFront.transform.position);
		lineBack.SetPosition (0, lineBack.transform.position);

		lineFront.sortingLayerName = "Asteroid";
		lineBack.sortingLayerName = "Asteroid";

		lineFront.sortingOrder = 3;
		lineBack.sortingOrder = 1;
	
	}

	void OnMouseDown(){
		spring.enabled = false;
		isDrag = true;
	}

	void OnMouseUp(){
		spring.enabled = true;
		rb2d.isKinematic = false;
		isDrag = false;
	}

	void Dragging(){
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 catapultToMouse = mousePos - catapult.position;

		if ( catapultToMouse.sqrMagnitude > maxStretchSqr ) {
			rayToMouse.direction = catapultToMouse;
			mousePos = rayToMouse.GetPoint (maxStretch);
		}
		mousePos.z = 0f;
		transform.position = mousePos;
	}

	void LineRendererUpdate(){
		Vector2 catapultToProjectile = transform.position - lineFront.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;
		Vector3 holdPint = leftCatapultToProjectile.GetPoint (catapultToProjectile.magnitude + circleRadius);
		lineFront.SetPosition (1, holdPint);
		lineBack.SetPosition (1, holdPint);
	}

	public void Standby(){
		spring.enabled = false;
		this.enabled = false;
	}

	public void Reload(){
		spring.enabled = true;
		transform.position = catapult.transform.position;

		lineFront.enabled = true;
		lineBack.enabled = true;
	}

	void CheckOver(){
		if (rb2d.velocity.sqrMagnitude < 0.01f) {
			this.gameObject.SetActive (false);
			gameManager.ballOver ();
		}
	}
		
	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag ("Boundary")) {
			gameManager.ballOver ();
		}
	}
}
