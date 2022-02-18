using AsteroidsPlus.Core;
using UnityEngine;

namespace AsteroidsPlus.SpaceObjects.Movement
{
	public class ShipMovement : SpaceObjectMovement
	{
		protected float _acceleration;
		protected float _rotationSpeed;
		protected float _maxSpeed;

		public ShipMovement(Vector2 position, float rotation, float speed, float maxSpeed, float acceleration, float rotationSpeed)
			: base(position, rotation, speed)
		{
			_acceleration = acceleration;
			_rotationSpeed = rotationSpeed;
			_maxSpeed = maxSpeed;
		}

		public ShipMovement() : base(Vector2.zero, 0, 0)
		{
			_position = Data.Instance().Settings.ShipStartPosiotion;
			_rotation = Data.Instance().Settings.ShipStartRotation;
			_speed = Data.Instance().Settings.ShipStartSpeed;
			_maxSpeed = Data.Instance().Settings.ShipMaxSpeed;
			_acceleration = Data.Instance().Settings.ShipAcceleration;
			_rotationSpeed = Data.Instance().Settings.ShipRotationSpeed;
		}


		public void MoveUpdate(
			Transform movedTranform,			
			bool moveForward = false,
			bool turnRight = false,
			bool turnLeft = false)
		{
			if (turnRight) _rotation -= _rotationSpeed * Time.deltaTime;
			if (turnLeft) _rotation += _rotationSpeed * Time.deltaTime;
			movedTranform.rotation = Quaternion.Euler(0, 0, _rotation);

			Vector2 forwardVector = (Quaternion.Euler(0, 0, _rotation) * Vector2.up).normalized;
			if (moveForward) _inertiaVector += forwardVector * _acceleration * Time.deltaTime;

			//Speed limit
			if (_inertiaVector.magnitude > _maxSpeed) _inertiaVector = _inertiaVector.normalized * _maxSpeed;

			base.MoveUpdate(movedTranform);
		}

	}
}
