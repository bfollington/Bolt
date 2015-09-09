using UnityEngine;
using System.Collections;
using Bolt;

public class AbstractCollider : MonoBehaviour, ICollider {

	public bool isActive() {
		return false;
	}

	public CollisionResult Intersect(ICollider col, float speedX, float speedY) {
		return new CollisionResult() {
			Intersect = false
		};
	}

	public string GetCollisionType() {
		return "";
	}

	public Vector3 GetPosition() {
		return gameObject.transform.position;
	}

	public GameObject GetGameObject() {
		return gameObject;
	}

}
