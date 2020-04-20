using UnityEngine;

namespace Assets.Scripts.Projectiles
{
	[CreateAssetMenu(menuName = "Config/Launcher/LinearLauncher")]
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
}