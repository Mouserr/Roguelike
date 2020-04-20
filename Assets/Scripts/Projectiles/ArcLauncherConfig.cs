using UnityEngine;

namespace Assets.Scripts.Projectiles
{
	[CreateAssetMenu(menuName = "Config/Launcher/ArcLauncher")]
	public class ArcLauncherConfig : LauncherConfig
	{
		public float StartAngle;

		public override void Launch(Rigidbody projectile, ProjectileInfo projectileInfo)
		{
			projectile.useGravity = true;

			if (!TryGetStartVelocity(projectileInfo.StartPosition, projectileInfo.TargetPosition, StartAngle,
				out var startVelocity))
			{
				startVelocity = StartSpeed * startVelocity.normalized;
			}

			projectile.velocity = startVelocity;
		}

		private bool TryGetStartVelocity(Vector3 startGlobalPostion, Vector3 targetGlobalPosition, float radAngle, out Vector3 startVelocity)
		{
			Vector3 lineDirection = targetGlobalPosition - startGlobalPostion;
			float heightDiff = lineDirection.y;
			lineDirection.y = 0;

			float length = lineDirection.magnitude;
			lineDirection.y = length * Mathf.Tan(radAngle);
			length += heightDiff / Mathf.Tan(radAngle);
			if (length <= 0)
			{
				startVelocity = lineDirection;
				return false;
			}

			float velocity = Mathf.Sqrt(length * Physics.gravity.magnitude / Mathf.Sin(2 * radAngle));
			startVelocity = velocity * lineDirection.normalized;
			return true;
		}
	}
}