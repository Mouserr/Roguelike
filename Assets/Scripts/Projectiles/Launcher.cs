using UnityEngine;

namespace Assets.Scripts.Projectiles
{
	public class Launcher
	{
		private float _lastShotTime;
		private float _shootDelay;

		public LauncherConfig Config { get; }
		public Transform LaunchPoint { get; }
		public bool CanShoot => Time.time > _lastShotTime + _shootDelay;

		public Launcher(LauncherConfig config, Transform launchPoint)
		{
			Config = config;
			LaunchPoint = launchPoint;
			_shootDelay = 1 / config.ShootRate;
		}

		public void Launch(Rigidbody projectile, ProjectileInfo projectileInfo)
		{
			Config.Launch(projectile, projectileInfo);
			_lastShotTime = Time.time;
		}
	}
}