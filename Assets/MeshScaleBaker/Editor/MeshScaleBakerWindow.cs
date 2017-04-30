using UnityEngine;
using UnityEditor;

namespace Kanonji.MeshScaleBaker {
	public class MeshScaleBakerWindow : EditorWindow {
		public GameObject source;

		[MenuItem("Tools/MeshScaleBaker")]
		static void Open() {
			GetWindow<MeshScaleBakerWindow>();
		}

		void OnGUI() {
			this.source = (GameObject)EditorGUILayout.ObjectField("GameObject having mesh.", this.source, typeof(GameObject), true);
			if (GUILayout.Button("Bake")) {
				if (this.source == null) {
					ShowNotification(new GUIContent("No object selected"));
					return;
				}
				var meshScaleBaker = new MeshScaleBaker(this.source);
				meshScaleBaker.Bake();
			}
		}
	}
}
