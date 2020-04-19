using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class Unit
	{
		private readonly List<IBehaviourSystem> _behaviours = new List<IBehaviourSystem>();
		public int Fraction { get; set; }
		public Rigidbody Rigidbody { get; }
		public Stats Stats { get; }
		public Vector3 Position { get; set; }
		public Vector3 Direction { get; set; }
		public float CurrentSpeed { get; set; }
		public bool IsStaying { get; set; }
		public bool IsAlive => Stats.CurrentHealth > 0;

		public Unit(Rigidbody rigidbody, Stats stats, params IBehaviourSystem[] behaviours)
		{
			Rigidbody = rigidbody;
			Stats = stats;
			foreach (var behaviour in behaviours)
			{
				AddBehaviour(behaviour);
			}

			Direction = Rigidbody.transform.forward;
		}

		public void AddBehaviour(IBehaviourSystem behaviour)
		{
			_behaviours.Add(behaviour);
		}

		public void Update()
		{
			foreach (var behaviour in _behaviours)
			{
				if (behaviour.IsActive)
				{
					behaviour.Update(this);
				}
			}
		}
	}
}