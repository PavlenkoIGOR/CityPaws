using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionSpawnPoint
{
    MoveForeward,
    MoveBack
}

public class SpawnPoint : MonoBehaviour
{
    public DirectionSpawnPoint directionSpawnPoint;
    void Start()
    {
        
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 2);
    }
}
