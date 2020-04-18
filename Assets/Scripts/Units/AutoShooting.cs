using Assets.Scripts.Projectiles;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class AutoShooting : IBehaviourSystem
	{
		private readonly UnitsManager _unitsManager;
		private readonly ProjectilesManager _projectilesManager;
		private readonly Launcher _launcher;
		private Unit _chosenEnemy;

		public bool IsActive => true;

		public AutoShooting(UnitsManager unitsManager, ProjectilesManager projectilesManager, Launcher launcher)
		{
			_unitsManager = unitsManager;
			_projectilesManager = projectilesManager;
			_launcher = launcher;

			_projectilesManager.RegisterLauncher(launcher.Config);
		}

		public void Update(Unit unit)
		{
			if (!unit.IsStaying)
			{
				_chosenEnemy = null;
				return;
			}

			if (_chosenEnemy == null || !_chosenEnemy.IsAlive)
			{
				if (!_unitsManager.TryGetNearestEnemy(unit, out _chosenEnemy))
				{
					return;
				}
			}

			unit.Direction = _chosenEnemy.Position - unit.Position;

			_projectilesManager.Launch(_launcher, new ProjectileInfo
			{
				Launcher = _launcher,
				Damage = _launcher.Config.DamageMultiplier * unit.Stats.BaseDamage,
				Sender = unit,
				TargetPosition = _chosenEnemy.Position
			});
		}
	}
}