using System;

namespace Assets.Scripts.Units
{
	public class Observable<T>
	{
		private T _value;
		public event Action<T> OnChanged;

		public T Value
		{
			get { return _value; }
			set
			{
				_value = value;
				OnChanged?.Invoke(_value);
			}
		}
		public Observable(T value)
		{
			Value = value;
		}

		public static implicit operator Observable<T>(T value)
		{
			return new Observable<T>(value);
		}

		public static implicit operator T(Observable<T> observable)
		{
			return observable.Value;
		}
	}
}