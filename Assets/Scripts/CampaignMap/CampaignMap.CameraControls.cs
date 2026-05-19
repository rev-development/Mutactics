using UnityEngine;
using UnityEngine.InputSystem;

namespace CampaignMap
{
    public class CameraControls : MonoBehaviour, DefaultControls.ICampaignMapActions
    {

        public float PanSpeed;

        private DefaultControls.CampaignMapActions _campaignMapActions;

        private DefaultControls _defaultControls;

        private Camera _mainCamera;

        public void Awake() {
            _defaultControls = new DefaultControls();
            _campaignMapActions = _defaultControls.CampaignMap;
            _campaignMapActions.AddCallbacks(this);
            _mainCamera = Camera.main;
        }

        public void Update() {
            if (_campaignMapActions.Pan.IsPressed())
            {
                var rawInput = _campaignMapActions.Pan.ReadValue<Vector2>();
                _mainCamera.transform.Translate(new Vector3(rawInput.x, 0, rawInput.y) * PanSpeed);
            }
        }

        public void OnEnable() {
            _defaultControls.Enable();
        }

        public void OnDisable() {
            _defaultControls.Disable();
        }

        public void OnDestroy() {
            _defaultControls.Dispose();
        }

        public void OnPan(InputAction.CallbackContext context) {
        }

    }
}