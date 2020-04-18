using System;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour
	{
		private Rigidbody _owner;

		public event Action<Projectile, Collision> OnHit;

		public Rigidbody Rigidbody
		{
			get
			{
				if (!_owner)
				{
					_owner = GetComponent<Rigidbody>();
				}

				return _owner;
			}
		}
		public ProjectileInfo Info { get; private set; }

		public void Init(ProjectileInfo projectileInfo)
		{
			Info = projectileInfo;
		}

		private void OnCollisionEnter(Collision collision)
		{
			OnHit?.Invoke(this, collision);
		}
	}
}
