using Core.Map;
using UnityEngine;

namespace CampaignMap
{
    [AddComponentMenu("Camera Controls - CampaignMap Module")]
    [RequireComponent(typeof(CameraControls))]
    public class CameraControlsModule : MonoBehaviour
    {

        private CameraControls _cameraControls;

        public void Awake() {
            _cameraControls = gameObject.GetComponent<CameraControls>();
        }

        public void OnEnable() {
            _cameraControls?.MouseRaycasted.AddListener(OnMouseRaycasted);
        }

        public void OnMouseRaycasted(RaycastHit hit) {
            if (hit.collider.gameObject.TryGetComponent(out World.World gridItem))
            {
                Manager.Instance.GridItemSelected.Invoke(gridItem);
            }
        }

    }
}