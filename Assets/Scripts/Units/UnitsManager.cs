using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class UnitsManager
	{
		private readonly List<Unit> _units = new List<Unit>();

		public void Add(Unit unit)
		{
			_units.Add(unit);
			var unitLink = unit.Rigidbody.gameObject.AddComponent<UnitLink>();
			unitLink.Init(unit);
		}

		public void Update()
		{
			for (var i = _units.Count - 1; i >= 0; i--)
			{
				var unit = _units[i];
				if (unit.IsAlive)
				{
					unit.Update();
				}
				else
				{
					Kill(unit);
				}
			}
		}

		public bool TryGetNearestEnemy(Unit unit, out Unit closestEnemy)
		{
			closestEnemy = null;
			float minDistanceSqr = float.MaxValue; 

			foreach (var checkingUnit in _units)
			{
				if (checkingUnit.Fraction == unit.Fraction)
				{
					continue;
				}

				var distSqr = (checkingUnit.Position - unit.Position).sqrMagnitude;
				if (distSqr < minDistanceSqr)
				{
					closestEnemy = checkingUnit;
					minDistanceSqr = distSqr;
				}
			}

			return closestEnemy != null;
		}

		public void Kill(Unit unit)
		{
			_units.Remove(unit);
			Object.Destroy(unit.Rigidbody.gameObject);
		}
	}
}