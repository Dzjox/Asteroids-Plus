using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects.Movement;
using AsteroidsPlus.SpaceObjects.Spawner;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidsPlus.SpaceObjects
{
	public class Asteroid : MonoBehaviour
	{
		private SpaceObjectMovement _movemet = null;
		private BorderTeleport _borderTeleport = null;
		private Action _destoryAllAsteroids;

		public static Action AsteroidDestroyed;

		public Asteroid Launch(Vector2 startPosition, Action DestoryAllAsteroids)
		{
			_movemet = new SpaceObjectMovement(
				startPosition,
				Random.Range(0, 360),
				Random.Range(0, Data.Instance().Settings.AsteroidsMaxSpeed));

			_borderTeleport = new BorderTeleport(_movemet);

			_destoryAllAsteroids = DestoryAllAsteroids;
			_destoryAllAsteroids += OnDestoryAllAsteroids;

			return this;
		}

		private void FixedUpdate()
		{
			if (_movemet != null) 
				_movemet.MoveFixedUpdate(transform);

			if (_borderTeleport!=null) 
				if (_borderTeleport.Check()) 
					_borderTeleport.Teleport();
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

		private void OnDestoryAllAsteroids()
		{
			Destroy(gameObject);
		}

		private void OnDestroy()
		{
			_destoryAllAsteroids -= OnDestoryAllAsteroids;
		}

		private void SpawnsMiniAsteroids()
		{
			new AsteroidsSpawner()
				.SpawnMini(
					transform.position,
					Data.Instance().Settings.MiniAsteroidsCount,
					_destoryAllAsteroids,
					Data.Instance().Settings.MiniAsteroidsScale);
		}
	}
}
