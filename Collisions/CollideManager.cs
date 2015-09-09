using UnityEngine;
using System.Collections;
using Eppy;
using Bolt;
using System.Linq;

namespace Bolt {
	public class Collide {

		public static CollisionResult With(string type, ICollider collider ) {
			return WithAtPos (new string[] {type}, collider, collider.GetPosition().x, collider.GetPosition().y);
		}

		public static CollisionResult With(string[] types, ICollider collider ) {
			return WithAtPos (types, collider, collider.GetPosition().x, collider.GetPosition().y);
		}

		public static CollisionResult WithAtPos( string type, ICollider collider, float x, float y )
		{
			return WithAtPos (new string[] {type}, collider, x, y);
		}

		public static CollisionResult WithAtPos( string[] types, ICollider collider, float x, float y )
		{

			var collisionList = Object.FindObjectsOfType<AbstractCollider> ();

			foreach (ICollider col in collisionList)
			{
				if (col.isActive() && col != collider && types.Contains(col.GetCollisionType()))
				{
					var intersect = collider.Intersect(col, x, y);

					if (intersect.Intersect)
					{
						return intersect;
					}
				}
			}

			return new CollisionResult() {
				Intersect = false
			};
		}
	}
}
