using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kanonji.MeshScaleBaker {
	public class MeshScaleBaker {

		private GameObject source;
		protected GameObject Source {
			get { return source; }
			set { source = value; }
		}

		public MeshScaleBaker(GameObject source) {
			this.Source = source;
		}

		public GameObject Bake() {
			Mesh sharedMesh = this.Source.GetComponent<MeshFilter>().sharedMesh;
			var matrix = this.Source.transform.localToWorldMatrix;
			var vertices = new Vector3[sharedMesh.vertexCount];
			for (int i = 0; i < sharedMesh.vertexCount; i++) {
				vertices[i] = matrix.MultiplyPoint3x4(sharedMesh.vertices[i]);
			}

			var mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.bindposes = sharedMesh.bindposes;
			mesh.boneWeights = sharedMesh.boneWeights;
			mesh.bounds = sharedMesh.bounds;
			mesh.colors = sharedMesh.colors;
			mesh.colors32 = sharedMesh.colors32;
			mesh.hideFlags = sharedMesh.hideFlags;
			mesh.normals = sharedMesh.normals;
			mesh.tangents = sharedMesh.tangents;
			mesh.triangles = sharedMesh.triangles;
			mesh.uv = sharedMesh.uv;
			mesh.uv2 = sharedMesh.uv2;
			mesh.uv3 = sharedMesh.uv3;
			mesh.uv4 = sharedMesh.uv4;

			var go = Object.Instantiate(this.Source.gameObject);
			Object.DestroyImmediate(go.GetComponent<Collider>());
			go.GetComponent<MeshFilter>().mesh = mesh;
			go.transform.localScale = Vector3.one;
			go.AddComponent<MeshCollider>();
			return go;
		}
	}
}
