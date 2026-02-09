using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    List<Collider> objectsOnTile = new List<Collider>();
 
    public Vector2Int gridPosition;

    void OnTriggerEnter (Collider ent) {
        objectsOnTile.Add(ent);
    }
}
