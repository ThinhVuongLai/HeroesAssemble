using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPoissonSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private float objectRadius = 1f;
    [SerializeField] private int spawnCount = 30;
    [SerializeField] private float maxOverlapPercentage = 0.1f; // Tỉ lệ giao nhau cho phép (0-1)

    [Header("Area Settings")]
    [SerializeField] private Vector2 areaSize = new Vector2(30f, 30f);
    [SerializeField] private Transform spawnAreaCenter;

    [Header("Poisson Settings")]
    [SerializeField] private float minDistanceBetweenObjects = 2.5f; // >= objectRadius*2 để không giao nhau
    [SerializeField] private int samplingAttempts = 30;

    private List<Vector3> spawnedPositions = new List<Vector3>();

    public int SpawnObjectsOnNavMeshWithPoisson()
    {
        Vector3 centerPos = spawnAreaCenter != null ? spawnAreaCenter.position : transform.position;
        int returnSpawnCount = SpawnObjectsOnNavMeshWithPoisson(spawnCount ,areaSize, centerPos);
        return returnSpawnCount;
    }

    public int SpawnObjectsOnNavMeshWithPoisson(int spawnNumber, Vector2 areaSize, Vector3 centerPos, Transform parent = null)
    {
        // Tính toán khoảng cách tối thiểu giữa các vị trí dựa trên bán kính và tỉ lệ giao nhau
        float minDistanceFactor = 2f * (1f - maxOverlapPercentage);
        float actualMinDist = Mathf.Max(minDistanceBetweenObjects, objectRadius * minDistanceFactor);

        // Tạo danh sách các điểm theo thuật toán Poisson Disc Sampling
        List<Vector2> points = PoissonDiscSampling(actualMinDist, areaSize, samplingAttempts);

        points.Shuffle();

        int spawnedCount = 0;

        // Thử spawn tại các vị trí đã tạo
        foreach (Vector2 point in points)
        {
            if (spawnedCount >= spawnCount)
                break;

            // Chuyển từ điểm 2D sang vị trí trong không gian 3D
            Vector3 samplePosition = new Vector3(
                centerPos.x - areaSize.x / 2 + point.x,
                centerPos.y,
                centerPos.z - areaSize.y / 2 + point.y
            );

            // Tìm điểm gần nhất trên NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(samplePosition, out hit, 3f, NavMesh.AllAreas))
            {
                // Spawn đối tượng tại vị trí hợp lệ
                GameObject obj = Instantiate(objectPrefab, hit.position, Quaternion.identity, parent);
                // Lưu lại vị trí đã spawn
                spawnedPositions.Add(hit.position);
                spawnedCount++;

                // Thêm visualizer cho vùng bán kính (chỉ dùng trong debug)
#if UNITY_EDITOR
                DrawRadiusVisualizer(hit.position, objectRadius);
#endif
                if(spawnedCount >= spawnNumber)
                {
                    break;
                }
            }
        }

        Debug.Log($"Đã spawn {spawnedCount} đối tượng trên NavMesh.");

        return spawnedCount;
    }

    // Thuật toán Poisson Disc Sampling
    private List<Vector2> PoissonDiscSampling(float minDist, Vector2 sampleRegionSize, int numSamplesBeforeRejection)
    {
        float cellSize = minDist / Mathf.Sqrt(2);

        int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize),
                               Mathf.CeilToInt(sampleRegionSize.y / cellSize)];

        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();

        // Thêm điểm đầu tiên
        Vector2 firstPoint = new Vector2(Random.Range(0, sampleRegionSize.x),
                                        Random.Range(0, sampleRegionSize.y));
        spawnPoints.Add(firstPoint);
        points.Add(firstPoint);
        grid[(int)(firstPoint.x / cellSize), (int)(firstPoint.y / cellSize)] = points.Count;

        // Tiếp tục thêm các điểm mới
        while (spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnCentre = spawnPoints[spawnIndex];
            bool candidateAccepted = false;

            for (int i = 0; i < numSamplesBeforeRejection; i++)
            {
                // Tạo điểm ngẫu nhiên cách điểm hiện tại từ minDist đến 2*minDist
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                float dist = minDist + Random.value * minDist;
                Vector2 candidate = spawnCentre + dir * dist;

                // Kiểm tra xem điểm có nằm trong vùng và cách xa các điểm khác không
                if (IsValid(candidate, sampleRegionSize, cellSize, minDist, points, grid))
                {
                    // Thêm điểm hợp lệ
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
                    candidateAccepted = true;
                    break;
                }
            }

            // Loại bỏ điểm không thể tạo thêm điểm mới
            if (!candidateAccepted)
                spawnPoints.RemoveAt(spawnIndex);
        }

        return points;
    }

    private bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float minDist, List<Vector2> points, int[,] grid)
    {
        // Kiểm tra xem điểm có nằm trong vùng spawn không
        if (candidate.x < 0 || candidate.x >= sampleRegionSize.x || candidate.y < 0 || candidate.y >= sampleRegionSize.y)
            return false;

        // Tính các ô lưới lân cận để kiểm tra
        int cellX = (int)(candidate.x / cellSize);
        int cellY = (int)(candidate.y / cellSize);
        int searchStartX = Mathf.Max(0, cellX - 2);
        int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
        int searchStartY = Mathf.Max(0, cellY - 2);
        int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

        // Kiểm tra khoảng cách với các điểm đã có
        for (int x = searchStartX; x <= searchEndX; x++)
        {
            for (int y = searchStartY; y <= searchEndY; y++)
            {
                int pointIndex = grid[x, y] - 1;
                if (pointIndex != -1)
                {
                    float sqrDist = (candidate - points[pointIndex]).sqrMagnitude;
                    if (sqrDist < minDist * minDist)
                        return false;
                }
            }
        }

        return true;
    }

#if UNITY_EDITOR
    // Vẽ visualizer cho vùng bán kính (chỉ dùng trong editor)
    private void DrawRadiusVisualizer(Vector3 position, float radius)
    {
        GameObject visualizer = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        visualizer.transform.position = position;
        visualizer.transform.localScale = new Vector3(radius * 2, 0.05f, radius * 2);

        // Đặt làm con của đối tượng này
        visualizer.transform.parent = transform;

        // Đổi material sang trong suốt
        Renderer renderer = visualizer.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = new Material(Shader.Find("Standard"));
            material.color = new Color(1f, 0.5f, 0.5f, 0.3f);
            renderer.material = material;
        }

        // Tắt collider
        Collider collider = visualizer.GetComponent<Collider>();
        if (collider != null)
        {
            Destroy(collider);
        }
    }
#endif
}