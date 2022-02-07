using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects.Movement;
using AsteroidsPlus.SpaceObjects.Spawner;
using System.Collections;
using UnityEngine;

namespace AsteroidsPlus.SpaceObjects
{
	public class Asteroid : MonoBehaviour
	{
		private SpaceObjectMovement _movemet = null;
		private BorderTeleport _borderTeleport;

		public delegate void Handler();
		public static event Handler AsteroidDestroyed;

		public Asteroid Launch(Vector2 startPosition)
		{
			_movemet = new SpaceObjectMovement(
				startPosition,
				Random.Range(0, 360),
				Random.Range(0, Data.Instance().Settings.AsteroidsMaxSpeed));

			_borderTeleport = new BorderTeleport(_movemet);

			StartCoroutine(Fly());

			AsteroidsSpawner.DestoryAllAsteroids += OnDestoryAllAsteroids;

			return this;
		}

		private IEnumerator Fly()
		{
			do
			{
				_movemet.MoveFixedUpdate(transform);
				if (_borderTeleport.Check()) _borderTeleport.Teleport();
				yield return new WaitForFixedUpdate();
			} while (true);
		}

		private void OnDestoryAllAsteroids()
		{
			Destroy(gameObject);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Missile")
			{
				if (gameObject.tag == "Asteroid") SpawnsMiniAsteroids();
				AsteroidDestroyed?.Invoke();
				Destroy(this.gameObject);
			}

			if (collision.tag == "LaserBeam")
			{
				AsteroidDestroyed?.Invoke();
				Destroy(this.gameObject);
			}
		}

		private void OnDestroy()
		{
			AsteroidsSpawner.DestoryAllAsteroids -= OnDestoryAllAsteroids;
		}

		private void SpawnsMiniAsteroids()
		{
			new AsteroidsSpawner().SpawnMini(transform.position, Data.Instance().Settings.MiniAsteroidsCount, Data.Instance().Settings.MiniAsteroidsScale);
		}
	}
}
