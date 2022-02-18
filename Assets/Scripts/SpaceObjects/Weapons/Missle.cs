using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects.Movement;
using UnityEngine;

namespace AsteroidsPlus.Weapon
{
	public class Missle : MonoBehaviour
	{
		private SpaceObjectMovement _movemet = null;
		private BorderTeleport _borderTeleport;
		private float _destroyTime;

		public void Launch(Transform shipTransform)
		{
			_movemet = new SpaceObjectMovement(
				shipTransform.position,
				shipTransform.rotation.eulerAngles.z,
				Data.Instance().Settings.MissleSpeed);

			_borderTeleport = new BorderTeleport(_movemet);

			_destroyTime = Time.time + Data.Instance().Settings.MissleFlyTime;
		}

		private void Update()
		{
			if (_movemet != null)
			{
				_movemet.MoveUpdate(transform);
				if (_destroyTime < Time.time) Destroy(this.gameObject);				
			}

			if (_borderTeleport != null)
				if (_borderTeleport.Check())
					_borderTeleport.Teleport();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Asteroid"
				|| collision.tag == "MiniAsteroid"
				|| collision.tag == "UFO")
				Destroy(this.gameObject);
		}
	}
}
