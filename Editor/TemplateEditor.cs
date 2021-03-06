using UnityEngine;
using UnityEditor;

namespace Forge.Editor {

	[CustomEditor(typeof(Template))]
	public class TemplateEditor : UnityEditor.Editor {

		private bool _DisplayJson = false;

		void OnEnable() {
			if (GraphEditor.Selection != null) {
				GraphEditor.Selection.SelectionChanged += OnSelectionChangedHandler;
			}
		}

		void OnDisable() {
			if (GraphEditor.Selection != null) {
				GraphEditor.Selection.SelectionChanged -= OnSelectionChangedHandler;
			}
		}

		void OnSelectionChangedHandler(GraphSelection graphSelection) {
			Repaint();
		}

		public override void OnInspectorGUI() {
			Template template = (Template) serializedObject.targetObject;

			EditorGUILayout.LabelField(template.ToString());
			EditorGUILayout.LabelField("Operators", template.Operators.Count.ToString());
			EditorGUILayout.LabelField("Connections", template.Connections.Count.ToString());
			
			if (GUILayout.Button("Load Demo")) {
				template.Connections.Clear();
				template.Operators.Clear();
				template.LoadDemo();
			}

			if (GUILayout.Button("Serialize")) {
				template.Serialize();
			}

			if (GUILayout.Button("Deserialize")) {
				template.Deserialize();
			}

			EditorGUILayout.Space();

			string jsonTitle = System.String.Format("JSON ({0})", template.JSON.Length);
			_DisplayJson = EditorGUILayout.Foldout(_DisplayJson, jsonTitle);
			if (_DisplayJson) {
				EditorGUILayout.TextArea(template.JSON);
			}

			EditorGUILayout.Space();
		}

	}

}