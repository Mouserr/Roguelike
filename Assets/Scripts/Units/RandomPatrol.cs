using UnityEngine;

namespace Assets.Scripts.Units
{
	public class RandomPatrol : IBehaviourSystem
	{
		private readonly float _linearMoveTime;
		private float _lastDirectionChangeTime;
		public bool IsActive => true;

		public RandomPatrol(float linearMoveTime)
		{
			_linearMoveTime = linearMoveTime;
			_lastDirectionChangeTime = -_linearMoveTime;
		}

		public void Update(Unit unit)
		{
			if (Time.time < _lastDirectionChangeTime + _linearMoveTime)
			{
				return;
			}

			var randomPoint = Random.insideUnitCircle;
			unit.Direction = new Vector3(randomPoint.x, 0, randomPoint.y);
			unit.CurrentSpeed = unit.Stats.MoveSpeed;
			_lastDirectionChangeTime = Time.time;
		}
	}
}