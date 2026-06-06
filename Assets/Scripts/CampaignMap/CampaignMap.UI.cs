using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
#if UNITY_EDITOR
#endif

namespace CampaignMap
{
    [AddComponentMenu("CampaignMap.UI")]
    public class UI : MonoBehaviour
    {

        public Manager CampaignMapManager;

        public UIDocument UIDocument;

        [SerializeField] [DontCreateProperty] private string _worldName = "";

        [SerializeField] [DontCreateProperty] private string _hexCords = "";

        [SerializeField] [DontCreateProperty] private bool _isInvadeButtonInConfirmState = false;

        public Label HexCordsLabel;

        public Button InvadeButton;

        public Label WorldNameLabel;

        [CreateProperty]
        public string WorldName { get => _worldName; set => _worldName = value; }
        [CreateProperty]
        public string HexCords { get => _hexCords; set => _hexCords = value; }
        [CreateProperty]
        public string InvadeButtonText => _isInvadeButtonInConfirmState ? "Confirm" : "Invade";

        public void OnEnable() {
            // Attach listeners to Unity Events (separate from UI binding)
            CampaignMapManager.GridItemSelected.AddListener(OnGridItemSelect);

            // Bind labels
            var root = UIDocument.rootVisualElement;
            WorldNameLabel = root.Q<Label>("WorldName");
            HexCordsLabel = root.Q<Label>("HexCords");
            InvadeButton = root.Q<Button>("InvadeButton");

            new List<VisualElement>
            {
                WorldNameLabel,
                HexCordsLabel,
                InvadeButton
            }.ForEach(visualElement =>
                    {
                        if (visualElement != null)
                        {
                            visualElement.dataSource = this;
                        }
                    }
                );


            WorldNameLabel?.SetBinding(
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(WorldName)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            HexCordsLabel?.SetBinding(
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(HexCords)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            InvadeButton?.SetBinding(
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(InvadeButtonText)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            if (InvadeButton != null)
            {
                InvadeButton.clicked += OnInvadeButtonClick;

                InvadeButton.RegisterCallback<MouseLeaveEvent>(_ => _isInvadeButtonInConfirmState = false);
            }

            // Hide Panel
            UIDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        private void OnInvadeButtonClick() {
            if (!_isInvadeButtonInConfirmState)
            {
                _isInvadeButtonInConfirmState = true;
            }
            else
            {
                SceneManager.LoadScene("BattleMap");
            }
            // TODO: Scene Transition w/ Persistent Data Save
        }

        private void OnGridItemSelect(World.World worldItem) {
            if (!worldItem.gameObject.TryGetComponent<World.World>(out var world)) return;

            WorldName = world.DataSO.Name;
            HexCords = world.DataSO.Cell.ToString();

            UIDocument.rootVisualElement.style.display
                = !string.IsNullOrEmpty(WorldName) && !string.IsNullOrEmpty(HexCords)
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
        }

    }
}