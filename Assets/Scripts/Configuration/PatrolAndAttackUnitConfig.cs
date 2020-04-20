using Assets.Scripts.Projectiles;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Configuration
{
	[CreateAssetMenu(menuName = "Config/PatrolAndAttackUnit")]
	public class PatrolAndAttackUnitConfig : UnitConfig
	{
		[SerializeField]
		private float _linearMoveTime;
		[SerializeField]
		private float _idleTime;
		[SerializeField]
		private LauncherConfig _launcherConfig;

		public override Unit CreateUnit(Rigidbody instance, UnitsManager unitsManager, ProjectilesManager projectilesManager)
		{
			var unit = base.CreateUnit(instance, unitsManager, projectilesManager);

			unit.AddBehaviour(new RandomPatrol(_linearMoveTime, _idleTime));
			unit.AddBehaviour(new MovementController(instance));
			unit.AddBehaviour(new AutoShooting(unitsManager, projectilesManager, new Launcher(_launcherConfig, instance.GetComponentInChildren<LaunchPoint>().transform)));

			return unit;
		}
	}
}