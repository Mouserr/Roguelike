using UnityEngine;

namespace Assets.Scripts.Units
{
	public class MovementSystem : IBehaviourSystem
	{
		private readonly Rigidbody _rigidbody;
		private readonly float _lowSpeedThresholdSqr;
		public bool IsActive => true;

		public Vector3 Position
		{
			get => _rigidbody.position;
			protected set => _rigidbody.MovePosition(value);
		}

		public Quaternion Rotation
		{
			get => _rigidbody.rotation;
			set => _rigidbody.MoveRotation(value);
		}

		public Vector3 Velocity
		{
			get => _rigidbody.velocity;
			set => _rigidbody.velocity = value;
		}

		public MovementSystem(Rigidbody rigidbody, float lowSpeedThreshold = 0.1f)
		{
			_rigidbody = rigidbody;
			_lowSpeedThresholdSqr = lowSpeedThreshold * lowSpeedThreshold;
		}

		public void Update(Unit unit)
		{
			Velocity = unit.Velocity;
			
			unit.IsStaying = Velocity.sqrMagnitude < _lowSpeedThresholdSqr;

			unit.Position = Position;
			if (!unit.IsStaying)
			{
				Rotation = Quaternion.LookRotation(unit.Velocity);
				unit.CurrentSpeed = Velocity.magnitude;
			}
			else
			{
				unit.CurrentSpeed = 0;
			}
		}
	}
}