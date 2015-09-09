using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Eppy;
using Bolt;
using System.Linq;

namespace Bolt {
	public class Collide {

		private static Dictionary<string, HashSet<ICollider>> typeCache = new Dictionary<string, HashSet<ICollider>>();

		public static void ClearTypeCache() {
			typeCache.Clear();
		}

		public static void AssignToType(ICollider col, string type) {
			HashSet<ICollider> colliders;

			typeCache.TryGetValue(type, out colliders);

			if (colliders == null) {
					colliders = new HashSet<ICollider>();
					typeCache.Add(type, colliders);
			}

			colliders.Add(col);
		}

		public static void UnassignFromType(ICollider col, string type) {
			HashSet<ICollider> colliders;

			typeCache.TryGetValue(type, out colliders);

			if (colliders == null) {
					colliders = new HashSet<ICollider>();
					typeCache.Add(type, colliders);
			}

			colliders.Remove(col);
		}

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
			foreach (var type in types) {

				HashSet<ICollider> colliders;
				typeCache.TryGetValue(type, out colliders);

				if (colliders != null) {
					foreach (var col in colliders) {
						// @TODO check center distances for optimisation? or full quadtree?
						if (col.isActive() && col != collider) {
							var intersect = collider.Intersect(col, x, y);

							if (intersect.Intersect && (intersect.MinimumTranslation.x != 0 || intersect.MinimumTranslation.y != 0) )
							{
								return intersect;
							}
						}
					}
				}
			}

			return new CollisionResult() {
				Intersect = false
			};
		}
	}
}
