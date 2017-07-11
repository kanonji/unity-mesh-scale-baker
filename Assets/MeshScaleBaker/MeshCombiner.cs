using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kanonji.MeshScaleBaker {
	public class MeshCombiner {

		private MeshFilter[] sources;
		protected MeshFilter[] Sources {
			get { return sources; }
			set { sources = value; }
		}

		[SerializeField] private CombineInstance[] combineInstances;
		public CombineInstance[] CombineInstances{
			get { return combineInstances; }
			set { combineInstances = value; }
		}

		public MeshCombiner(MeshFilter[] meshFilters) {
			this.Sources = meshFilters;
			this.CombineInstances = new CombineInstance[this.Sources.Length];
			this.Init();
		}

		public Mesh Combine() {
			Mesh mesh = new Mesh();
			mesh.CombineMeshes(this.CombineInstances);
			return mesh;
		}

		protected void Init() {
			for (int i = 0; i < this.Sources.Length; ++i) {
				MeshFilter meshFilter = this.Sources[i];
				this.CombineInstances[i].transform = meshFilter.transform.localToWorldMatrix;
				this.CombineInstances[i].mesh = meshFilter.sharedMesh;
			}
		}
	}
}
