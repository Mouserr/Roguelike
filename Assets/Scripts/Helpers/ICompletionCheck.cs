using System;

namespace Assets.Scripts.Helpers
{
	public interface ICompletionCheck
	{
		event Action OnComplete;
	}
}