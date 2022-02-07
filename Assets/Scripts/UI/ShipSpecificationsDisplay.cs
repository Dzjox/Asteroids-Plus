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

		private ShipMovement _playerMovemet = null;
		private Laser _laser = null;

		private void OnEnable()
		{
			StartCoroutine(FindPlayer());
		}

		private IEnumerator FindPlayer()
		{
			GameObject player = null;
			do
			{
				player = GameObject.FindGameObjectWithTag("Player");
				yield return new WaitForSeconds(1);
			} while (player == null);

			_playerMovemet = player.GetComponent<Ship>().Movement;
			_laser = player.GetComponent<Laser>();
		}

		private void FixedUpdate()
		{
			if (_playerMovemet != null)
			{
				_position.text = $"X:{_playerMovemet.Position.x.ToString("0.0")} Y:{_playerMovemet.Position.y.ToString("0.0")}";
				_rotation.text = "R: " + _playerMovemet.Rotation.ToString("0");
				_speed.text = "S: " + _playerMovemet.Speed.ToString("0.0");
				_lazerCharges.text = "LC: " + _laser.Charges.ToString("0");
				var timeToNextLC = _laser.NextChargeTime - Time.time;
				_timeToNextLC.text = "Next LC: " + (timeToNextLC > 0 ? timeToNextLC.ToString("0.00") : "0");
			}
		}
	}
}
