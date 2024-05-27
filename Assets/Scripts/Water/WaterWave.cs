using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshCollider))]
public class WaterWave : MonoBehaviour
{
	[System.Serializable]
	struct Wave
	{
		[Range(0, 20)] public float amplitude;
		[Range(0, 40)] public float length;
		[Range(0, 10)] public float roll;
		[Range(0, 10)] public float rate;
		[Range(0, 360)] public float direction;
	}

	[SerializeField] Wave[] waves; // wave struct

	[Header("Mesh Generator")]
	[SerializeField][Range(1, 80)] float xSize = 40;
	[SerializeField][Range(1, 80)] float zSize = 40;
	[SerializeField][Range(2, 80)] int xVertexNum = 40;
	[SerializeField][Range(2, 80)] int zVertexNum = 40;

	MeshFilter meshFilter;
	MeshCollider meshCollider;

	Mesh mesh;
	Vector3[] vertices;
	Vector3[,] buffer;

	void Start()
	{
		meshFilter = GetComponent<MeshFilter>(); // get mesh filter component
        meshCollider = GetComponent<MeshCollider>(); // get mesh collider component
		//RegenerateMesh(); // regenerate mesh

        MeshGenerator.Plane(meshFilter, xSize, zSize, xVertexNum, zVertexNum); // generate mesh

        mesh = meshFilter.mesh; // get mesh from mesh filter
        vertices = mesh.vertices; // update vertices array with mesh vertices

        buffer = new Vector3[xVertexNum, zVertexNum]; // create buffer with vertex values

        //Debug.Log("Mesh initialized with: " + vertices.Length + " vertices");
	}

	Vector3 GerstnerWave(Vector3 position, Vector2 direction, float speed, float length, float amplitude, float roll)
	{
        //Debug.Log("Received amplitude: " + amplitude);
        Vector3 v = Vector3.zero;

		float coord = position.x * direction.x + position.z * direction.y; // directional wave
        float k = 2 * Mathf.PI / length; // wave number
		float f = k * coord * speed; // phase

        v.x = Mathf.Cos(f) * roll; // roll
		//Debug.Log("Gerstner Wave roll: " + v.x.ToString());
        v.y = Mathf.Sin(f) * amplitude; // amplitude
        //Debug.Log("Gerstner Wave amplitude: " + v.y.ToString());

        //Debug.Log("Gerstner Wave displacement: " + v.ToString());

        return v;

    }

	//Vector3 v = Vector3.zero;


    void Update()
	{
        //if (vertices.Length != xVertexNum * zVertexNum)
        //{
        //    RegenerateMesh();
        //}

        // update vertex values with wave
        for (int z = 0; z < zVertexNum; z++)
		{
			//float zPosition = ((float)z / (zVertexNum - 1) - 0.5f) * xSize;
			//float xPosition = ((float)z / (zVertexNum - 1) - 0.5f) * xSize;
            float zPosition = ((float)z / (zVertexNum - 1) - 0.5f) * xSize;
			for (int x = 0; x < xVertexNum; x++)
			{
				Vector3 p = Vector3.zero;
				Vector3 o = Vector3.zero;

				p.x = ((x / (float)(xVertexNum - 1)) - 0.5f) * xSize; // set x position
                p.z = ((z / (float)(zVertexNum - 1)) - 0.5f) * zSize; // set z position
																	  //p.y = Mathf.Sin(p.x * wave.length + Time.time * wave.rate) * wave.amplitude;

				for (int i = 0; i < waves.Length; i++)
				{
					Vector2 d = new Vector2(Mathf.Cos(Mathf.Deg2Rad * waves[i].direction), Mathf.Sin(Mathf.Deg2Rad * waves[i].direction));
					d.Normalize();
					o += GerstnerWave(p, d, Time.time * waves[i].rate, waves[i].length, waves[i].amplitude, waves[i].roll);
				}

                buffer[x, z] = p + o; // update buffer with new vertex values
            }
		}

		// set vertices from buffer
		for (int x = 0; x < xVertexNum; x++)
		{
			for (int z = 0; z < zVertexNum; z++)
			{
				vertices[x + z * xVertexNum] = buffer[x, z];
			}
		}

		//Debug.Log("Vertices updated.");

		// recalculate mesh with new vertice values
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
		mesh.RecalculateBounds();
		meshCollider.sharedMesh = mesh;
	}

	void RegenerateMesh()
	{
		MeshGenerator.Plane(meshFilter, xSize, zSize, xVertexNum, zVertexNum); // regenerate mesh
		mesh = meshFilter.mesh; // get mesh from mesh filter
        vertices = mesh.vertices; // update vertices array with mesh vertices
        buffer = new Vector3[xVertexNum, zVertexNum]; // create buffer with vertex values
    }
}
