namespace Assets.Scripts.Units
{
	public interface IBehaviourSystem
	{
		bool IsActive { get; }
		void Update(Unit unit);
	}
}