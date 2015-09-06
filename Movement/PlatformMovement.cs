using UnityEngine;
using System.Collections;
using Bolt;

namespace Bolt {

	public class PlatformMovement : MonoBehaviour {

		public Vector2 velocity;

		public Vector2 maxSpeed;
		public float gravity;

		public string[] collideTypes;

		// Use this for initialization
		void Start () {
			velocity.x = velocity.y = 0;
		}

		// Update is called once per frame
		void Update () {

			var hitbox = this.GetComponent<Hitbox> ();

			var scaledVelocity = new Vector2 (
				velocity.x * TimeUtil.scale (),
                velocity.y * TimeUtil.scale ()
            );

			if (velocity.x != 0)
			{
				var collision = Collide.WithAtPos (collideTypes, this.GetComponent<Hitbox> (), scaledVelocity.x, 0);

				if (!collision.Intersect)
				{
					hitbox.left += scaledVelocity.x;
				} else {

					velocity.x = 0;

					if (hitbox.left <= (collision.Collider as Hitbox).left) {
						hitbox.right = (collision.Collider as Hitbox).left;
					} else {
						hitbox.left = (collision.Collider as Hitbox).right;
					}
				}
			}

			if (velocity.y != 0)
			{
				var collision = Collide.WithAtPos (collideTypes, this.GetComponent<Hitbox> (), 0, scaledVelocity.y);

				if (!collision.Intersect)
				{
					hitbox.bottom += scaledVelocity.y;
				} else {

					velocity.y = 0;
				
					if (hitbox.bottom >= (collision.Collider as Hitbox).bottom) {
						hitbox.bottom = (collision.Collider as Hitbox).top;
					} else {
						hitbox.top = (collision.Collider as Hitbox).bottom;
					}
				}
			}

			velocity.y -= gravity;

			velocity.x = ClampUtil.Clamp (velocity.x, -1 * maxSpeed.x, maxSpeed.x);
			velocity.y = ClampUtil.Clamp (velocity.y, -1 * maxSpeed.y, maxSpeed.y);
		}

		public bool OnGround()
		{
			var collision = Collide.WithAtPos (collideTypes, this.GetComponent<Hitbox> (), 0, -0.5f);

			return collision.Intersect;
		}
	}
}

