using Core.Map.Camera;
using UnityEngine;

namespace BattleMap
{
    [AddComponentMenu("Camera Controls - BattleMap Module")]
    [RequireComponent(typeof(Controls))]
    public class CameraControlsModule : MonoBehaviour
    {

        private Controls _controls;

        private void Awake() {
            _controls = gameObject.GetComponent<Controls>();
        }

        private void OnEnable() {
            _controls.MouseRaycasted.AddListener(OnMouseRaycasted);
            _controls.EscapeStarted.AddListener(OnEscapeStarted);
            _controls.RightClickStarted.AddListener(OnRightClickStarted);
        }

        private void OnEscapeStarted() {
            UnifiedManager.Deselect(true);
        }

        private void OnRightClickStarted() {
            UnifiedManager.Deselect();
        }

        private void OnMouseRaycasted(Ray ray) {
            // TODO: This should maybe just be the UnifiedManager
            var layerMask = UnifiedManager.GetSelectionLayerMask();

            if (Physics.Raycast(
                        ray,
                        out var hit,
                        Mathf.Infinity,
                        layerMask
                    ))
            {
                if (hit.collider.gameObject.TryGetComponent(out BattleMap.Hex.Hex hex))
                {
                    Hex.Manager.Instance.SelectGridItem(hex);
                }
                else if (hit.collider.gameObject.TryGetComponent(out BattleMap.Pawn.Pawn pawn))
                {
                    Pawn.Manager.Instance.SelectGridItem(pawn);
                }
            }
            else
            {
                UnifiedManager.Deselect();
            }
        }

    }
}