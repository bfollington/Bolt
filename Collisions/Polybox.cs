using UnityEngine;
using System.Collections.Generic;

namespace Bolt {
	public class Polybox : AbstractCollider, ICollider {

		public float offsetX;
		public float offsetY;
		public bool active = true;

		private Polygon lastHitboxPoly;

		private Polygon polygon = new Polygon();


		public override bool isActive()
		{
			return active;
		}

		public void AddVertex(Vector2 v) {
			polygon.Points.Add(v);
			polygon.BuildEdges();
		}


		// Common shape creation

		public static void CreateSlopeDownRight(Polybox p, float size) {
			p.AddVertex(new Vector2(0, 0));
			p.AddVertex(new Vector2(size, -size));
			p.AddVertex(new Vector2(0, -size));
		}






		public Polygon GetPolygon() {
			return polygon;
		}

		public Polygon GetPolygonAtPosition(float xOffset, float yOffset) {
			var e = GetComponent<EntityAccess>().entity;

			return polygon.OffsetBy(e.x + xOffset, e.y + yOffset);
		}

		public override string GetCollisionType() {
			return type;
		}

		public override Vector3 GetPosition() {
			return transform.position;
		}

		public override GameObject GetGameObject() {
			return gameObject;
		}

		public override Vector2 GetCenter() {
			return GetPolygonAtPosition(0, 0).Center;
		}

		public override CollisionResult Intersect(ICollider col, float speedX, float speedY) {
			if (col is Hitbox) {
				var res = IntersectHitbox(col as Hitbox, speedX, speedY);

				return new CollisionResult() {
					Intersect = res.Intersect,
					MinimumTranslation = res.MinimumTranslation,
					Collider = col,
					CollisionObject = col.GetGameObject()
				};
			}

			if (col is Polybox) {
				var res = IntersectPolybox(col as Polybox, speedX, speedY);

				return new CollisionResult() {
					Intersect = res.Intersect || res.WillIntersect,
					MinimumTranslation = res.MinimumTranslation,
					Collider = col,
					CollisionObject = col.GetGameObject()
				};
			}

			Logging.Log("Collision type not supported for", this, col);

			return new CollisionResult() {
				Intersect = false
			};
		}

		public PolygonCollisionUtil.PolygonCollisionResult IntersectHitbox(Hitbox hb, float speedX, float speedY)
		{
			var poly = hb.GetPolygonRepresentation(0, 0);
			lastHitboxPoly = poly;

			return PolygonCollisionUtil.PolygonCollision(GetPolygonAtPosition(speedX, speedY), poly, new Vector2(0, 0));
		}

		public PolygonCollisionUtil.PolygonCollisionResult IntersectPolybox(Polybox p, float speedX, float speedY) {
			return PolygonCollisionUtil.PolygonCollision(GetPolygonAtPosition(speedX, speedY), p.GetPolygonAtPosition(0, 0), new Vector2(0, 0));
		}

		public void OnDrawGizmos()
		{

			Gizmos.color = new Color (1f, 1f , 0f, 1f);

			if (lastHitboxPoly != null) {
				for (var i = 0; i < lastHitboxPoly.Points.Count - 1; i++) {
					var point = lastHitboxPoly.Points[i];
					var nextPoint = lastHitboxPoly.Points[i + 1];

					Gizmos.DrawLine(
						new Vector3(point.x, point.y, 0),
						new Vector3(nextPoint.x, nextPoint.y, 0)
					);
				}
			}



			for (var i = 0; i < polygon.Points.Count - 1; i++) {
				var point = polygon.Points[i];
				var nextPoint = polygon.Points[i + 1];

				Gizmos.DrawLine(
					new Vector3(transform.position.x + point.x, transform.position.y + point.y, transform.position.z),
					new Vector3(transform.position.x + nextPoint.x, transform.position.y + nextPoint.y, transform.position.z)
				);
			}

			Gizmos.DrawLine(
				new Vector3(transform.position.x + polygon.Points[polygon.Points.Count - 1].x, transform.position.y + polygon.Points[polygon.Points.Count - 1].y, transform.position.z),
				new Vector3(transform.position.x + polygon.Points[0].x, transform.position.y + polygon.Points[0].y, transform.position.z)
			);

			Gizmos.color = new Color (0f, 1f , 0f, 0.5f);
			Gizmos.DrawWireCube (transform.position,
			                     new Vector3 (0.1f, 0.1f, 1f));
		}
	}
}
