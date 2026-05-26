using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
#if UNITY_EDITOR
#endif

namespace CampaignMap
{
    public class UI : MonoBehaviour
    {

        // TODO: Implement transition to battle scene on Invade button click

        public Manager CampaignMapManager;

        public UIDocument UIDocument;

        [SerializeField] [DontCreateProperty] private string _worldName = "";

        [SerializeField] [DontCreateProperty] private string _hexCords = "";

        [SerializeField] [DontCreateProperty] private bool _isInvadeButtonInConfirmState = false;

        public Button ConfirmInvadeButton;

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
            CampaignMapManager.MapItemSelect.AddListener(OnMapItemSelect);

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
            }.ForEach
                (visualElement =>
                    {
                        if (visualElement != null)
                        {
                            visualElement.dataSource = this;
                        }
                    }
                );


            WorldNameLabel?.SetBinding
                (
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(WorldName)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            HexCordsLabel?.SetBinding
                (
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(HexCords)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            InvadeButton?.SetBinding
                (
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(InvadeButtonText)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            // Bind Invade Button + Confirm Invade Button

            if (InvadeButton != null)
            {
                InvadeButton.clicked += OnInvadeButtonClick;

                InvadeButton.RegisterCallback<MouseLeaveEvent>(_ => _isInvadeButtonInConfirmState = false);
            }

            // Hide Panel
            UIDocument.rootVisualElement.style.display = DisplayStyle.None;
        }

        public void OnInvadeButtonClick() {
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

        public void OnMapItemSelect(MapItem mapItem) {
            WorldName = mapItem.ItemName;
            HexCords = mapItem.HexCords.ToString();

            UIDocument.rootVisualElement.style.display
                = !string.IsNullOrEmpty(WorldName) && !string.IsNullOrEmpty(HexCords)
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
        }

    }
}