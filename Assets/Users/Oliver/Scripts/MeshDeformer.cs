using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour {

    public float springForce = 20.0f;
    public float damping = 5f;
    public float uniformScale = 1.0f;
	Mesh deformingMesh;
	Vector3[] originalVertices, displacedVertices;
	Vector3[] vertexVelocities;
    MeshCollider meshCollider;

	void Start () {
		deformingMesh = GetComponent<MeshFilter>().mesh;
		originalVertices = deformingMesh.vertices;
		displacedVertices = new Vector3[originalVertices.Length];
		for (int i = 0; i < originalVertices.Length; i++) {
			displacedVertices[i] = originalVertices[i];
		}
		vertexVelocities = new Vector3[originalVertices.Length];
        meshCollider = GetComponent<MeshCollider>();
	}

    void UpdateMesh()
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            UpdateVertex(i);
        }

        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();
        meshCollider.sharedMesh = deformingMesh;
    }

	void Update () {
        uniformScale = transform.localScale.x;

        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateMesh();
        }
    }

    public void ResetMesh()
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            ResetVertex(i);
        }
    }

    void ResetVertex(int i)
    {
        vertexVelocities[i] = Vector3.zero;
        displacedVertices[i] = originalVertices[i];
    }

	void UpdateVertex (int i) {
		Vector3 velocity = vertexVelocities[i];
        Vector3 displacement = displacedVertices[i] - originalVertices[i];
        displacement *= uniformScale;
        velocity -= displacement * springForce * Time.deltaTime;
        velocity *= 1f - damping * Time.deltaTime;
        vertexVelocities[i] = velocity;
		displacedVertices[i] += velocity / (uniformScale);
	}

	public void AddDeformingForce (Vector3 point, float force) {
        point = transform.InverseTransformPoint(point);
		for (int i = 0; i < displacedVertices.Length; i++) {
			AddForceToVertex(i, point, force);
		}

        UpdateMesh();
    }

	void AddForceToVertex (int i, Vector3 point, float force) {
		Vector3 pointToVertex = displacedVertices[i] - point;
        pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities[i] += pointToVertex.normalized * velocity;
	}
}