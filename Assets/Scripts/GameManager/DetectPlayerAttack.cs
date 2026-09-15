using UnityEngine;

public class DetectPlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject backWall; // so player doesnt run back after attacking
    [SerializeField] private SansAttack sansAttackScript;
    [SerializeField] private int playerCrossedMax = 1;
    private int playerCrossedCount;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        if (this.playerCrossedCount >= playerCrossedMax) return;
        this.playerCrossedCount++;
        backWall.SetActive(true);
        sansAttackScript.SansStartAttack();
    }

    private void OnDrawGizmos()
    {
        bool playerCrossed = playerCrossedCount < 1 ? false : true;
        Gizmos.color = playerCrossed ? Color.green : Color.red;

        Gizmos.DrawWireCube(transform.position,transform.lossyScale);
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
