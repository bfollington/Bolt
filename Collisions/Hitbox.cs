using UnityEngine;
using System.Collections.Generic;

namespace Bolt {
	public class Hitbox : AbstractCollider, ICollider {

		[SerializeField] 
		private float width;
		[SerializeField] 
		private float height;

		public float offsetX;
		public float offsetY;

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

		public Vector2 Center
		{
			get {
				return new Vector2(left + width / 2, bottom + height / 2);
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

		public override bool isActive()
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
			return Center;
		}

		public void BuildPolygon(float xOffset = 0, float yOffset = 0) {
			var corners = GetCorners(xOffset, yOffset);
			polygon = new Polygon();

			foreach (var corner in corners) {
				polygon.Points.Add(corner);
			}

			polygon.BuildEdges();
		}

		public Polygon GetPolygonRepresentation(float xOffset, float yOffset) {
			BuildPolygon(xOffset, yOffset);

			return polygon;
		}

		private List<Vector2> GetCorners(float speedX, float speedY) {
			List<Vector2> corners = new List<Vector2>();

			corners.Add( new Vector2(left + speedX, top + speedY) );
			corners.Add( new Vector2(right + speedX, top + speedY) );
			corners.Add( new Vector2(right + speedX, bottom + speedY) );
			corners.Add( new Vector2(left + speedX, bottom + speedY) );

			return corners;

		}

		override public CollisionResult Intersect(ICollider col, float speedX, float speedY) {
			if (col is Hitbox) {
				return IntersectHitbox(col as Hitbox, speedX, speedY);
			}

			if (col is Polybox) {
				return IntersectPolybox(col as Polybox, speedX, speedY);
			}

			Logging.Log("Collision type not supported for", this, col);

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

						var translation = new Vector2();

						if (Center.x < hb.Center.x) {
							translation.x = hb.left - right;
						} else {
							translation.x = hb.right - left;
						}

						if (Center.y > hb.Center.y) {
							translation.y = hb.top - bottom;
						} else {
							translation.y = hb.bottom - top;
						}

						return new CollisionResult() {
							Intersect = true,
							Collider = hb,
							CollisionObject = hb.GetGameObject(),
							MinimumTranslation = translation
						};
					}
				}
			}

			return new CollisionResult() {
				Intersect = false
			};
		}

		public CollisionResult IntersectPolybox(Polybox p, float speedX, float speedY) {

			var poly = this.GetPolygonRepresentation(speedX, speedY);

			var collision = PolygonCollisionUtil.PolygonCollision(poly, p.GetPolygonAtPosition(0, 0), new Vector2(0, 0));

			return new CollisionResult() {
				Intersect = collision.Intersect || collision.WillIntersect,
				Collider = p,
				CollisionObject = p.GetGameObject(),
				MinimumTranslation = collision.MinimumTranslation
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
