using UnityEngine;

namespace Rev.Helpers
{
	public class NavBeacon : MonoBehaviour
	{

		public global::Helpers.Events.Channels.GameObjectEC NavBeaconEC;

		private void Awake() => NavBeaconEC.RaiseEvent(gameObject);

	}
}