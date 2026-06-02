using Core.Map;
using UnityEngine;

namespace BattleMap
{
    [AddComponentMenu("Camera Controls - BattleMap Module")]
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

        public void OnMouseRaycasted(Ray ray) {
            var layerMask = Pawn.Manager.Instance.ActiveSelection != null
                ? LayerMask.GetMask("Hex")
                : LayerMask.GetMask("Pawn");

            if (Physics.Raycast(
                        ray,
                        out var hit,
                        Mathf.Infinity,
                        layerMask
                    ))
            {
                if (hit.collider.gameObject.TryGetComponent(out BattleMap.Hex.Hex hex))
                {
                    Hex.Manager.Instance.GridItemSelected.Invoke(hex);
                }
                else if (hit.collider.gameObject.TryGetComponent(out BattleMap.Pawn.Pawn pawn))
                {
                    Pawn.Manager.Instance.GridItemSelected.Invoke(pawn);
                }
            }
        }

    }
}