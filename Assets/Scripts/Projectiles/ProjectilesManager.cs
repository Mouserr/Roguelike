using System;
using System.Collections.Generic;
using Assets.Scripts.Helpers;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
	public class ProjectilesManager
	{
		private readonly DamageSystem _damageSystem;
		private readonly Transform _bulletsRoot;
		private readonly Transform _explosionsRoot;
		private readonly Dictionary<LauncherConfig, GameObjectPool<Projectile>> _projectilePools = new Dictionary<LauncherConfig, GameObjectPool<Projectile>>();
		private readonly Dictionary<LauncherConfig, GameObjectPool<Transform>> _explosionPools = new Dictionary<LauncherConfig, GameObjectPool<Transform>>();

		public ProjectilesManager(DamageSystem damageSystem)
		{
			_damageSystem = damageSystem;
			_bulletsRoot = new GameObject("BulletsPool").transform;
			_explosionsRoot = new GameObject("ExplosionsPool").transform;
		}

		public void RegisterLauncher(LauncherConfig launcher)
		{
			_projectilePools.Add(launcher, new GameObjectPool<Projectile>(_bulletsRoot.transform, launcher.ProjectilePrefab, 30));
			_explosionPools.Add(launcher, new GameObjectPool<Transform>(_explosionsRoot.transform, launcher.ExplosionPrefab, 30));
		}

		public bool Launch(Launcher launcher, ProjectileInfo projectileInfo)
		{
			if (!launcher.CanShoot)
			{
				return false;
			}

			var projectile = _projectilePools[launcher.Config].GetObject();
			projectile.Init(projectileInfo);
			projectile.OnHit += OnHit;
			projectile.transform.position = launcher.LaunchPoint.position;
			projectile.transform.rotation = launcher.LaunchPoint.rotation;
			projectile.gameObject.SetActive(true);

			projectileInfo.StartPosition = launcher.LaunchPoint.position;
			launcher.Launch(projectile.Rigidbody, projectileInfo);

			return true;
		}

		private void OnHit(Projectile projectile, Collision collision)
		{
			projectile.OnHit -= OnHit;
			if (collision.gameObject.TryGetComponent<UnitLink>(out var unitLink))
			{
				_damageSystem.ApplyDamage(projectile.Info, unitLink.Unit);
			}

			var launcherConfig = projectile.Info.Launcher.Config;
			_projectilePools[launcherConfig].ReleaseObject(projectile);

			var explosion = _explosionPools[launcherConfig].GetObject();
			explosion.transform.position = collision.GetContact(0).point;
			explosion.gameObject.SetActive(true);
		}
	}
}