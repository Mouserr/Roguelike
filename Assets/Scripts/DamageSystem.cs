using System;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Units;

namespace Assets.Scripts
{
	public class DamageSystem
	{
		public event Action<Unit> DamageTaken;

		public void ApplyDamage(ProjectileInfo projectileInfo, Unit unit)
		{
			if (unit.IsAlive && projectileInfo.Sender != unit)
			{
				unit.Stats.CurrentHealth.Value -= projectileInfo.Damage;
				DamageTaken?.Invoke(unit);
			}
		}
	}
}