using System;
using Assets.Scripts.Helpers;
using Assets.Scripts.Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Pickups
{
	public class PickupsManager
	{
		private readonly UnitsManager _unitsManager;
		private readonly UserInfo _userInfo;
		private readonly GameObjectPool<Pickup> _pickupsPool;

		public PickupsManager(UnitsManager unitsManager, UserInfo userInfo, Pickup pickupPrefab)
		{
			_unitsManager = unitsManager;
			_userInfo = userInfo;
			_unitsManager.UnitKilled += OnUnitKilled;
			_pickupsPool = new GameObjectPool<Pickup>(new GameObject("Pickups").transform, pickupPrefab, 5);
		}

		private void OnUnitKilled(Unit unit)
		{
			var spawnCount = Random.Range(0, 20);
			float explosionRadius = 0.1f;
			for (int i = 0; i < spawnCount; i++)
			{
				var pickup = _pickupsPool.GetObject();
				var randomShift = Random.insideUnitCircle * explosionRadius;
				pickup.transform.position = unit.Position + new Vector3(randomShift.x, explosionRadius, randomShift.y);
				pickup.gameObject.SetActive(true);
				pickup.Rigidbody.AddExplosionForce(5, unit.Position, explosionRadius);
				pickup.Collected += OnPickupCollected;
			}
		}

		private void OnPickupCollected(Pickup pickup)
		{
			_userInfo.AddGold(pickup.GoldAmount);
			_pickupsPool.ReleaseObject(pickup);
		}
	}
}