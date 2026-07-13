using UnityEditor;

namespace Rev.Helpers.Editor
{
	public static class EditorMenus
	{

		[MenuItem("Tools/Helpers/Toggle Inspector Lock %l")]
		private static void ToggleInspectorLock() {
			ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
			ActiveEditorTracker.sharedTracker.ForceRebuild();
		}

	}
}