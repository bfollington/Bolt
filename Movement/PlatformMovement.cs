using UnityEngine;
using System.Collections;
using Bolt;

namespace Bolt {

	public class PlatformMovement : MonoBehaviour {

		public Vector2 velocity;

		public Vector2 maxSpeed;
		public float gravity;

		public string[] collideTypes;

		private AbstractCollider mask;

		// Use this for initialization
		void Start () {
			velocity.x = velocity.y = 0;
		}

		void SetMask(AbstractCollider mask) {
			this.mask = mask;
		}

		private AbstractCollider GetMask() {
			AbstractCollider mask;

			if (this.mask != null) {
				mask = this.mask;
			} else {
				mask = GetComponent<AbstractCollider>();
			}

			return mask;
		}

		// Update is called once per frame
		void Update () {

			var entity = GetComponent<EntityAccess>().entity;
			mask = GetMask();


			var scaledVelocity = new Vector2 (
				velocity.x * TimeUtil.scale (),
                velocity.y * TimeUtil.scale ()
            );

			if (velocity.x != 0)
			{
				var collision = Collide.WithAtPos (collideTypes, mask, scaledVelocity.x, 0);

				if (!collision.Intersect)
				{
					entity.x += scaledVelocity.x;
				} else {

					velocity.x = 0;

					entity.x += collision.MinimumTranslation.x;
				}
			}

			if (velocity.y != 0)
			{
				var collision = Collide.WithAtPos (collideTypes, mask, 0, scaledVelocity.y);

				if (!collision.Intersect)
				{
					entity.y += scaledVelocity.y;
				} else {

					velocity.y = 0;

					entity.y += collision.MinimumTranslation.y;
				}
			}

			velocity.y -= gravity;

			velocity.x = ClampUtil.Clamp (velocity.x, -1 * maxSpeed.x, maxSpeed.x);
			velocity.y = ClampUtil.Clamp (velocity.y, -1 * maxSpeed.y, maxSpeed.y);
		}

		public bool OnGround()
		{
			mask = GetMask();
			var collision = Collide.WithAtPos (collideTypes, mask, 0, -0.5f);

			return collision.Intersect;
		}
	}
}

