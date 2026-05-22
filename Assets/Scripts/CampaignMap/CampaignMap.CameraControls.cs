using UnityEngine;

namespace CampaignMap
{
    public class CameraControls : MonoBehaviour
    {

        public float PanSpeed = 0.03125f;

        public float ZoomSpeed = 1f;

        public float RotateSpeed = 0.125f;

        public float CameraPanYBoundsMin = 1f;

        public float CameraPanYBoundsMax = 5f;

        public float CameraRotateXBoundsMin = 0f;

        public float CameraRotateXBoundsMax = 90f;

        private DefaultControls.CampaignMapActions _campaignMapActions;

        private DefaultControls _defaultControls;

        private Camera _mainCamera;

        public void Awake() {
            _defaultControls = new DefaultControls();
            _campaignMapActions = _defaultControls.CampaignMap;
            _mainCamera = Camera.main;
        }

        public void Update() {
            if (_campaignMapActions.Pan.IsPressed())
            {
                Pan();
            }

            if (_campaignMapActions.Zoom.IsPressed())
            {
                Zoom();
            }

            if (_campaignMapActions.RightClick.IsPressed())
            {
                Rotate();
            }

            if (_campaignMapActions.LeftClick.triggered)
            {
                MouseRaycast();
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

        public void Pan() {
            var panInput = _campaignMapActions.Pan.ReadValue<Vector2>();

            var panMoveVec = _mainCamera.transform.rotation.normalized
                             * new Vector3(panInput.x, 0, panInput.y)
                             * PanSpeed;

            panMoveVec.y = 0;


            _mainCamera.transform.Translate(panMoveVec, Space.World);
        }

        public void Rotate() {
            var mouseDeltaInput = _campaignMapActions.MouseDelta.ReadValue<Vector2>();


            _mainCamera.transform.localEulerAngles = new Vector3
                (
                    Mathf.Clamp
                        (
                            _mainCamera.transform.localEulerAngles.x - mouseDeltaInput.y * RotateSpeed,
                            CameraRotateXBoundsMin,
                            CameraRotateXBoundsMax
                        ),
                    _mainCamera.transform.localEulerAngles.y + mouseDeltaInput.x * RotateSpeed,
                    0
                );
        }

        public void Zoom() {
            // To keep the camera within bounds without stuttering, need to calculate the position it will end up in before allowing it to move there

            // Get the camera's forward vector as a world-space vector and scale it based on our input
            var intendedZoomVec = _mainCamera.transform.forward
                                  * (_campaignMapActions.Zoom.ReadValue<float>() * ZoomSpeed);

            // Predict the camera's ending position by applying the intended zoom vector to its current position
            var predictedCamPos = _mainCamera.transform.position + intendedZoomVec;

            // Clamp the value of the y-axis to our min/max
            predictedCamPos.y = Mathf.Clamp(predictedCamPos.y, CameraPanYBoundsMin, CameraPanYBoundsMax);

            // Calc the difference in the predicted position and the current position
            // If the intended movement does not cause the camera to hit either bounds, this should be equal to the intended zoom vector
            var predictedCamPosDelta = predictedCamPos - _mainCamera.transform.position;

            // This vector can be applied to the camera's position without violating intended bounds
            var safeMoveVec = predictedCamPosDelta;

            // This is to prevent 'skating' when the camera is fully zoomed in or out 
            if (safeMoveVec.y == 0)
            {
                safeMoveVec.z = 0;
                safeMoveVec.x = 0;
            }

            _mainCamera.transform.Translate(safeMoveVec, Space.World);
        }

        public void MouseRaycast() {
            var ray = _mainCamera.ScreenPointToRay(_campaignMapActions.MousePosition.ReadValue<Vector2>());

            if (!Physics.Raycast(ray, out var hit)) return;

            var mapItem = hit.collider.gameObject.GetComponent<MapItem>();

            if (mapItem)
            {
                Manager.Instance.MapItemSelect.Invoke(mapItem);
            }
        }

    }
}