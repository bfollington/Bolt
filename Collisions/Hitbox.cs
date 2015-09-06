using UnityEngine;
using System.Collections.Generic;

namespace Bolt {
	public class Hitbox : Mask, ICollider {

		[SerializeField] 
		private float width;
		[SerializeField] 
		private float height;

		public float offsetX;
		public float offsetY;
		public string type = "solid";
		public bool active = true;

		// Representation
		private Polygon polygon;

		public float Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
			}
		}

		public float Height
		{
			get
			{
				return height;
			}
			set
			{
				height = value;
			}
		}

		public float left
		{
			get {
				return transform.position.x + offsetX;
			}

			set {
				transform.position = new Vector3(value - offsetX, transform.position.y,  transform.position.z);
			}
		}

		public float right
		{
			get {
				return transform.position.x + offsetX + width;
			}

			set {
				transform.position = new Vector3(value - width - offsetX, transform.position.y,  transform.position.z);
			}

		}

		public float top
		{
			get {
				return transform.position.y + offsetY;
			}

			set {
				transform.position = new Vector3(transform.position.x, value - offsetY, transform.position.z);
			}
		}

		public float bottom
		{
			get {
				return transform.position.y - height + offsetY;
			}

			set {
				transform.position = new Vector3(transform.position.x, value + height - offsetY, transform.position.z);
			}
		}

		public bool isActive()
		{
			return active;
		}

		public bool Active
		{
			get
			{
				return active;
			}
			set
			{
				active = value;
			}
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

		public void BuildPolygon() {
			var corners = GetCorners(0, 0);
			polygon = new Polygon();

			foreach (var corner in corners) {
				polygon.Points.Add(corner);
			}

			polygon.BuildEdges();
		}

		public Polygon GetPolygonRepresentation() {
			if (polygon == null) {
				BuildPolygon();
			}

			return polygon;
		}

		private List<Vector2> GetCorners(float speedX, float speedY) {
			List<Vector2> corners = new List<Vector2>();

			corners.Add( new Vector2(left + speedX, top + speedY) );
			corners.Add( new Vector2(left + speedX, bottom + speedY) );
			corners.Add( new Vector2(right + speedX, top + speedY) );
			corners.Add( new Vector2(right + speedX, bottom + speedY) );

			return corners;

		}

		public CollisionResult Intersect(ICollider col, float speedX, float speedY) {
			if (col is Hitbox) {
				return IntersectHitbox(col as Hitbox, speedX, speedY);
			}

			if (col is Polybox) {
				return new CollisionResult() {
					Intersect = false
				};
			}

			return new CollisionResult() {
				Intersect = false
			};
		}

		/// <summary>
		/// Does this Hitbox intersect with another one.
		/// </summary>
		/// <param name="hb">Hb.</param>
		/// <param name="xOff">X off.</param>
		/// <param name="yOff">Y off.</param>
		public CollisionResult IntersectHitbox(Hitbox hb, float speedX, float speedY)
		{
			var corners = GetCorners(speedX, speedY);

			foreach (var corner in corners) {
				if (corner.x > hb.left && corner.x < hb.right) {
					if (corner.y > hb.bottom && corner.y < hb.top) {
						return new CollisionResult() {
							Intersect = true,
							Collider = hb,
							CollisionObject = hb.GetGameObject(),
							MinimumTranslation = new Vector2(0, 0) // @TODO
						};
					}
				}
			}

			return new CollisionResult() {
				Intersect = false
			};
		}

		public void OnDrawGizmos()
		{
			Gizmos.color = new Color (1f, 0f , 0f, 0.5f);
			Gizmos.DrawWireCube (new Vector3( transform.position.x + offsetX + width / 2, transform.position.y + offsetY - height / 2, transform.position.z),
			                 new Vector3 (width, height, 1));

			Gizmos.color = new Color (0f, 1f , 0f, 0.5f);
			Gizmos.DrawWireCube (transform.position,
			                     new Vector3 (0.1f, 0.1f, 1f));

			Gizmos.color = new Color (0f, 0f, 1f, 0.5f);
		}

		public Rect AsRect()
		{
			return new Rect (left, top, width, height);
		}

	}
}
