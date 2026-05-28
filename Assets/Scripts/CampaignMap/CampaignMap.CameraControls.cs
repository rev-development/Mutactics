using UnityEngine;

namespace CampaignMap
{
    [AddComponentMenu("Campaign Map Camera Controls")]
    public class CameraControls : global::CameraControls
    {

        public override void OnEnable() {
            base.OnEnable();
            MouseRaycasted.AddListener(OnMouseRaycasted);
        }

        public void OnMouseRaycasted(RaycastHit hit) {
            if (hit.collider.gameObject.TryGetComponent(out MapItem mapItem))
            {
                Manager.Instance.MapItemSelected.Invoke(mapItem);
            }
        }

    }
}