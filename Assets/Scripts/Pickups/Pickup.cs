using System;
using UnityEngine;

namespace Assets.Scripts.Pickups
{
	[RequireComponent(typeof(Rigidbody))]
	public class Pickup : MonoBehaviour
	{
		private Rigidbody _rigidbody;

		[SerializeField]
		private int _goldAmount;

		public event Action<Pickup> Collected;

		public Rigidbody Rigidbody
		{
			get
			{
				if (!_rigidbody)
				{
					_rigidbody = GetComponent<Rigidbody>();
				}

				return _rigidbody;
			}
		}

		public int GoldAmount => _goldAmount;

		public void Collect()
		{
			Collected?.Invoke(this);
		}
	}
}