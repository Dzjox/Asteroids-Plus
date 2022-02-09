using UnityEngine;

namespace AsteroidsPlus.SpaceObjects.Movement
{
	public class SpaceObjectMovement
	{
		protected Vector2 _position;
		protected float _rotation;
		protected float _speed;

		public Vector2 Position { get { return _position; } }
		public float Rotation { get { return _rotation; } }
		public float Speed { get { return _speed; } }


		protected Vector2 _inertiaVector;

		public SpaceObjectMovement(Vector2 position, float rotation, float speed)
		{
			_position = position;
			_rotation = rotation;
			_speed = speed;

			var forwardVector = (Quaternion.Euler(0, 0, _rotation) * Vector2.up).normalized;
			_inertiaVector = forwardVector * _speed * Time.fixedDeltaTime;
		}

		public void MoveFixedUpdate(Transform movedTranform)
		{
			_speed = _inertiaVector.sqrMagnitude / Time.fixedDeltaTime;
			_position += _inertiaVector;
			movedTranform.position = _position;
		}

		public void MoveWithVector(Vector2 moveVector)
		{
			_position += moveVector;
		}
	}
}
