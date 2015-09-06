using System;
using UnityEngine;

public static class Vector2Extension {
	public static Vector2 Rotate(this Vector2 v, float degrees) {
		float radians = degrees * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);

		float tx = v.x;
		float ty = v.y;

		return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
	}

	public static float DotProduct(this Vector2 v, Vector2 vector) {
		return v.x * vector.x + v.y * vector.y;
	}

	public static float DistanceTo(this Vector2 v, Vector2 vector) {
		return (float)Math.Sqrt(Math.Pow(vector.x - v.x, 2) + Math.Pow(vector.x - v.y, 2));
	}
 }
