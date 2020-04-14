using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class AnimatorSystem : IBehaviourSystem
	{
		protected readonly int ForwardSpeedHash = Animator.StringToHash("Forward");
		private readonly Animator _animator;

		public bool IsActive => true;

		public AnimatorSystem(Animator animator)
		{
			_animator = animator;
		}

		public void Update(Unit unit)
		{
			_animator.SetFloat(ForwardSpeedHash, unit.CurrentSpeed);
		}
	}
}