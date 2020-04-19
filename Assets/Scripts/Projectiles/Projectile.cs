using System;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour
	{
		private Rigidbody _rigidbody;

		public event Action<Projectile, Collision> Hit;

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
		public ProjectileInfo Info { get; private set; }

		public void Init(ProjectileInfo projectileInfo)
		{
			Info = projectileInfo;
		}

		private void OnCollisionEnter(Collision collision)
		{
			Hit?.Invoke(this, collision);
		}
	}
}
