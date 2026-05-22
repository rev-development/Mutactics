using UnityEngine;
using UnityEngine.Events;

namespace CampaignMap
{
    public abstract class MapItem : MonoBehaviour
    {

        public string ItemName;

        public Outline Outline;

        public Vector3Int HexCords;

        public UnityEvent<bool> Select = new();

        protected Manager CampaignMapManager;

        public bool IsSelected { get; protected set; } = false;

        public virtual void OnEnable() {
            if (!Outline)
            {
                Outline = gameObject.GetComponent<Outline>();
            }

            Outline.enabled = IsSelected;
            Select.AddListener(OnSelect);
        }

        public virtual void OnDisable() {
            Select.RemoveAllListeners();
        }

        protected void OnSelect(bool isSelected) {
            IsSelected = isSelected;
            Outline.enabled = IsSelected;
        }

        public void AssignCords(Vector3Int hexCords, Manager campaignMapManager, string itemName = "MapItem") {
            CampaignMapManager = campaignMapManager;
            HexCords = hexCords;
            gameObject.name = itemName;
        }

        protected void CenterSelf() {
            if (!CampaignMapManager)
            {
                CampaignMapManager = Manager.Instance;
            }

            if (CampaignMapManager.Tilemap.WorldToCell(transform.position) != HexCords)
            {
                transform.position = CampaignMapManager.Tilemap.CellToWorld(HexCords);
            }
        }

    }
}