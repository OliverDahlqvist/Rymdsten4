using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deformMesh : MonoBehaviour {
    [HideInInspector]
    public Mesh mesh;
    public Vector3[] vertices;
    [SerializeField]
    private float minSize;
    [SerializeField]
    private float maxSize;

    Vector3[] orgVertices;
    MeshCollider collider;
    private float delayTime;

    // Use this for initialization
    void Awake () {
        
        mesh = GetComponent<MeshFilter>().mesh;

        if (GetComponent<MeshCollider>())
            collider = GetComponent< MeshCollider>();


        vertices = mesh.vertices;
        orgVertices = mesh.vertices;

        deform(vertices);
        //deform(vertices);

        if(gameObject.tag == "Laser")
        {
            delayTime = 0.05f;
        }

    }
    void Update ()
    {
        delayTime -= Time.deltaTime;
        if(delayTime <= 0)
        {
            if (gameObject.tag == "Laser")
            {
                deform(vertices);
            }
            delayTime = 0.05f;
        }
    }
    // Update is called once per frame
    public void deform(Vector3[] vertices)
    {
        Dictionary<Vector3, List<int>> realVertices = new Dictionary<Vector3, List<int>>();
        for (int i = 0; i < vertices.Length; i++)
        {
            if (!realVertices.ContainsKey(vertices[i]))
            {
                realVertices.Add(vertices[i], new List<int>());
            }
            realVertices[vertices[i]].Add(i);
        }
        foreach (var kvp in realVertices)
        {
            Vector3 newPos = orgVertices[kvp.Value[0]] * Random.Range(minSize, maxSize);
            for (int i = 0; i < kvp.Value.Count; i++)
            {
                vertices[kvp.Value[i]] = newPos;
            }
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        if(collider)
            collider.sharedMesh = mesh;
    }
}