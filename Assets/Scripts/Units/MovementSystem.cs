using UnityEngine;

namespace Assets.Scripts.Units
{
	public class MovementSystem : IBehaviourSystem
	{
		private readonly Rigidbody _rigidbody;
		private readonly float _lowSpeedThreshold;
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
			_lowSpeedThreshold = lowSpeedThreshold;
		}

		public void Update(Unit unit)
		{
			Velocity = unit.Direction * unit.CurrentSpeed;
			Rotation = Quaternion.LookRotation(unit.Direction);

			unit.IsStaying = unit.CurrentSpeed < _lowSpeedThreshold;

			unit.Position = Position;
		}
	}
}