using UnityEngine;

public class SpawnFloorPrefab : MonoBehaviour
{
    [SerializeField] private GameObject spawnFloor;
    [SerializeField] private Transform spawnPos;
    private int playerCrossedMax = 1;
    private int playerCrossedCount;
    private static GameObject floorPrefabTemplate;
    private static int floorSpawnCount = 0;

    private void Awake()
    {
        if (floorPrefabTemplate == null && spawnFloor != null)
            floorPrefabTemplate = spawnFloor; // captured once, before any remap happens on this instance
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        if (playerCrossedCount >= playerCrossedMax) return;
        playerCrossedCount++;

        if (spawnFloor != null)
        {
            GameObject spawnPrefab = Instantiate(spawnFloor, spawnPos.position, Quaternion.identity);
            floorSpawnCount++;
            spawnPrefab.name = $"Ground_{floorSpawnCount}";
        }
    }

}
