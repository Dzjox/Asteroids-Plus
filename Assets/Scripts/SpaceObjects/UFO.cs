using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects.Movement;
using AsteroidsPlus.SpaceObjects.Spawner;
using System;
using UnityEngine;

namespace AsteroidsPlus.SpaceObjects
{
	public class UFO : MonoBehaviour
	{
		private UFOMovement _movemet = null;
		private BorderTeleport _borderTeleport;
		private Transform _player;
		private UFOSpawner _uFOSpawner;

		public static Action UFODestroyed;

		public void Launch(Vector2 startPosition, Transform player, UFOSpawner uFOSpawner)
		{
			_player = player;
			_movemet = new UFOMovement(startPosition, 0, Data.Instance().Settings.UFOSpeed);
			_borderTeleport = new BorderTeleport(_movemet);

			_uFOSpawner = uFOSpawner;
			_uFOSpawner.DestoryAllUFO += OnDestoryAllUFO;
		}

		private void Update()
		{
			if (_movemet != null)
				_movemet.MoveUpdate(transform,_player);

			if (_borderTeleport != null)
				if (_borderTeleport.Check())
					_borderTeleport.Teleport();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Missile" || collision.tag == "LaserBeam")
			{
				UFODestroyed?.Invoke();
				Destroy(this.gameObject);
			}
		}

		private void OnDestoryAllUFO()
		{
			Destroy(this.gameObject);
		}

		private void OnDestroy()
		{
			_uFOSpawner.DestoryAllUFO -= OnDestoryAllUFO;
		}

	}
}
