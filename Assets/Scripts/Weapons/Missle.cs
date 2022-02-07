using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects.Movement;
using System.Collections;
using UnityEngine;

namespace AsteroidsPlus.Weapon
{
	public class Missle : MonoBehaviour
	{
		private SpaceObjectMovement _movemet = null;
		private BorderTeleport _borderTeleport;

		public void Launch(Transform shipTransform)
		{
			_movemet = new SpaceObjectMovement(
				shipTransform.position,
				shipTransform.rotation.eulerAngles.z,
				Data.Instance().Settings.MissleSpeed);

			_borderTeleport = new BorderTeleport(_movemet);

			StartCoroutine(Fly(Data.Instance().Settings.MissleFlyTime));
		}

		private IEnumerator Fly(float flyTime)
		{
			var destroyTime = Time.time + flyTime;

			do
			{
				_movemet.MoveFixedUpdate(transform);
				if (_borderTeleport.Check()) _borderTeleport.Teleport();
				yield return new WaitForFixedUpdate();
			} while (destroyTime > Time.time);

			Destroy(this.gameObject);
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
