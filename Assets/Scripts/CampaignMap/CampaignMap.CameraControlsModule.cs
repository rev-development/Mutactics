using Core.Map.Camera;
using UnityEngine;

namespace CampaignMap
{
    [AddComponentMenu("Camera Controls - CampaignMap Module")]
    [RequireComponent(typeof(Controls))]
    public class CameraControlsModule : MonoBehaviour
    {

        private Controls _controls;

        public void Awake() {
            _controls = gameObject.GetComponent<Controls>();
        }

        public void OnEnable() {
            _controls?.MouseRaycasted.AddListener(OnMouseRaycasted);
        }

        public void OnMouseRaycasted(Ray ray) {
            if (!Physics.Raycast(ray, out var hit)) return;

            if (hit.collider.gameObject.TryGetComponent(out World.World gridItem))
            {
                Manager.Instance.SelectGridItem(gridItem);
            }
        }

    }
}