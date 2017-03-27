using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int ball_num = 3;
	private int ball_index = 0;
	public ProjectileDragging[] balls;

	public int bird_num = 1;
	private int count = 0;
	public BirdScript[] birds;

	public projectileFollow cam;
	public Text gameText;
	public Button next;

	public bool hasNext;

	// Use this for initialization
	void Start () {
		ball_num = balls.Length;
		for (int i = 0; i < balls.Length; i++) {
			balls [i].gameManager = this;
			if (i == 0)
				balls [i].Reload ();
			else {
				balls [i].Standby ();
			}
		}
		for (int i = 0; i < birds.Length; i++) {
			birds [i].gameManager = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ballShoot(){
		ball_index++;
	}

	public void ballOver(){
		if (ball_index < ball_num) {
			balls [ball_index].enabled = true;
			balls [ball_index].Reload ();
			cam.projectile = balls [ball_index].transform;
		} else {
			GameFailed ();	
		}
	}

	public void GameFailed(){
		gameText.text = "Failed";
		gameText.enabled = true;
	}

	public void GameWin(){
		gameText.text = "Win!!";
		gameText.enabled = true;
		if (hasNext) {
			next.gameObject.SetActive (true);	
		}
	}

	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void LoadLevel(string name){
		Application.LoadLevel (name);
	}

	public void BirdKilled(){
		count++;
		if (count == bird_num) {
			GameWin ();
		}
	}
}
