using System;
using System.Collections.Generic;

namespace Rev.Helpers
{
	/// <summary>
	///     Holds a list of elements and hands out random elements one at a time without
	///     repeating until every element has been chosen ("shuffle bag" pattern). Once
	///     exhausted, the bag automatically reshuffles and starts over — guaranteeing no
	///     immediate repeat at the reshuffle boundary unless the source list has 1 element.
	/// </summary>
	[Serializable]
	[AiGenerated("Claude", "Sonnet 4.6")]
	public class RandomBag<T>
	{

		public bool AutoRefillOnExhaustion = true;

		private List<T> _bag;

		private List<T> _source;

		public RandomBag() {
		}

		public RandomBag(IEnumerable<T> source) => Init(source);

		public int RemainingCount => _bag.Count;

		public bool IsInitialized { get; private set; }

		/// <summary>
		///     Sets (or replaces) the source pool and reshuffles. Required before drawing
		///     when using the parameterless constructor. Safe to call again later to swap
		///     the pool at runtime (e.g. unlocking new variants).
		/// </summary>
		public void Init(IEnumerable<T> source) {
			_source = new List<T>(source);
			_bag = new List<T>();
			IsInitialized = true;
			Refill();
		}

		public T Next() {
			TryNext(out var result);

			return result;
		}

		/// <summary>
		///     Attempts to draw the next random unchosen element. Returns false (and logs an
		///     error) if the bag has no source elements, instead of silently handing back
		///     default(T) — which is indistinguishable from a real 0/false/struct(0) for
		///     value types. Prefer this over Next() whenever T might be a value type.
		/// </summary>
		public bool TryNext(out T result) {
			if (!IsInitialized)
			{
				UnityEngine.Debug.LogError("RandomBag.Next/TryNext called before Init(). Call Init(source) first.");
				result = default;

				return false;
			}

			if (_source.Count == 0)
			{
				UnityEngine.Debug.LogError("RandomBag has no source elements to choose from.");
				result = default;

				return false;
			}

			var attempts = 0;
			var maxAttempts = _source.Count;

			while (attempts < maxAttempts)
			{
				attempts++;

				if (_bag.Count == 0)
				{
					if (!AutoRefillOnExhaustion)
					{
						UnityEngine.Debug.LogWarning("RandomBag is exhausted and AutoRefillOnExhaustion is false.");
						result = default;

						return false;
					}

					Refill();
				}

				var lastIndex = _bag.Count - 1;
				var candidate = _bag[lastIndex];
				_bag.RemoveAt(lastIndex);

				if (IsValid(candidate))
				{
					result = candidate;

					return true;
				}

				UnityEngine.Debug.LogWarning(
						"RandomBag skipped a destroyed/null entry. Consider calling RemoveItem to clean it up."
					);

				_source.Remove(candidate);
			}

			UnityEngine.Debug.LogError("RandomBag has no valid (non-destroyed) elements left to choose from.");
			result = default;

			return false;
		}

		/// <summary>
		///     Catches Unity's "fake null" case: a destroyed GameObject/Component whose
		///     C# reference is still non-null but should be treated as gone. Plain
		///     reference types use standard null checks; value types are always valid.
		/// </summary>
		private static bool IsValid(T item) {
			if (item is UnityEngine.Object unityObject) return unityObject != null;

			if (typeof(T).IsValueType) return true;

			return item != null;
		}

		/// <summary>
		///     Adds an item to the source pool. The item also becomes immediately
		///     drawable in the current cycle (added to the live bag), not just on
		///     the next reshuffle.
		/// </summary>
		public void AddItem(T item) {
			if (!IsInitialized)
			{
				UnityEngine.Debug.LogError("RandomBag.AddItem called before Init(). Call Init(source) first.");

				return;
			}

			_source.Add(item);
			_bag.Add(item);
		}

		/// <summary>
		///     Removes the first matching item from the source pool and, if present,
		///     from the current live bag. Returns false (and logs a warning) if no
		///     matching item was found — this is treated as a caller mistake, not
		///     a hard error.
		/// </summary>
		public bool RemoveItem(T item) {
			if (!IsInitialized)
			{
				UnityEngine.Debug.LogError("RandomBag.RemoveItem called before Init(). Call Init(source) first.");

				return false;
			}

			var removedFromSource = _source.Remove(item);

			if (!removedFromSource)
			{
				UnityEngine.Debug.LogWarning("RandomBag.RemoveItem could not find a matching item in the source pool.");

				return false;
			}

			_bag.Remove(item);

			return true;
		}

		public void Refill() {
			_bag = new List<T>(_source);
			Shuffle(_bag);
		}

		private static void Shuffle(List<T> list) {
			for (var i = list.Count - 1; i > 0; i--)
			{
				var j = UnityEngine.Random.Range(0, i + 1);
				(list[i], list[j]) = (list[j], list[i]);
			}
		}

	}
}