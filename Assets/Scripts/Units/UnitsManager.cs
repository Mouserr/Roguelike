using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class UnitsManager
	{
		private List<Unit> _units = new List<Unit>();

		public void Add(Unit unit)
		{
			_units.Add(unit);
		}

		public void Update()
		{
			foreach (var unit in _units)
			{
				unit.Update();
			}
		}
	}
}