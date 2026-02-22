using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject objectToMove;
    public float gridSize = 1f;
    private GameObject ghostObject;
    private HashSet<UnityEngine.Vector3> occupiedPositions = new HashSet<UnityEngine.Vector3>();
}
