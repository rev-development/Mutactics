using Core.Map.GridItem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace CampaignMap.World
{
    public class World : GridItem<IWorldData, WorldSO>
    {

        public UnityEvent<bool> PlayerControlChanged;
        public WorldSO WorldSO;
        public Outline Outline;
        public Tilemap Tilemap;

        public void Start() {
            PlayerControlChanged.Invoke(WorldSO.IsPlayerControlled);
        }

        public override void OnEnable() {
            base.OnEnable();

            if (!Outline)
            {
                Outline = gameObject.GetComponent<Outline>();
            }

            Outline.enabled = IsSelected;

            PlayerControlChanged.AddListener(OnPlayerControlChanged);
        }

        public override void OnDisable() {
            base.OnDisable();
            PlayerControlChanged.RemoveAllListeners();
        }

        protected override void OnSelect(bool isSelected) {
            base.OnSelect(isSelected);
            Outline.enabled = IsSelected;
        }

        private void OnPlayerControlChanged(bool isPlayerControlled) {
            WorldSO.IsPlayerControlled = isPlayerControlled;
            Helpers.Shaders.ChangeSimpleColor(WorldSO.IsPlayerControlled ? Color.blue : Color.red, gameObject);
        }

    }
}