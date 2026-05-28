using Core.Map;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace CampaignMap
{
    public class World : GridItem
    {

        #region Config Values

        [Header("Config Values")]
        public WorldData WorldData;

        #endregion

        #region Runtime Values

        public UnityEvent<bool> PlayerControlChanged;

        #endregion

        public void AssignData(WorldDataStruct worldDataStruct) {
            WorldData = ScriptableObject.CreateInstance<WorldData>();
            WorldData.AssignData(worldDataStruct);
            ApplyData();
        }

        [ContextMenu("Apply Data")]
        public void ApplyData() {
            if (!WorldData) return;

            gameObject.name = WorldData.ItemName;

            if (CampaignMap.Manager.Instance)
            {
                gameObject.transform.position = CampaignMap.Manager.Instance.Tilemap.CellToWorld(Cell);
            }
            else if (Tilemap)
            {
                gameObject.transform.position = Tilemap.CellToWorld(Cell);
            }
            else
            {
                var tilemap = gameObject.GetComponentInParent<Tilemap>();

                if (tilemap)
                {
                    gameObject.transform.position = tilemap.CellToWorld(Cell);
                }
            }

            // Offset for Tilemap Alignment
            if (gameObject.TryGetComponent<MeshFilter>(out var proBuilderMesh))
            {
                gameObject.transform.position = new Vector3
                    (
                        gameObject.transform.position.x,
                        gameObject.transform.position.y + proBuilderMesh.sharedMesh.bounds.size.y / 2,
                        gameObject.transform.position.z
                    );
            }
        }

        #region Listeners

        protected override void OnSelect(bool isSelected) {
            base.OnSelect(isSelected);
            Outline.enabled = IsSelected;
        }

        #endregion

        #region References

        [Header("References")]
        public Outline Outline;
        public Tilemap Tilemap;

        #endregion

        #region Lifecycle

        public void Start() {
            PlayerControlChanged.Invoke(WorldData.IsPlayerControlled);
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

        private void OnPlayerControlChanged(bool isPlayerControlled) {
            WorldData.IsPlayerControlled = isPlayerControlled;
            Helpers.Shaders.ChangeSimpleColor(WorldData.IsPlayerControlled ? Color.blue : Color.red, gameObject);
        }

        public override void OnDisable() {
            base.OnDisable();
            PlayerControlChanged.RemoveAllListeners();
        }

        public void OnDestroy() {
            Destroy(WorldData);
        }

        #endregion

    }
}