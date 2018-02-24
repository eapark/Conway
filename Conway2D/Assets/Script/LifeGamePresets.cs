using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct xy {
	public int x;
	public int y;

	public xy(int a, int b){
		x = a;
		y = b;
	}
}

public class LifeGamePresets {
	// Source: https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life#Examples_of_patterns
	public xy[] blinker, block, random;

	public LifeGamePresets(int x, int y) {
		int xHalf = x / 2;
		int yHalf = y / 2;

		if (x >= 3 && y >= 3) {
			blinker = new xy[3] { new xy (xHalf, yHalf), new xy (xHalf, yHalf - 1), new xy (xHalf, yHalf + 1) };
		} else {
			blinker = new xy[0];
		}

		if (x >= 2 && y >= 2) {
			block = new xy[4] {
				new xy (xHalf, yHalf),
				new xy (xHalf, yHalf + 1),
				new xy (xHalf + 1, yHalf),
				new xy (xHalf + 1, yHalf + 1)
			};
		} else {
			block = new xy[0];
		}

		int randomSize = Random.Range (0, x*y+1);
		random = new xy[randomSize];
		for (int i = 0; i < randomSize; i++) {
			int randomX = Random.Range (0, x);
			int randomY = Random.Range (0, y);
			random[i] = new xy (randomX, randomY);
		}
	}
}
