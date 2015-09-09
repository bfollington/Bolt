using UnityEngine;
using System.Collections;
using Bolt;

public class AbstractCollider : MonoBehaviour, ICollider {

	public virtual bool isActive() {
		return false;
	}

	public virtual CollisionResult Intersect(ICollider col, float speedX, float speedY) {
		return new CollisionResult() {
			Intersect = false
		};
	}

	public virtual string GetCollisionType() {
		return "";
	}

	public virtual Vector3 GetPosition() {
		return gameObject.transform.position;
	}

	public virtual GameObject GetGameObject() {
		return gameObject;
	}

}
