using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject tilePrefab;

    public int width = 5;
    public int height = 5;
    public float tileSize = 1f;

    private Tile[,] tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
{
    tiles = new Tile[width, height];

    MeshFilter mf = tilePrefab.GetComponentInChildren<MeshFilter>();

    float realTileSize =
        mf.sharedMesh.bounds.size.x;

    float offsetX = (width - 1) * realTileSize * 0.5f;
    float offsetY = (height - 1) * realTileSize * 0.5f;

    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            Vector3 position = new Vector3(
                x * realTileSize - offsetX,
                0f,
                y * realTileSize - offsetY
            );

            GameObject tileObj = Instantiate(tilePrefab, transform);

            tileObj.transform.localPosition = position;
            tileObj.transform.localScale = Vector3.one;

            tiles[x, y] = tileObj.GetComponent<Tile>();
        }
    }
}

}