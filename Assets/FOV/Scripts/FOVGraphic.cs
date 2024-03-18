using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVGraphic : MonoBehaviour
{

	private FOVMechanic fov;
	private Mesh mesh;
	private Vector3[] vertices;
	private List<Vector3> viewVertex;
	private RaycastHit2D hit;
	private float meshRes = 2;
	private int[] triangles;
	private int stepCount;
	private Enemy enemy;

	//Getting a refrence to the FOVMechanic script and the mesh
	private void Awake()
	{
		fov = GetComponentInParent<FOVMechanic>();
		mesh = GetComponent<MeshFilter>().mesh;
		enemy = GetComponentInParent<Enemy>();
	}

	//Updating the fov mesh every late frame dynamically
	private void LateUpdate()
	{
		UpdateMesh();
	}

	//Shooting rays and creating a mesh based on the rays
	private void UpdateMesh()
	{
		ShootRays();
		CreateMesh();
	}

	//Shooting rays to check for walls and players
	private void ShootRays()
	{
		stepCount = Mathf.RoundToInt(fov.viewAngle * meshRes);
		float stepAngle = fov.viewAngle / stepCount;

		viewVertex = new List<Vector3>();
		hit = new RaycastHit2D();

		GameObject spottedPlayer = null;
		for (int i = 0; i < stepCount; i++)
		{
			float angle = fov.transform.eulerAngles.y - fov.viewAngle / 2 + stepAngle * i;
			Vector3 dir = fov.DirFromAngle(angle) * Mathf.Sign(transform.parent.transform.localScale.x);
			hit = Physics2D.Raycast(fov.transform.position, dir, fov.viewRadius, fov.obstacleMask | fov.playerMask);

			if (hit.collider != null)
			{
				// bool playerHit = (fov.playerMask.value & 1 << hit.transform.gameObject.layer) > 0;
				Player player = hit.collider.gameObject.GetComponent<Player>();
				bool playerCanBeHit = player == null ? false : player.floorNumber == enemy.floorNumber;

				bool playerHit = (fov.playerMask.value & 1 << hit.transform.gameObject.layer) > 0 && playerCanBeHit;
				viewVertex.Add(transform.position + dir.normalized * ((playerHit && fov.playerBlockView || !playerHit) ? hit.distance : fov.viewRadius));

				if (playerHit)
					spottedPlayer = hit.transform.gameObject;
			}
			else
			{
				viewVertex.Add(transform.position + dir.normalized * fov.viewRadius);
			}
		}

		if (spottedPlayer != fov._spottedPlayer)
			fov.UpdatePlayer(spottedPlayer);
	}

	//Creating a fov mesh
	private void CreateMesh()
	{
		int vertexCount = viewVertex.Count + 1;
		vertices = new Vector3[vertexCount];
		triangles = new int[(vertexCount - 2) * 3];
		vertices[0] = Vector3.zero;

		for (int i = 0; i < vertexCount - 1; i++)
		{
			vertices[i + 1] = transform.InverseTransformPoint(viewVertex[i]);
			if (i < vertexCount - 2)
			{
				triangles[i * 3 + 2] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3] = i + 2;
			}
		}
		mesh.Clear();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
	}

}
