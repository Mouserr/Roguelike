using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
	public struct ProjectileInfo
	{
		public float Damage;
		public Launcher Launcher;
		public Unit Sender;
		public Vector3 StartPosition; 
		public Vector3 TargetPosition;
	}
}