using UnityEngine;

namespace Assets.Scripts.Units
{
	public class InputSystem : IBehaviourSystem
	{
		private readonly Quaternion _forwardDirection;
		public bool IsActive => true;

		public InputSystem(Vector3 forwardDirection)
		{
			_forwardDirection = Quaternion.Euler(forwardDirection);
		}

		public void Update(Unit unit)
		{
			var inputDirection = GetInputDirection();

			var movementDirection = _forwardDirection * inputDirection;

			unit.Velocity = movementDirection.normalized * unit.Stats.MoveSpeed;
		}

		private static Vector3 GetInputDirection()
		{
			var inputDirection = Vector3.zero;

			if (Input.GetKey(KeyCode.W))
			{
				inputDirection += Vector3.forward;
			}

			if (Input.GetKey(KeyCode.A))
			{
				inputDirection += Vector3.left;
			}

			if (Input.GetKey(KeyCode.S))
			{
				inputDirection += Vector3.back;
			}

			if (Input.GetKey(KeyCode.D))
			{
				inputDirection += Vector3.right;
			}

			return inputDirection;
		}
	}
}