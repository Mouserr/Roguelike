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
			if (TryGetInputDirection(out var inputDirection))
			{
				var movementDirection = _forwardDirection * inputDirection;
				unit.Direction = movementDirection.normalized;
				unit.CurrentSpeed = unit.Stats.MoveSpeed;
			}
			else
			{
				unit.CurrentSpeed = 0;
			}
		}

		private static bool TryGetInputDirection(out Vector3 direction)
		{
			direction = Vector3.zero;
			bool hasInput = false;

			if (Input.GetKey(KeyCode.W))
			{
				direction += Vector3.forward;
				hasInput = true;
			}

			if (Input.GetKey(KeyCode.A))
			{
				direction += Vector3.left;
				hasInput = true;
			}

			if (Input.GetKey(KeyCode.S))
			{
				direction += Vector3.back;
				hasInput = true;
			}

			if (Input.GetKey(KeyCode.D))
			{
				direction += Vector3.right;
				hasInput = true;
			}

			return hasInput;
		}
	}
}