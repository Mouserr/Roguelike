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
		private Pickup _pickupPrefab;
		[SerializeField]
		private GoldLabel _goldLabel;
		[SerializeField]
		private HealthBar _healthBar;
		[SerializeField]
		private CameraController _camera;
		[SerializeField]
		private LevelController _levelController;

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

			_damageSystem.DamageTaken += (u) => _camera.Shake(0.5f);
		}

		private void Start()
		{
			StartGame();
		}

		public void StartGame()
		{
			var player = _levelController.StartLevel(_unitsManager, _projectilesManager);
			_camera.Init(player.Rigidbody.transform);
			_healthBar.Init(player);
		}

		private void Update()
		{
			_unitsManager.Update();
		}
	}
}