using Core.Map.GridItem;
using UnityEngine.Events;

namespace CampaignMap.World
{
    public class World : GridItem<WorldSO, WorldData, IWorldData>
    {

        public GridItemOptions GridItemOptions = new()
        {
            ClearTilemapOnReset = true,
            PlaceObjectBelowGrid = true
        };

        public UnityEvent<bool> PlayerControlChanged;

        // BUG:
        // public void Start() {
        //     PlayerControlChanged.Invoke(DataSO.IsPlayerControlled);
        // }

        // BUG:
        // public void OnEnable() {
        //     PlayerControlChanged.AddListener(OnPlayerControlChanged);
        // }

        public void OnDisable() {
            PlayerControlChanged.RemoveAllListeners();
        }

        // BUG:
        // private void OnPlayerControlChanged(bool isPlayerControlled) {
        //     DataSO.IsPlayerControlled = isPlayerControlled;
        //     Helpers.Shaders.ChangeSimpleColor(DataSO.IsPlayerControlled ? Color.blue : Color.red, gameObject);
        // }

    }
}