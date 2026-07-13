using System;
using System.Collections.Generic;
using System.Linq;

namespace Rev.Helpers
{
	[Serializable]
	public class Timer
	{

		public float BaseAlarmTime = 1f;

		public float AlarmVarianceLowerBound = 0f;

		public float AlarmVarianceUpperBound = 0f;

		public Timer(float baseAlarmTime = 1f, float alarmVarianceLowerBound = 0f, float alarmVarianceUpperBound = 0f) {
			BaseAlarmTime = baseAlarmTime;
			AlarmVarianceLowerBound = alarmVarianceLowerBound;
			AlarmVarianceUpperBound = alarmVarianceUpperBound;
		}

		public float CurrentAlarmTime { get; private set; }

		public float ElapsedTime { get; private set; }

		public bool Running { get; private set; } = false;

		public bool Ringing { get; private set; } = false;

		/// <summary>
		///     Advances the timer. Call this once per frame (e.g. from MonoBehaviour.Update)
		///     with Time.deltaTime, passing the relevant delta for your use case
		///     (Time.deltaTime, Time.unscaledDeltaTime, etc).
		/// </summary>
		public void Tick(float deltaTime) {
			if (!Running || Ringing) return;

			ElapsedTime += deltaTime;

			if (ElapsedTime >= CurrentAlarmTime)
			{
				ElapsedTime = CurrentAlarmTime;
				Ringing = true;
				Running = false;
			}
		}

		public void GenerateAlarmTime() =>
			CurrentAlarmTime
				= BaseAlarmTime + UnityEngine.Random.Range(AlarmVarianceLowerBound, AlarmVarianceUpperBound);

		public void StartTimer() {
			GenerateAlarmTime();
			ElapsedTime = 0f;
			Ringing = false;
			Running = true;
		}

		public void RestartTimer() => StartTimer();

		public void ResetTimer() {
			GenerateAlarmTime();
			ElapsedTime = 0f;
			Ringing = false;
			Running = false;
		}

		public void StopTimer() => Running = false;

		public static List<Timer> FilterRinging(List<Timer> timers) => timers.Where(timer => timer.Ringing).ToList();

	}
}