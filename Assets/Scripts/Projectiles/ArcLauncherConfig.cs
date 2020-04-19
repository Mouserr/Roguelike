using UnityEngine;

namespace Assets.Scripts.Projectiles
{
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