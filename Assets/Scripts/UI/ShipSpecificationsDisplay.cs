using AsteroidsPlus.Logic;
using AsteroidsPlus.SpaceObjects;
using AsteroidsPlus.SpaceObjects.Movement;
using AsteroidsPlus.Weapon;
using System.Collections;
using TMPro;
using UnityEngine;

namespace AsteroidsPlus.UI
{
	public class ShipSpecificationsDisplay : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _position;
		[SerializeField] private TextMeshProUGUI _rotation;
		[SerializeField] private TextMeshProUGUI _speed;
		[SerializeField] private TextMeshProUGUI _lazerCharges;
		[SerializeField] private TextMeshProUGUI _timeToNextLC;

		private RoundsLogic _roundsLogic;
		private ShipMovement _playerMovemet = null;
		private Laser _laser = null;

		private float _nextTime;
		private readonly float _delay = 1;

		public void SetRoundsLogic(RoundsLogic roundsLogic)
		{
			_roundsLogic = roundsLogic;
		}

		private void FixedUpdate()
		{
			if (_playerMovemet != null && _laser != null) SetValue();
			else if (_roundsLogic != null) FindShip();			
		}
		private void SetValue()
		{
			_position.text = $"X:{_playerMovemet.Position.x.ToString("0.0")} Y:{_playerMovemet.Position.y.ToString("0.0")}";
			_rotation.text = "R: " + _playerMovemet.Rotation.ToString("0");
			_speed.text = "S: " + _playerMovemet.Speed.ToString("0.0");
			_lazerCharges.text = "LC: " + _laser.Charges.ToString("0");
			var timeToNextLC = _laser.NextChargeTime - Time.time;
			_timeToNextLC.text = "Next LC: " + (timeToNextLC > 0 ? timeToNextLC.ToString("0.00") : "0");
		}

		private void FindShip()
		{
			if (_nextTime > Time.time) return;

			if (_roundsLogic.PlayerShip == null)
			{
				_nextTime = Time.time + _delay;
			}
			else
			{
				_playerMovemet = _roundsLogic.PlayerShip.GetComponent<Ship>().Movement;
				_laser = _roundsLogic.PlayerShip.GetComponent<Laser>();
			}
		}
	}
}
