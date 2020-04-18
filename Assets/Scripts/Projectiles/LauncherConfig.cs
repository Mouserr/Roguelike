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

	[CreateAssetMenu(menuName = "Launchers/LinearLauncher")]
	public class LinearLauncherConfig : LauncherConfig
	{
		public override void Launch(Rigidbody projectile, ProjectileInfo projectileInfo)
		{
			projectile.useGravity = false;
			var direction = (projectileInfo.TargetPosition - projectileInfo.StartPosition).normalized;
			projectile.transform.rotation = Quaternion.LookRotation(direction);
			projectile.velocity = direction * StartSpeed;
		}
	}

	[CreateAssetMenu(menuName = "Launchers/ArcLauncher")]
	public class ArcLauncherConfig : LauncherConfig
	{
		public float StartAngle;

		public override void Launch(Rigidbody projectile, ProjectileInfo projectileInfo)
		{
			projectile.useGravity = true;
		}
	}
}