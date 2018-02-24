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

	public Sprite spriteAlive;
	public Sprite spriteDead;

	private int x, y;
	private LifeGame lifeGame;

	public void Initialize(int x, int y, LifeGame lifeGame) {
		this.x = x;
		this.y = y;
		this.lifeGame = lifeGame;
		this.currentState = States.Dead;
	}
		
	public void UpdateState(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = (currentState == States.Alive) ? spriteAlive : spriteDead;

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
	}

	private int ReturnNumNeighbor() {
		LifeGame game = lifeGame.GetComponent<LifeGame> ();
		Life[,] lifeBoard = game.lifeBoard;
		int gameX = game.X;
		int gameY = game.Y;
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

				// Skip i=0 and j=0 (itself)
				if (i == 0 && j == 0) {
					continue;
				}
				Life neighbor = lifeBoard [x + i, y + j];
				numLiveNeighbor += (neighbor.currentState == States.Alive) ? 1 : 0;
			}
		}

		return numLiveNeighbor;
	}

	public void ApplyStateChange() {
		currentState = nextState;
	}
}
