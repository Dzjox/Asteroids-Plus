using AsteroidsPlus.Core;
using AsteroidsPlus.Core.Interface;
using AsteroidsPlus.SpaceObjects.Movement;
using System;
using UnityEngine;

namespace AsteroidsPlus.SpaceObjects
{
	public class Ship : MonoBehaviour
	{
		private ShipMovement _shipMovement;
		private InputSystem _inputActions;
		private BorderTeleport _borderTeleport;
		private IWeaponSystem _mainWeapon;
		private IWeaponSystem _ultimateWeapon;

		public Component MainWeapon;
		public Component UltimateWeapon;

		public static event Action ShipDestroyed;

		public ShipMovement Movement { get { return _shipMovement; } }

		private void Awake()
		{
			_shipMovement = new ShipMovement();

			_inputActions = new InputSystem();
			_inputActions.Ship.Enable();

			_borderTeleport = new BorderTeleport(_shipMovement);

			_mainWeapon = (IWeaponSystem)MainWeapon;
			_ultimateWeapon = (IWeaponSystem)UltimateWeapon;
		}

		private void Update()
		{
			_shipMovement.MoveUpdate(
				transform,
				_inputActions.Ship.MoveForward.IsPressed(),
				_inputActions.Ship.TurnRight.IsPressed(),
				_inputActions.Ship.TurnLeft.IsPressed());

			if (_borderTeleport.Check()) _borderTeleport.Teleport();

			if (_inputActions.Ship.FireMainWeapon.IsPressed()) _mainWeapon.Fire();
			if (_inputActions.Ship.FireUltimateWeapon.IsPressed()) _ultimateWeapon.Fire();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Asteroid"
				|| collision.tag == "MiniAsteroid"
				|| collision.tag == "UFO")
			{
				ShipDestroyed?.Invoke();
				Destroy(this.gameObject);
			}
		}

	}
}
