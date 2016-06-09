using UnityEngine;
using System.Collections;

public class update_mesh : MonoBehaviour {

	public static Mesh viewedModel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		MeshFilter viewedModelFilter = (MeshFilter)gameObject.GetComponent("MeshFilter");
		viewedModel=viewedModelFilter.sharedMesh;
	}
}
