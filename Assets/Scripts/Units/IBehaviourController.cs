namespace Assets.Scripts.Units
{
	public interface IBehaviourController
	{
		bool IsActive { get; }
		void Update(Unit unit);
	}
}