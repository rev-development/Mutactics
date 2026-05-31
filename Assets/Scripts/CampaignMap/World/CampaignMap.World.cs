using Core.Map.GridItem;
using UnityEngine;
using UnityEngine.Events;

namespace CampaignMap.World
{
    public class World : GridItem<IWorldData, WorldSO>
    {

        public GridItemOptions GridItemOptions = new()
        {
            ClearTilemapOnReset = true,
            PlaceObjectBelowGrid = true,
            OffsetProBuilderMesh = true
        };
        public UnityEvent<bool> PlayerControlChanged;

        public void Start() {
            PlayerControlChanged.Invoke(DataSO.IsPlayerControlled);
        }

        public override void OnEnable() {
            base.OnEnable();

            PlayerControlChanged.AddListener(OnPlayerControlChanged);
        }

        public override void OnDisable() {
            base.OnDisable();
            PlayerControlChanged.RemoveAllListeners();
        }

        private void OnPlayerControlChanged(bool isPlayerControlled) {
            DataSO.IsPlayerControlled = isPlayerControlled;
            Helpers.Shaders.ChangeSimpleColor(DataSO.IsPlayerControlled ? Color.blue : Color.red, gameObject);
        }

    }
}