#if FALSE
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Editor.CampaignMap
{
    [CustomEditor(typeof(global::CampaignMap.World))]
    public class WorldDrawer : UnityEditor.Editor
    {

        public Vector3Int Cell;
        public Tile Tile;
        public bool IsSelected;

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.LabelField("Runtime Values");

            Cell = EditorGUILayout.Vector3IntField("Cell", Cell);

            Tile = EditorGUILayout.ObjectField
                (
                    "Tile",
                    Tile,
                    typeof(Tile),
                    false
                ) as Tile;

            IsSelected = EditorGUILayout.Toggle("Is Selected?", IsSelected);

            serializedObject.ApplyModifiedProperties();
        }

    }
}
#endif