using UnityEngine;

namespace Assets.Scripts.Units
{
	public class UnitLink : MonoBehaviour
	{
		public Unit Unit { get; private set; }

		public void Init(Unit unit)
		{
			Unit = unit;
		}
	}
}