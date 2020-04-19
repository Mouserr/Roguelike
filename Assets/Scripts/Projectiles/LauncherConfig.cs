using UnityEngine;

namespace Assets.Scripts.Projectiles
{
	public abstract class LauncherConfig : ScriptableObject
	{
		public Projectile ProjectilePrefab;
		public Transform ExplosionPrefab;

		public float StartSpeed;
		public float ShootRate;
		public float DamageMultiplier;

		public abstract void Launch(Rigidbody projectile, ProjectileInfo projectileInfo);
	}
}