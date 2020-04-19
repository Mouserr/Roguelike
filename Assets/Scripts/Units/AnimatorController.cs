using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class AnimatorController : IBehaviourController
	{
		protected readonly int ForwardSpeedHash = Animator.StringToHash("Forward");
		private readonly Animator _animator;

		public bool IsActive => true;

		public AnimatorController(Animator animator)
		{
			_animator = animator;
		}

		public void Update(Unit unit)
		{
			_animator.SetFloat(ForwardSpeedHash, unit.CurrentSpeed);
		}
	}
}