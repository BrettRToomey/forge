using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace Forge.Editor {

	public class IconLoader {

		public Dictionary<string, Texture2D> Icons;

		public IconLoader() {
			Icons = new Dictionary<string, Texture2D>();

			string[] icons = new string[] {
				"defaultGizmo",
				"vertex", "vertexIndex", "vertexNormal", "vertexTangent", "vertexPosition",
				"face", "faceIndex", "faceNormal", "facePosition",
				"polygon", "polygonIndex",
				"origin", "uv"
			};

			for (int i = 0; i < icons.Length; i++) {
				string filePath = System.String.Format("Forge/Editor/Icons/{0}.png", icons[i]);
				string vertexIconPath = Path.Combine(Application.dataPath, filePath);
				Texture2D texture = new Texture2D(1, 1);
				texture.hideFlags = HideFlags.HideAndDontSave;
				texture.LoadImage(File.ReadAllBytes(vertexIconPath));
				texture.Apply();
				Icons.Add(icons[i], texture);
			}

		}

	}

}