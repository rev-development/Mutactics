using UnityEngine;
using UnityEngine.Events;

namespace CampaignMap
{
    public class World : MapItem
    {

        public bool IsPlayerControlled = false;

        public UnityEvent<bool> PlayerControlChanged;

        public void Start() {
            PlayerControlChanged.Invoke(IsPlayerControlled);
        }

        public override void OnEnable() {
            base.OnEnable();
            PlayerControlChanged.AddListener(OnPlayerControlChanged);
        }

        public override void OnDisable() {
            base.OnDisable();
            PlayerControlChanged.RemoveAllListeners();
        }

        public void OnPlayerControlChanged(bool isPlayerControlled) {
            IsPlayerControlled = isPlayerControlled;
            Helpers.Shaders.ChangeSimpleColor(IsPlayerControlled ? Color.blue : Color.red, gameObject);
        }

    }
}