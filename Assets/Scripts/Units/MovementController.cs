using UnityEngine;

namespace Assets.Scripts.Units
{
	public class MovementController : IBehaviourController
	{
		private readonly Rigidbody _rigidbody;
		private readonly float _lowSpeedThreshold;
		public bool IsActive => true;

		private Vector3 Position
		{
			get => _rigidbody.position;
			set => _rigidbody.MovePosition(value);
		}

		private Quaternion Rotation
		{
			get => _rigidbody.rotation;
			set => _rigidbody.MoveRotation(value);
		}

		private Vector3 Velocity
		{
			get => _rigidbody.velocity;
			set => _rigidbody.velocity = value;
		}

		private Vector3 AngularVelocity
		{
			get => _rigidbody.angularVelocity;
			set => _rigidbody.angularVelocity = value;
		}

		public MovementController(Rigidbody rigidbody, float lowSpeedThreshold = 0.1f)
		{
			_rigidbody = rigidbody;
			_lowSpeedThreshold = lowSpeedThreshold;
		}

		public void Update(Unit unit)
		{
			unit.IsStaying = unit.CurrentSpeed < _lowSpeedThreshold;
			if (unit.IsStaying)
			{
				Velocity = Vector3.zero;
				AngularVelocity = Vector3.zero;
			}
			else
			{
				Velocity = unit.Direction * unit.CurrentSpeed;
			}
			
			Rotation = Quaternion.LookRotation(unit.Direction);

			unit.Position = Position;
		}
	}
}