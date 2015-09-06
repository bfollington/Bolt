using UnityEngine;
using System.Collections.Generic;

namespace Bolt {
	public class Polybox : Mask, ICollider {

		public float offsetX;
		public float offsetY;
		public string type = "solid";
		public bool active = true;

		private Polygon polygon = new Polygon();


		public bool isActive()
		{
			return active;
		}

		public void AddVertex(Vector2 v) {
			polygon.Points.Add(v);
			polygon.BuildEdges();
		}

		public Polygon GetPolygon() {
			return polygon;
		}

		public string GetCollisionType() {
			return type;
		}

		public Vector3 GetPosition() {
			return transform.position;
		}

		public GameObject GetGameObject() {
			return gameObject;
		}

		public CollisionResult Intersect(ICollider col, float speedX, float speedY) {
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
					Intersect = res.Intersect,
					MinimumTranslation = res.MinimumTranslation,
					Collider = col,
					CollisionObject = col.GetGameObject()
				};
			}

			return new CollisionResult() {
				Intersect = false
			};
		}

		public PolygonCollisionUtil.PolygonCollisionResult IntersectHitbox(Hitbox hb, float speedX, float speedY)
		{
			var poly = hb.GetPolygonRepresentation();

			return PolygonCollisionUtil.PolygonCollision(polygon, poly, new Vector2(speedX, speedY));
		}

		public PolygonCollisionUtil.PolygonCollisionResult IntersectPolybox(Polybox p, float speedX, float speedY) {
			return PolygonCollisionUtil.PolygonCollision(polygon, p.GetPolygon(), new Vector2(speedX, speedY));
		}

		public void OnDrawGizmos()
		{
			Gizmos.color = new Color (1f, 1f , 0f, 1f);

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
