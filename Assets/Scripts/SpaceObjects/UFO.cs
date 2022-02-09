using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects.Movement;
using AsteroidsPlus.SpaceObjects.Spawner;
using System;
using System.Collections;
using UnityEngine;

namespace AsteroidsPlus.SpaceObjects
{
	public class UFO : MonoBehaviour
	{
		private UFOMovement _movemet = null;
		private BorderTeleport _borderTeleport;
		private Transform _player;

		public static Action UFODestroyed;

		public void Launch(Vector2 startPosition, Transform player)
		{
			_player = player;
			_movemet = new UFOMovement(startPosition, 0, Data.Instance().Settings.UFOSpeed);
			_borderTeleport = new BorderTeleport(_movemet);
			StartCoroutine(Fly());

			UFOSpawner.DestoryAllUFO += OnDestoryAllUFO;
		}

		private void OnDestoryAllUFO()
		{
			Destroy(this.gameObject);
		}

		private IEnumerator Fly()
		{
			do
			{
				_movemet.MoveFixedUpdate(transform, _player);
				if (_borderTeleport.Check()) _borderTeleport.Teleport();
				yield return new WaitForFixedUpdate();
			} while (true);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Missile" || collision.tag == "LaserBeam")
			{
				UFODestroyed?.Invoke();
				Destroy(this.gameObject);
			}
		}

		private void OnDestroy()
		{
			UFOSpawner.DestoryAllUFO -= OnDestoryAllUFO;
		}

	}
}
