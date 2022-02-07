using System.Collections;
using UnityEngine;

namespace AsteroidsPlus.Core
{
	public class Coroutiner : MonoBehaviour
	{
		public static Coroutiner Instance = null;

		private void Awake()
		{
			if (Instance == null) Instance = this;
			else if (Instance == this) Destroy(gameObject);
		}

		public Coroutine DoCoroutine(IEnumerator coroutine)
		{
			return StartCoroutine(coroutine);
		}

		public void StopDoCoroutine(Coroutine coroutine)
		{
			StopCoroutine(coroutine);
		}
	}
}
