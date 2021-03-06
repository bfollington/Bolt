﻿using UnityEngine;
using System.Collections;
using Bolt;


namespace Bolt {

	public class PlatformMovement : MonoBehaviour {

		public Vector2 velocity;

		public Vector2 maxSpeed;
		public float gravity;

		public string[] collideTypes;

		private AbstractCollider mask;
		
		public event HitWallHandler HitWall;

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
		
		public delegate void HitWallHandler(PlatformMovement m, bool x, bool y);

		// Update is called once per frame
		void Update () {


			var entity = GetComponent<EntityAccess>().entity;
			mask = GetMask();

            CollisionResult col;

			if (!( col = Collide.WithAtPos(collideTypes, mask, velocity.x, 0) ).Intersect) {
				entity.x += velocity.x;
			} else {
				if (col.Collider is Hitbox) {
					entity.x += col.MinimumTranslation.x;
				} else {
					entity.y += col.MinimumTranslation.y;
					entity.x += col.MinimumTranslation.x + velocity.x;
				}

				velocity.x = 0;

				HitWall(this, true, false);

			}

			if (!( col = Collide.WithAtPos(collideTypes, mask, 0, velocity.y) ).Intersect) {
				entity.y += velocity.y;
			} else {

				if (col.Collider is Hitbox) {
					entity.y += col.MinimumTranslation.y;
				} else {
					entity.x += col.MinimumTranslation.x;
					entity.y += col.MinimumTranslation.y + velocity.y;
				}

				velocity.y = 0;
				
				HitWall(this, false, true);

			}

			// Stops sliding down slopes
			if (!OnGround()) {
				velocity.y -= gravity;
			}
           
			velocity.x = ClampUtil.Clamp (velocity.x, -1 * maxSpeed.x, maxSpeed.x);
			velocity.y = ClampUtil.Clamp (velocity.y, -1 * maxSpeed.y, maxSpeed.y);
		}

		public bool OnGround(float offsetX = 0, float offsetY = 0)
		{
			mask = GetMask();
			var collision = Collide.WithAtPos (collideTypes, mask, 0 + offsetX, -0.5f + offsetY);

			return collision.Intersect;
		}
	}
}

