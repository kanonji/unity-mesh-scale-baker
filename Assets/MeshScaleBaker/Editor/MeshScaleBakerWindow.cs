using UnityEngine;
using UnityEditor;

namespace Kanonji.MeshScaleBaker {
	public class MeshScaleBakerWindow : EditorWindow {
		public GameObject source;
		public string fileName;

		[MenuItem("Tools/MeshScaleBaker")]
		static void Open() {
			GetWindow<MeshScaleBakerWindow>();
		}

		void OnGUI() {
			EditorGUI.BeginChangeCheck();
			this.source = (GameObject)EditorGUILayout.ObjectField("GameObject having mesh.", this.source, typeof(GameObject), true);
			if (EditorGUI.EndChangeCheck()) {
				this.fileName = this.source.name;
			}
			this.fileName = EditorGUILayout.TextField("Filename for new mesh.", this.fileName);
			if (GUILayout.Button("Bake")) {
				if (this.source == null) {
					ShowNotification(new GUIContent("No object selected"));
					return;
				}
				var meshFilters = this.source.GetComponentsInChildren<MeshFilter>();
				var meshCombiner = new MeshCombiner(meshFilters);
				var combinedMesh = meshCombiner.Combine();
				AssetDatabase.CreateAsset(combinedMesh, "Assets/" + this.fileName + ".asset");
			}
		}
	}
}
