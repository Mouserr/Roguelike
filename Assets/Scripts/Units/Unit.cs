using System.Collections.Generic;
using Assets.Scripts.Configuration;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class Unit
	{
		private readonly List<IBehaviourController> _behaviours = new List<IBehaviourController>();
		public UnitConfig Config { get; set; }
		public int Fraction { get; set; }
		public Rigidbody Rigidbody { get; }
		public Stats Stats { get; }
		public Vector3 Position { get; set; }
		public Vector3 Direction { get; set; }
		public float CurrentSpeed { get; set; }
		public bool IsStaying { get; set; }
		public bool IsAlive => Stats.CurrentHealth > 0;

		public Unit(Rigidbody rigidbody, Stats stats, params IBehaviourController[] behaviours)
		{
			Rigidbody = rigidbody;
			Stats = stats;
			foreach (var behaviour in behaviours)
			{
				AddBehaviour(behaviour);
			}

			Direction = Rigidbody.transform.forward;
		}

		public void AddBehaviour(IBehaviourController behaviour)
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