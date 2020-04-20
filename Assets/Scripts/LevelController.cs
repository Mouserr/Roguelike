using System;
using System.Collections.Generic;
using Assets.Scripts.Configuration;
using Assets.Scripts.Helpers;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts
{
	public class LevelController : MonoBehaviour
	{
		private readonly Dictionary<UnitConfig, GameObjectPool<Rigidbody>> _unitPools = new Dictionary<UnitConfig, GameObjectPool<Rigidbody>>();

		[SerializeField]
		private List<UnitConfig> _allias;

		[SerializeField]
		private List<UnitConfig> _enemies;

		[SerializeField]
		private Transform _unitsRoot;

		[SerializeField]
		private List<Vector3> _spawnPositions;

		[SerializeField]
		[Tooltip("Add points here and press Save")]
		private List<Transform> _spawnPoints;

		[ContextMenu("Save")]
		public void Save()
		{
			_spawnPositions.Clear();
			foreach (var spawnPoint in _spawnPoints)
			{
				_spawnPositions.Add(spawnPoint.position);
			}
		}

		public Unit StartLevel(UnitsManager unitsManager, ProjectilesManager projectilesManager)
		{
			var positions = RandomHelper.ShuffleList(_spawnPositions);
			var nextPosIdx = 0;
			Unit player = null;

			foreach (var ally in _allias)
			{
				var unit = CreateUnit(ally, positions[nextPosIdx++], unitsManager, projectilesManager);
				unit.Fraction = 0;
				unitsManager.Add(unit);
				if (ally.OnlyOne)
				{
					player = unit;
				}
			}

			foreach (var enemy in _enemies)
			{
				var unit = CreateUnit(enemy, positions[nextPosIdx++], unitsManager, projectilesManager);
				unit.Fraction = 1;
				unitsManager.Add(unit);
			}

			unitsManager.UnitKilled += OnUnitKilled;

			return player;
		}

		private void OnUnitKilled(Unit unit)
		{
			if (!unit.Config.OnlyOne)
			{
				_unitPools[unit.Config].ReleaseObject(unit.Rigidbody);
			}
		}

		private Unit CreateUnit(UnitConfig unitConfig, Vector3 position, UnitsManager unitsManager, ProjectilesManager projectilesManager)
		{
			Rigidbody instance;
			if (unitConfig.OnlyOne)
			{
				instance = Instantiate(unitConfig.Prefab, _unitsRoot);
			}
			else
			{
				if (!_unitPools.TryGetValue(unitConfig, out var pool))
				{
					pool = new GameObjectPool<Rigidbody>(_unitsRoot, unitConfig.Prefab, 5);
					_unitPools.Add(unitConfig, pool);
				}

				instance = pool.GetObject();
			}

			instance.transform.position = position;
			instance.gameObject.SetActive(true);

			var unit = unitConfig.CreateUnit(instance, unitsManager, projectilesManager);
			unit.Config = unitConfig;
			return unit;
		}
	}
}