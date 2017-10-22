using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Stone : MonoBehaviour {
    public string objectName;
    public int amountStones;
    private int tempAmountStones;
    public float perc;
    public bool destroyObject;
    private float speed;
    public float startScale;
    public bool changeColor;
    private bool respawn;
    public float respawnTime;

    Mesh mesh;
    Vector3[] vertices;
    Vector3[] normals;
    public GameObject PS_Impact;
    Renderer rend;
    NavMeshObstacle obstacle;
    deformMesh deformScript;
    MeshCollider col;

    [SerializeField]
    private float minValueScale;
    [SerializeField]
    private float maxValueScale;
    [SerializeField]
    private float stoneAmountMultiplier;

    public GameObject destroyedVersion;

    Color orgColor;

    void Start () {
        respawn = false;

        obstacle = GetComponent<NavMeshObstacle>();
        deformScript = GetComponent<deformMesh>();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = deformScript.mesh.vertices;
        rend = GetComponent<Renderer>();
        col = GetComponent<MeshCollider>();

        orgColor = rend.material.GetColor("_Color");

        RandomizeStone();
    }
    void Update()
    {
        if(amountStones < tempAmountStones)
        {
            transform.localScale -= new Vector3(Mathf.Clamp(perc, 0, 0.9f), Mathf.Clamp(perc, 0, 0.9f), Mathf.Clamp(perc, 0, 0.9f));
        }
        tempAmountStones = amountStones;

        if (destroyObject && !respawn)
        {
            col.enabled = false;
            /*if (destroyedVersion != null)
            {
                Instantiate(destroyedVersion, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            }*/
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] -= vertices[i].normalized / 50;
            }
            mesh.vertices = vertices;
            speed += Time.deltaTime;
            if (speed > 1f)
            {
                //GameObject impact = Instantiate(PS_Impact, transform.position, Quaternion.identity);
                //Destroy(this.gameObject);
                rend.enabled = false;
                obstacle.enabled = false;
                respawn = true;

                
                speed = 0;
            }
            /*rend.enabled = false;
            obstacle.enabled = false;
            respawn = true;*/

            if (changeColor)
            {
                rend.material.SetColor("_Color", Color.Lerp(orgColor, new Color(0, 0, 0, 0), speed));
            }
        }

        if (respawn)
        {
            speed += Time.deltaTime;
            if (speed > respawnTime * 1f)
            {
                RandomizeStone();
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            RandomizeStone();
        }
    }

    void RandomizeStone()
    {
        float randVal = Random.Range(minValueScale, maxValueScale);
        
        // Randomize scale/amount stones/rotation
        transform.localScale = new Vector3(randVal, randVal, randVal);
        amountStones = Mathf.FloorToInt(transform.localScale.x * stoneAmountMultiplier);
        startScale = transform.localScale.x;
        perc = (startScale / 2) / amountStones;
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        
        // Reset Variables
        rend.enabled = true;
        obstacle.enabled = true;
        speed = 0;
        col.enabled = true;
        respawn = false;
        destroyObject = false;
        // Reset Color
        rend.material.SetColor("_Color", orgColor);

        // Deform and get vertices
        deformScript.deform(deformScript.vertices);
        vertices = deformScript.mesh.vertices;
    }
}
