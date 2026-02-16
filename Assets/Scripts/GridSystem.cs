using System.Numerics;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject objectToMove;
    public float gridSize = 1f;
    private GameObject ghostObject;
    private HashSet<UnityEngine.Vector3> occupiedPositions = new HashSet<UnityEngine.Vector3>();
    
    public Tilemap tilemap;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UnityEngine.Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);
            if (tilemap.HasTile(cellPosition))
            {
                Debug.Log("Clicked tile at: " + cellPosition);
            }
        }
    }
}
