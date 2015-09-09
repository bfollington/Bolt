using UnityEngine;
using System.Collections;
using Bolt;

public class AbstractCollider : MonoBehaviour, ICollider {

	[SerializeField] 
	private string _type = "";

	public string type {
		get {
			return _type;
		}

		set {
			if (_type != "") {
				Collide.UnassignFromType(this, _type);
			}

			_type = value;
			Collide.AssignToType(this, _type);
		}
	}

	public virtual Vector2 GetCenter() {
		return new Vector2(0, 0);
	}

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

	void Start() {
		// Trigger adding to cache if type was set in editor
		type = type;
	}

}
