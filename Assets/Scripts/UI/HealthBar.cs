using Assets.Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	public class HealthBar : MonoBehaviour
	{
		private Unit _unit;

		[SerializeField]
		private Slider _slider;

		public void Init(Unit unit)
		{
			_unit = unit;
			_slider.maxValue = _unit.Stats.MaxHealth;
			_slider.value = _unit.Stats.CurrentHealth;
			_unit.Stats.CurrentHealth.OnChanged += OnHealthChanged;
		}

		private void OnHealthChanged(float newValue)
		{
			_slider.value = newValue;
		}
	}
}