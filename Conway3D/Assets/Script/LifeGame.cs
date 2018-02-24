using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour {
	[HideInInspector] public GameObject[,,] lifeBoard;
	private Object lifePrefab;
	public int X = 10;
	public int Y = 10;
	public int Z = 10;
	private int lifeScale;
	public LifeGamePresets lgp;

	// Use this for initialization
	void Start () {
		lifeBoard = new GameObject[X, Y, Z];
		lifePrefab = Resources.Load ("Prefab/LifeCube");

		for (int i = 0; i < X; i++) {
			for (int j = 0; j < Y; j++) {
				for (int k = 0; k < Z; k++) {
					GameObject newLife = Instantiate (lifePrefab,
						Vector3.zero,
						Quaternion.identity) as GameObject;
					lifeBoard [i, j, k] = newLife;
					newLife.GetComponent<Life> ().Initialize (i, j, k, this);

					// Reposition newLife
					Vector3 currentSize = transform.localScale;
					newLife.transform.position += new Vector3 (currentSize.x * i, currentSize.y * j, currentSize.z * k);
				}
			}
		}

		// Mark some lives as alive to get the game started
		lgp = new LifeGamePresets(X, Y, Z);
		xyz[] Preset = lgp.blinker;

		foreach (xyz coord in Preset) {
			lifeBoard [coord.x, coord.y, coord.z].GetComponent<Life> ().currentState = States.Alive;
		}

		StartCoroutine ("StartGame");
	}

	// Update is called once per frame
	void Update () {}

	IEnumerator StartGame() {
		while (true) {
			UpdateLives ();
			ApplyLivesUpdate ();
			yield return new WaitForSeconds (3f);
		}
	}

	void UpdateLives() {
		for (int i = 0; i < X; i++) {
			for (int j = 0; j < Y; j++) {
				for (int k = 0; k < Z; k++) {
					lifeBoard [i, j, k].GetComponent<Life> ().UpdateState ();
				}
			}
		}
	}

	void ApplyLivesUpdate() {
		for (int i = 0; i < X; i++) {
			for (int j = 0; j < Y; j++) {
				for (int k = 0; k < Z; k++) {
					lifeBoard [i, j, k].GetComponent<Life> ().ApplyStateChange ();
				}
			}
		}
	}
}
