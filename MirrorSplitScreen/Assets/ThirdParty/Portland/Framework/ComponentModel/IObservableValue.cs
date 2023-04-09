using System;

namespace Portland.ComponentModel
{
	public interface IObservableValue<T>
	{
		T Value { get; set; }

		void AddListener(Action<T> listener);
		void AddValidator(Func<T, bool> validator);
		void RemoveListener(Action<T> listener);
		void RemoveValidator(Func<T, bool> validator);
		void Set(T value);
	}
}