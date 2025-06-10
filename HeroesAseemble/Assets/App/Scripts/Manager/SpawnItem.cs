using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] private Vector2 areaSpawn = Vector2.one;

    public Vector2 AreaSpawn
    {
        get
        {
            return areaSpawn;
        }
    }

#if UNITY_EDITOR
    private void DrawnAreaSpawn()
    {
        Gizmos.color = Color.red;

        Vector3 center = transform.position;
        Vector3 size = new Vector3(areaSpawn.x, areaSpawn.y, 0f);

        // Tính các góc của hình chữ nhật (nằm trên mặt phẳng XY)
        Vector3 topLeft = center + new Vector3(-size.x / 2, 0f, size.y / 2);
        Vector3 topRight = center + new Vector3(size.x / 2, 0f, size.y / 2);
        Vector3 bottomRight = center + new Vector3(size.x / 2, 0f, -size.y / 2);
        Vector3 bottomLeft = center + new Vector3(-size.x / 2, 0f, -size.y / 2);

        // Vẽ các cạnh
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }

    private void OnDrawGizmos()
    {
        DrawnAreaSpawn();
    }
#endif
}
