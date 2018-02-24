using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct xyz {
	public int x;
	public int y;
	public int z;

	public xyz(int a, int b, int c){
		x = a;
		y = b;
		z = c;
	}
}

public class LifeGamePresets {
	// Source: https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life#Examples_of_patterns
	public xyz[] blinker, block, random;

	public LifeGamePresets(int x, int y, int z) {
		int xHalf = x / 2;
		int yHalf = y / 2;
		int zHalf = z / 2;

		if (x >= 3 && y >= 3) {
			blinker = new xyz[3] {
				new xyz (xHalf, yHalf, zHalf),
				new xyz (xHalf, yHalf - 1, zHalf),
				new xyz (xHalf, yHalf + 1, zHalf)
			};
		} else {
			blinker = new xyz[0];
		}
		if (x >= 2 && y >= 2) {
			block = new xyz[4] {
				new xyz (xHalf, yHalf, zHalf),
				new xyz (xHalf, yHalf + 1, zHalf),
				new xyz (xHalf + 1, yHalf, zHalf),
				new xyz (xHalf + 1, yHalf + 1, zHalf)
			};
		} else {
			block = new xyz[0];
		}
		int randomSize = Random.Range (0, x*y*z+1);
		random = new xyz[randomSize];
		for (int i = 0; i < randomSize; i++) {
			int randomX = Random.Range (0, x);
			int randomY = Random.Range (0, y);
			int randomZ = Random.Range (0, z);
			random[i] = new xyz (randomX, randomY, randomZ);
		}
	}
}
