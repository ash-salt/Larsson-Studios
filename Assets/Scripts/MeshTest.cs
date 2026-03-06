using UnityEngine;

public class MeshTest : MonoBehaviour
{
    void Start()
    {
        GameObject testObj = new GameObject("TestMesh");
        testObj.transform.position = Vector3.zero;

        MeshFilter mf = testObj.AddComponent<MeshFilter>();
        MeshRenderer mr = testObj.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Unlit/Color"));
        mr.sortingLayerName = "Default";
        mr.sortingOrder = 10; // on top of everything

        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            new Vector3(-1, -1, 0),
            new Vector3(-1,  1, 0),
            new Vector3( 1,  1, 0),
            new Vector3( 1, -1, 0),
        };
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        mesh.RecalculateNormals();
        mf.mesh = mesh;
    }
}