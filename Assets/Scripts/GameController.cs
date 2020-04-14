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

		private UserInfo _userInfo;
		private UnitsManager _unitsManager;

		private void Awake()
		{
			_userInfo = new UserInfo();
			_unitsManager = new UnitsManager();
		}

		private void Start()
		{
			StartGame();
		}

		public void StartGame()
		{
			_unitsManager.Add(new Unit(
				new Stats
				{
					CurrentHealth = 10,
					MaxHealth = 10,
					MoveSpeed = 5
				}, 
				new InputSystem(_forwardDirection), 
				new MovementSystem(_player),
				new AnimatorSystem(_playerAnimator)));
		}

		private void Update()
		{
			_unitsManager.Update();
		}
	}
}