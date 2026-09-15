using UnityEngine;

public class SpawnFloorPrefab : MonoBehaviour
{
    [SerializeField] private GameObject spawnFloor;
    [SerializeField] private Transform spawnPos;
    private int playerCrossedMax = 1;
    private int playerCrossedCount;
    private static GameObject floorPrefabTemplate;


    private void Awake()
    {
        if (floorPrefabTemplate == null)
            floorPrefabTemplate = spawnFloor; // captured once, before any remap happens on this instance
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        if (playerCrossedCount >= playerCrossedMax) return;
        playerCrossedCount++;
        Instantiate(floorPrefabTemplate, spawnPos.position, Quaternion.identity);
    }

}
