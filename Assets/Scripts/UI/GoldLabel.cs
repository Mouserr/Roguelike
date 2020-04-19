using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class GoldLabel : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _text;

		public void Init(UserInfo userInfo)
		{
			userInfo.Gold.OnChanged += OnGoldChanged;
		}

		private void OnGoldChanged(float newValue)
		{
			_text.text = newValue.ToString("N0");
		}
	}
}