using Assets.Scripts.Projectiles;
using Assets.Scripts.Units;

namespace Assets.Scripts
{
	public class DamageSystem
	{
		public void ApplyDamage(ProjectileInfo projectileInfo, Unit unit)
		{
			if (unit.IsAlive)
			{
				unit.Stats.CurrentHealth.Value -= projectileInfo.Damage;
			}
		}
	}
}