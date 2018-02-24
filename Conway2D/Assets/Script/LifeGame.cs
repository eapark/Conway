using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour {
	[HideInInspector] public Life[,] lifeBoard;
	private Object lifePrefab;
	public int X = 10;
	public int Y = 10;
	private int lifeScale;
	public LifeGamePresets lgp;

	// Use this for initialization
	void Start () {
		lifeBoard = new Life[X, Y];
		lifePrefab = Resources.Load ("Prefab/LifePrefab");

		for (int i = 0; i < X; i++) {
			for (int j = 0; j < Y; j++) {
				GameObject newLife = Instantiate(lifePrefab,
					Camera.main.ScreenToWorldPoint(Vector3.zero),
					Quaternion.identity) as GameObject;

				Life lifeComponent = newLife.GetComponent<Life> ();
				lifeBoard [i, j] = lifeComponent;
				lifeComponent.Initialize (i, j, this);

				// Reposition newLife
				SpriteRenderer lifeSprite = newLife.GetComponent<SpriteRenderer> ();
				Vector3 currentSize = lifeSprite.bounds.size;
				newLife.transform.position += new Vector3 (currentSize.x * (i+0.5f), currentSize.y * (j+0.5f), 10);
				// add 0.5*currentSize to make the sprite isn't cut off (sprites are center-pivoted)
			}
		}

		// Mark some lives as alive to get the game started
		lgp = new LifeGamePresets(X, Y);
		xy[] Preset = lgp.blinker;
		foreach (xy coord in Preset) {
			Debug.Log (coord.x+" "+coord.y);
			lifeBoard [coord.x, coord.y].currentState = States.Alive;
		}

		StartCoroutine ("StartGame");
	}
	
	// Update is called once per frame
	void Update () {}

	IEnumerator StartGame() {
		while (true) {
			UpdateLives ();
			ApplyLivesUpdate ();
			yield return new WaitForSeconds (1f);
		}
	}

	void UpdateLives() {
		for (int i = 0; i < X; i++) {
			for (int j = 0; j < Y; j++) {
				lifeBoard [i, j].UpdateState ();
			}
		}
	}

	void ApplyLivesUpdate() {
		for (int i = 0; i < X; i++) {
			for (int j = 0; j < Y; j++) {
				lifeBoard [i, j].ApplyStateChange ();
			}
		}
	}
}
