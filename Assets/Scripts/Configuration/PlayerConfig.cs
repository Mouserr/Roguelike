using Assets.Scripts.Projectiles;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Configuration
{
	[CreateAssetMenu(menuName = "Config/Player")]
	public class PlayerConfig : UnitConfig
	{
		[SerializeField]
		private Vector3 _forwardDirection;
		[SerializeField]
		private LauncherConfig _launcherConfig;

		public override bool OnlyOne => true;

		public override Unit CreateUnit(Rigidbody instance, UnitsManager unitsManager, ProjectilesManager projectilesManager)
		{
			var unit = base.CreateUnit(instance, unitsManager, projectilesManager);

			unit.AddBehaviour(new InputController(_forwardDirection));
			unit.AddBehaviour(new MovementController(instance));
			unit.AddBehaviour(new CollectPickups());
			unit.AddBehaviour(new AutoShooting(unitsManager, projectilesManager, new Launcher(_launcherConfig, instance.GetComponentInChildren<LaunchPoint>().transform)));
			unit.AddBehaviour(new AnimatorController(instance.GetComponentInChildren<Animator>()));

			return unit;
		}
	}
}