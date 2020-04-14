using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class Unit
	{
		private readonly List<IBehaviourSystem> _behaviours = new List<IBehaviourSystem>();
		public Stats Stats { get; }
		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }
		public float CurrentSpeed { get; set; }
		public bool IsStaying { get; set; }

		public Unit(Stats stats, params IBehaviourSystem[] behaviours)
		{
			Stats = stats;
			foreach (var behaviour in behaviours)
			{
				AddBehaviour(behaviour);
			}
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