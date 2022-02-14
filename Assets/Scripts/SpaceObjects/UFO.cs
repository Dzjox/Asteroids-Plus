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
		private Action _destoryAllUFO;

		public static Action UFODestroyed;

		public void Launch(Vector2 startPosition, Transform player, Action destoryAllUFO)
		{
			_player = player;
			_movemet = new UFOMovement(startPosition, 0, Data.Instance().Settings.UFOSpeed);
			_borderTeleport = new BorderTeleport(_movemet);

			_destoryAllUFO = destoryAllUFO;
			_destoryAllUFO += OnDestoryAllUFO;
		}

		private void FixedUpdate()
		{
			if (_movemet != null)
				_movemet.MoveFixedUpdate(transform);

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
			_destoryAllUFO -= OnDestoryAllUFO;
		}

	}
}
