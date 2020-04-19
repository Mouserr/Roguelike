using Assets.Scripts.Pickups;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class CollectPickups : IBehaviourSystem
	{
		private readonly int _pickupsLayer = LayerMask.GetMask("Pickup");
		private readonly float _pickupRadius;
		private Collider[] _colliders = new Collider[50];
		public bool IsActive => true;

		public CollectPickups(float pickupRadius = 2)
		{
			_pickupRadius = pickupRadius;
		}

		public void Update(Unit unit)
		{
			var count = Physics.OverlapSphereNonAlloc(unit.Position, _pickupRadius, _colliders, _pickupsLayer);
			if (count == 0)
			{
				return;
			}

			for (var i = 0; i < count; i++)
			{
				var collider = _colliders[i];
				var pickup = collider.GetComponent<Pickup>();
				pickup?.Collect();
			}
		}
	}
}