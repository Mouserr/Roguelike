using Assets.Scripts.Units;

namespace Assets.Scripts
{
	public class UserInfo
	{
		public Observable<float> Gold { get; }

		public UserInfo()
		{
			Gold = new Observable<float>(0);
		}

		public void AddGold(float amount)
		{
			Gold.Value += amount;
		}
	}
}