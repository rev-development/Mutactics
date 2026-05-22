using System;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace CampaignMap
{
    public class UI : MonoBehaviour, INotifyBindablePropertyChanged
    {

        private Manager _campaignMapManager;

        private string _hexCords;

        private TextField _hexCordsTextField;

        private UIDocument _uiDocument;

        private string _worldName;

        private TextField _worldNameTextField;

        [CreateProperty]
        public string WorldName
        {
            get => _campaignMapManager.MapItemSelected.ItemName;

            set
            {
                _worldName = value;
                Notify();
            }
        }

        [CreateProperty]
        public string HexCords
        {
            get => _campaignMapManager.MapItemSelected.ItemName;

            set
            {
                _worldName = value;
                Notify();
            }
        }

        public void OnEnable() {
            _uiDocument = GetComponent<UIDocument>();
            _campaignMapManager = Manager.Instance;

            if (!_uiDocument
                || !_campaignMapManager)
                return;

            _campaignMapManager.MapItemSelect.AddListener(OnMapItemSelect);

            _worldNameTextField = _uiDocument.rootVisualElement.Q("WorldName") as TextField;
            _hexCordsTextField = _uiDocument.rootVisualElement.Q("HexCords") as TextField;

            _worldNameTextField.SetBinding
                (
                    "value",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(WorldName))
                    }
                );

            _hexCordsTextField.SetBinding
                (
                    "value",
                    new DataBinding
                    {
                        dataSourcePath = new PropertyPath(nameof(HexCords))
                    }
                );
        }

        public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

        public void OnMapItemSelect(MapItem mapItem) {
            WorldName = mapItem.ItemName;
            HexCords = mapItem.HexCords.ToString();
        }

        private void Notify([CallerMemberName] string property = "") {
            propertyChanged.Invoke(this, new BindablePropertyChangedEventArgs(property));
            Debug.Log("Run");
        }

    }
}