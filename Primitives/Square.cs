﻿using UnityEngine;
using Forge.Filters;

namespace Forge.Primitives {

	public class Square {

		public OrientationPreset Orientation = OrientationPreset.XZ;
		public Vector2 Size = Vector2.one;
		public Vector3 Center = Vector3.zero;
		public Surface Surface = Surface.None;

		public Geometry Output() {

			Geometry geo = new Geometry();

			// Vertices and normals
			geo.Vertices = new Vector3 [] {
				new Vector3( Size.x / 2, 0, -Size.y / 2), // bottom right
				new Vector3(-Size.x / 2, 0, -Size.y / 2), // bottom left
				new Vector3(-Size.x / 2, 0,  Size.y / 2), // top left
				new Vector3( Size.x / 2, 0,  Size.y / 2)  // top right
			};

			Vector3 normal = new Vector3(0f, 1f, 0f);
			geo.Normals = new Vector3 [] {normal, normal, normal, normal};

			// UV
			geo.UV = new Vector2 [] {
				new Vector2(1f, 1f),
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 1f)
			};

			// Surface
			switch (Surface) {
				case Surface.None:
					geo.Triangles = new int[0];
					geo.Normals = new Vector3[4];
					break;
				case Surface.Triangulate:
					geo.Triangles = new int [] {
						0, 1, 2,
						2, 3, 0
					};
					break;
				case Surface.Converge:
					var conv = new Converge(geo);
					conv.Point = Center;
					conv.RecalculateNormals = false;
					geo = conv.Output();
					break;
			}
			
			geo.ApplyOrientation(Orientation);
			geo.Offset(Center);

			// Orientation
			return geo;
		}
		
	}

}