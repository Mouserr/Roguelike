using Assets.Scripts.Projectiles;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Configuration
{
	[CreateAssetMenu(menuName = "Config/PatrolUnit")]
	public class PatrolUnitConfig : UnitConfig
	{
		[SerializeField]
		private float _linearMoveTime;
		[SerializeField]
		private float _idleTime;

		public override Unit CreateUnit(Rigidbody instance, UnitsManager unitsManager, ProjectilesManager projectilesManager)
		{
			var unit = base.CreateUnit(instance, unitsManager, projectilesManager);

			unit.AddBehaviour(new RandomPatrol(_linearMoveTime, _idleTime));
			unit.AddBehaviour(new MovementController(instance));

			return unit;
		}
	}
}