using Unity.Properties;
using UnityEngine;
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

        [CreateProperty]
        public string WorldName { get => _worldName; set => _worldName = value; }

        [CreateProperty]
        public string HexCords { get => _hexCords; set => _hexCords = value; }

        public void OnEnable() {
            CampaignMapManager.MapItemSelect.AddListener(OnMapItemSelect);

            var worldNameLabel = UIDocument.rootVisualElement.Q<Label>("WorldName");
            worldNameLabel.dataSource = this;

            worldNameLabel.SetBinding
                (
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(WorldName)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            var hexCordsLabel = UIDocument.rootVisualElement.Q<Label>("HexCords");
            hexCordsLabel.dataSource = this;

            hexCordsLabel.SetBinding
                (
                    "text",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(HexCords)),
                        bindingMode = BindingMode.ToTarget
                    }
                );

            UIDocument.rootVisualElement.style.display = DisplayStyle.None;
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