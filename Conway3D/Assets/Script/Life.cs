using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum States {
	Alive, Dead
};

public class Life : MonoBehaviour {
	[HideInInspector] public States currentState;
	[HideInInspector] public States nextState;

	public Material materialAlive;
	public Material materialDead;

	private int x, y, z;
	private LifeGame lifeGame;

	public void Initialize(int x, int y, int z, LifeGame lifeGame) {
		this.x = x;
		this.y = y;
		this.z = z;
		this.lifeGame = lifeGame;
		this.currentState = States.Dead;
	}

	public void UpdateState(){
		int numNeighbor = ReturnNumNeighbor ();
		nextState = currentState; // maintain current state if none of the condition below are met
		if (currentState == States.Alive) {
			if (numNeighbor < 2 || numNeighbor > 3) {
				nextState = States.Dead;
			}
		} else {
			if (numNeighbor == 3) {
				nextState = States.Alive;
			}
		}
		gameObject.GetComponent<Renderer>().material = (currentState == States.Alive) ? materialAlive : materialDead;
	}

	private int ReturnNumNeighbor() {
		LifeGame game = lifeGame.GetComponent<LifeGame> ();
		GameObject[,,] lifeBoard = game.lifeBoard;
		int gameX = game.X;
		int gameY = game.Y;
		int gameZ = game.Z;
		int numLiveNeighbor = 0;
		// Check currentState of lives at position (this.x-1, this.y-1), (this.x-1, this.y), etc
		for (int i = -1; i <= 1; i++) {
			// Check for index out of bounds
			if(x + i < 0 || x + i >= gameX){
				continue;
			}
			for (int j = -1; j <= 1; j++) {
				// Check for index out of bounds
				if(y + j < 0 || y + j >= gameY){
					continue;
				}
				for (int k = -1; k <= 1; k++) {
					// Skip i=0 and j=0 and k=0 (itself)
					if (i == 0 && j == 0 && k==0) {
						continue;
					}
					// Check for index out of bounds
					if(z + k < 0 || z + k >= gameZ){
						continue;
					}
					Life neighbor = lifeBoard [x + i, y + j, z + k].GetComponent<Life> ();
					numLiveNeighbor += (neighbor.currentState == States.Alive) ? 1 : 0;
				}
			}
		}

		return numLiveNeighbor;
	}

	public void ApplyStateChange() {
		currentState = nextState;
	}
}
