using Assets.Scripts.Projectiles;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Configuration
{
	public abstract class UnitConfig : ScriptableObject
	{
		[SerializeField]
		private Rigidbody _prefab;
		[SerializeField]
		private float _maxHealth;
		[SerializeField]
		private float _moveSpeed;
		[SerializeField]
		private float _baseDamage;

		public Rigidbody Prefab => _prefab;
		public virtual bool OnlyOne => false;

		public virtual Unit CreateUnit(Rigidbody instance, UnitsManager unitsManager, ProjectilesManager projectilesManager)
		{
			return new Unit(
				instance,
				new Stats
				{
					CurrentHealth = new Observable<float>(_maxHealth),
					MaxHealth = new Observable<float>(_maxHealth),
					MoveSpeed = new Observable<float>(_moveSpeed),
					BaseDamage = new Observable<float>(_baseDamage),
				}
			);
		}
	}
}