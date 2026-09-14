using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackSpawnPos;
    [SerializeField] private GameObject attackColliderPrefab;
    private PlayerControls playerControlScript;

    private void OnEnable()
    {
        playerControlScript.OnAttackPressed += PerformAttack;
        playerControlScript.OnAttackReleased += ReleaseAttack;
    }

    private void PerformAttack()
    {
        GameObject attackCollider = Instantiate(attackColliderPrefab, attackSpawnPos.position, Quaternion.identity);
    }

    private void ReleaseAttack()
    {

    }

    private void Awake()
    {
        playerControlScript = GetComponent<PlayerControls>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
