using UnityEngine;

namespace Assets.Scripts.Units
{
	public class RandomPatrol : IBehaviourSystem
	{
		private readonly float _linearMoveTime;
		private readonly float _idleTime;
		private float _lastDirectionChangeTime;
		private float _lastMovedTime;
		public bool IsActive => true;

		public RandomPatrol(float linearMoveTime, float idleTime)
		{
			_linearMoveTime = linearMoveTime;
			_idleTime = idleTime;
			_lastDirectionChangeTime = -_linearMoveTime;
		}

		public void Update(Unit unit)
		{
			if (Time.time < _lastDirectionChangeTime + _linearMoveTime)
			{
				_lastMovedTime = Time.time;
				return;
			}

			if (Time.time < _lastMovedTime + _idleTime)
			{
				unit.CurrentSpeed = 0;
				return;
			}

			var randomPoint = Random.insideUnitCircle;
			unit.Direction = new Vector3(randomPoint.x, 0, randomPoint.y);
			unit.CurrentSpeed = unit.Stats.MoveSpeed;
			_lastDirectionChangeTime = Time.time;
		}
	}
}