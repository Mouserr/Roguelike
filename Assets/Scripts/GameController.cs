using System.Collections.Generic;
using Assets.Scripts.Pickups;
using Assets.Scripts.Projectiles;
using Assets.Scripts.UI;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts
{
	public class GameController : MonoBehaviour
	{
		[SerializeField]
		private Vector3 _forwardDirection;
		[SerializeField]
		private Rigidbody _player;
		[SerializeField]
		private Animator _playerAnimator;
		[SerializeField]
		private LauncherConfig _playerLauncherConfig;
		[SerializeField]
		private Transform _playerLaunchTransform;
		[SerializeField]
		private Pickup _pickupPrefab;
		[SerializeField]
		private GoldLabel _goldLabel;
		[SerializeField]
		private CameraShakeController _cameraShake;
		[SerializeField]
		private List<Rigidbody> _enemies;

		private UserInfo _userInfo;

		private UnitsManager _unitsManager;
		private DamageSystem _damageSystem;
		private PickupsManager _pickupsManager;
		private ProjectilesManager _projectilesManager;

		private void Awake()
		{
			_userInfo = new UserInfo();
			_unitsManager = new UnitsManager();
			_damageSystem = new DamageSystem();
			_pickupsManager = new PickupsManager(_unitsManager, _userInfo, _pickupPrefab);
			_projectilesManager = new ProjectilesManager(_damageSystem);
			_goldLabel.Init(_userInfo);

			_damageSystem.DamageTaken += (u) => _cameraShake.Shake(0.5f);
		}

		private void Start()
		{
			StartGame();
		}

		public void StartGame()
		{
			_unitsManager.Add(
				new Unit(
					_player,
					new Stats
					{
						CurrentHealth = new Observable<float>(10),
						MaxHealth = new Observable<float>(10),
						MoveSpeed = new Observable<float>(5),
						BaseDamage = new Observable<float>(1),
					},
					new InputSystem(_forwardDirection),
					new MovementSystem(_player),
					new CollectPickups(),
					new AutoShooting(_unitsManager, _projectilesManager, new Launcher(_playerLauncherConfig, _playerLaunchTransform)),
					new AnimatorSystem(_playerAnimator)));

			foreach (var enemy in _enemies)
			{
				_unitsManager.Add(
					new Unit(
						enemy,
						new Stats()
						{
							CurrentHealth = new Observable<float>(10),
							MaxHealth = new Observable<float>(3),
							MoveSpeed = new Observable<float>(5),
							BaseDamage = new Observable<float>(1),
						},
						new RandomPatrol(5),
						new MovementSystem(enemy)
					)
					{
						Fraction = 1
					});
			}
		}

		private void Update()
		{
			_unitsManager.Update();
		}
	}
}