using UnityEngine;
using System.Collections;
using Bolt;

public struct CollisionResult {
	public bool Intersect; // Are the polygons currently intersecting
	public Vector2 MinimumTranslation; // The translation to apply to polygon A to push the polygons appart.
	public GameObject CollisionObject;
	public ICollider Collider;
}