using UnityEngine;
using UnityEngine.Events;

namespace Core.Map.GridItem.Composables
{
    [AddComponentMenu("Map.GridItem.Selectable")]
    public class Selectable : MonoBehaviour
    {

        public UnityEvent<bool> Select = new();

        private Outline _outline;

        public bool IsSelected { get; protected set; } = false;

        public void Awake() {
            _outline = GetComponent<Outline>();
        }

        public virtual void OnEnable() {
            if (!_outline)
            {
                _outline = gameObject.GetComponent<Outline>();
            }

            _outline.enabled = IsSelected;

            Select.AddListener(OnSelect);
        }

        public virtual void OnDisable() {
            Select.RemoveAllListeners();
        }

        protected virtual void OnSelect(bool isSelected) {
            IsSelected = isSelected;
            _outline.enabled = IsSelected;
        }

        public static void TrySelect(GameObject go, bool value) {
            if (go.TryGetComponent<Selectable>(out var selectable))
            {
                selectable.Select.Invoke(value);
            }
        }

    }
}