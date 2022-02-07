using AsteroidsPlus.Core;
using System.Collections;
using UnityEngine;

namespace AsteroidsPlus.Weapon
{
	public class LaserBeam : MonoBehaviour
	{
		public void Launch()
		{
			transform.localScale = new Vector3(
				transform.localScale.x,
				Data.Instance().Settings.LaserBeamLengthScale,
				transform.localScale.z);

			transform.localPosition = Data.Instance().Settings.LaserBeamOffset;

			StartCoroutine(EndWithTime());
		}

		private IEnumerator EndWithTime()
		{
			yield return new WaitForSeconds(Data.Instance().Settings.LaserBeamTime);
			Destroy(this.gameObject);
		}
	}
}
