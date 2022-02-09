using AsteroidsPlus.Core;
using AsteroidsPlus.Core.Interface;
using UnityEngine;

namespace AsteroidsPlus.Weapon
{
	public class Cannon : MonoBehaviour, IWeaponSystem
	{
		private float _fireTime;

		public void Fire()
		{
			if (_fireTime < Time.time)
			{
				var missle = Object.Instantiate(Data.Instance().Settings.MisslePrefab, transform.position, transform.rotation);
				missle.GetComponent<Missle>().Launch(transform);
				_fireTime = Time.time + Data.Instance().Settings.CannonDelayTime;
			}
		}
	}
}
