using AsteroidsPlus.Core;
using AsteroidsPlus.Core.Interface;
using System.Collections;
using UnityEngine;

namespace AsteroidsPlus.Weapon
{
	public class Laser : MonoBehaviour, IWeaponSystem
	{
		private float _nextFireTime;
		private int _charges;
		private Coroutine _addChargeCoroutine = null;

		public int Charges { get { return _charges; } }
		public float NextChargeTime;

		private void Awake()
		{
			_charges = Data.Instance().Settings.LaserMaxCharges;
		}

		public void Fire()
		{
			if (_charges > 0)
			{
				if (_nextFireTime < Time.time)
				{
					Object.Instantiate(Data.Instance().Settings.LaserBeamPrefab, transform)
						.GetComponent<LaserBeam>()
						.Launch();

					_charges--;

					if (_charges < Data.Instance().Settings.LaserMaxCharges && _addChargeCoroutine == null) 
						_addChargeCoroutine = StartCoroutine(AddChargeWithTime());

					_nextFireTime = Time.time + Data.Instance().Settings.LaserBeamTime * 2;
				}
			}
		}

		private IEnumerator AddChargeWithTime()
		{
			do
			{
				NextChargeTime = Time.time + Data.Instance().Settings.LaserAddChargeTime;
				yield return new WaitForSeconds(Data.Instance().Settings.LaserAddChargeTime);
				_charges++;
			} while (_charges != Data.Instance().Settings.LaserMaxCharges);
			_addChargeCoroutine = null;
		}
	}
}
