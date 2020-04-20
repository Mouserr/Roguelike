using System.Collections.Generic;

namespace Assets.Scripts.Helpers
{
	public static class RandomHelper
	{
		public static List<T> ShuffleList<T>(List<T> list)
		{
			List<T> result = new List<T>(list.Count);
			ShuffleList(list, ref result);
			return result;
		}

		public static void ShuffleList<T>(List<T> source, ref List<T> destination)
		{
			List<T> availableItems = new List<T>(source);
			destination.Clear();
			for (int i = 0; i < source.Count; i++)
			{
				int itemIndex = UnityEngine.Random.Range(0, availableItems.Count);
				destination.Add(availableItems[itemIndex]);
				availableItems.RemoveAt(itemIndex);
			}
		}
	}
}