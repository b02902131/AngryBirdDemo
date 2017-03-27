using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileFollow : MonoBehaviour {

	public Transform projectile;
	public Transform farLeft;
	public Transform farRight;

	private Vector3 m_MoveVelocity;
	public float m_DampTime = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		newPosition.x = projectile.position.x;
		newPosition.x = Mathf.Clamp (newPosition.x, farLeft.position.x, farRight.position.x);
		transform.position = Vector3.SmoothDamp (transform.position, newPosition, ref m_MoveVelocity, m_DampTime);
	}
}
