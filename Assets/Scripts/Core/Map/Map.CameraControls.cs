using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

// ReSharper disable MemberCanBePrivate.Global

namespace Core.Map
{
    public class CameraControls : MonoBehaviour
    {

        private DefaultControls.MainActions _actions;
        private DefaultControls _defaultControls;
        private Camera _mainCamera;

        public void Pan() {
            var panMoveVec = _mainCamera.transform.rotation.normalized
                             * new Vector3(PanInput.x, 0, PanInput.y)
                             * (PanSpeed * Time.deltaTime);

            panMoveVec.y = 0;
            _mainCamera.transform.Translate(panMoveVec, Space.World);
        }

        public void Rotate() {
            _mainCamera.transform.localEulerAngles = new Vector3
                (
                    Mathf.Clamp
                        (
                            _mainCamera.transform.localEulerAngles.x - RotateInput.y * RotateSpeed * Time.deltaTime,
                            CameraRotateXBoundsMin,
                            CameraRotateXBoundsMax
                        ),
                    _mainCamera.transform.localEulerAngles.y + RotateInput.x * RotateSpeed * Time.deltaTime,
                    0
                );

            RotateInput = Vector2.zero;
        }

        public void Zoom(float zoomInput) {
            // To keep the camera within bounds without stuttering, need to calculate the position it will end up in before allowing it to move there

            // Get the camera's forward vector as a world-space vector and scale it based on our input
            var intendedZoomVec = _mainCamera.transform.forward * (zoomInput * ZoomSpeed);

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

        public void Zoom() {
            Zoom(ZoomInput * Time.deltaTime);
        }

        public void MouseRaycast(InputAction.CallbackContext _) {
            var ray = _mainCamera.ScreenPointToRay(_actions.MousePosition.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out var hit))
            {
                LastRaycastTarget = hit.collider.gameObject;
                MouseRaycasted.Invoke(hit);
            }
        }

        #region Lifecycle

        public void Awake() {
            _defaultControls = new DefaultControls();
            _actions = _defaultControls.Main;
            _mainCamera = Camera.main;
        }

        public void Update() {
            if (PanInput != Vector2.zero)
            {
                Pan();
            }

            if (ZoomInput != 0f)
            {
                Zoom();
            }

            if (IsRotating)
            {
                Rotate();
            }
        }

        public void OnEnable() {
            _defaultControls.Enable();

            _actions.Pan.performed += OnPanPerformed;
            _actions.Pan.canceled += OnPanCanceled;

            _actions.Zoom.performed += OnZoomPerformed;
            _actions.Zoom.canceled += OnZoomCanceled;

            _actions.ScrollZoom.performed += OnScrollZoomPerformed;

            _actions.RightClick.started += OnRightClickStarted;
            _actions.RightClick.canceled += OnRightClickCanceled;

            _actions.MouseDelta.performed += OnMouseDeltaPerformed;

            _actions.LeftClick.started += MouseRaycast;
        }

        public void OnDisable() {
            _actions.Pan.performed -= OnPanPerformed;
            _actions.Pan.canceled -= OnPanCanceled;

            _actions.Zoom.performed -= OnZoomPerformed;
            _actions.Zoom.canceled -= OnZoomCanceled;

            _actions.ScrollZoom.performed -= OnScrollZoomPerformed;

            _actions.RightClick.started -= OnRightClickStarted;
            _actions.RightClick.canceled -= OnRightClickCanceled;

            _actions.MouseDelta.performed -= OnMouseDeltaPerformed;

            _actions.LeftClick.started -= MouseRaycast;

            _defaultControls.Disable();

            MouseRaycasted.RemoveAllListeners();
        }

        public void OnDestroy() {
            OnDisable();
            _defaultControls.Dispose();
        }

        #endregion

        #region Runtime Values

        [Header("Runtime Values")]
        public Vector2 PanInput;
        public float ZoomInput;
        public bool IsRotating;
        public Vector2 RotateInput;
        public GameObject LastRaycastTarget;

        public UnityEvent<RaycastHit> MouseRaycasted = new();

        #endregion

        #region Config Values

        [Header("Config Values")]
        public float PanSpeed = 4f;
        public float ZoomSpeed = 4f;
        public float RotateSpeed = 40f;

        public float CameraPanYBoundsMin = 1f;
        public float CameraPanYBoundsMax = 5f;

        public float CameraRotateXBoundsMin = 0f;
        public float CameraRotateXBoundsMax = 90f;

        public float ScrollZoomMultiplier = 0.0625f;

        #endregion

        #region Action Listeners

        protected void OnMouseDeltaPerformed(InputAction.CallbackContext context) {
            if (!IsRotating) return;

            RotateInput = context.ReadValue<Vector2>();
        }

        protected void OnRightClickCanceled(InputAction.CallbackContext _) {
            IsRotating = false;
            RotateInput = Vector2.zero;
        }

        protected void OnRightClickStarted(InputAction.CallbackContext _) {
            IsRotating = true;
        }

        protected void OnScrollZoomPerformed(InputAction.CallbackContext context) {
            Zoom(context.ReadValue<float>() * ScrollZoomMultiplier);
        }

        protected void OnPanPerformed(InputAction.CallbackContext context) {
            PanInput = context.ReadValue<Vector2>();
        }

        protected void OnPanCanceled(InputAction.CallbackContext context) {
            PanInput = Vector2.zero;
        }

        protected void OnZoomPerformed(InputAction.CallbackContext context) {
            ZoomInput = context.ReadValue<float>();
        }

        protected void OnZoomCanceled(InputAction.CallbackContext _) {
            ZoomInput = 0f;
        }

        #endregion

    }
}