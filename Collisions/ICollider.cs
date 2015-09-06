using System;
using Bolt;
using UnityEngine;

namespace Bolt
{
	public interface ICollider
	{
		bool isActive();
		CollisionResult Intersect(ICollider col, float speedX, float speedY);
		string GetCollisionType();
		Vector3 GetPosition();
		GameObject GetGameObject();
	}

}

