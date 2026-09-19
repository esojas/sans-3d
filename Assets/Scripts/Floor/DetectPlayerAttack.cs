using UnityEngine;

public class DetectPlayerAttack : MonoBehaviour
{
    public static bool LimitBackMovement;
    [SerializeField] private SansAttack sansAttackScript;
    [SerializeField] private int playerCrossedMax = 1;
    private int playerCrossedCount;
    public bool limitBackMovement;

    private void Start()
    {
        sansAttackScript = GameObject.Find("Sans-Test").GetComponent<SansAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        if (this.playerCrossedCount >= playerCrossedMax) return;
        this.playerCrossedCount++;
        LimitBackMovement = false;
        limitBackMovement = false;
        sansAttackScript.SansStartAttack();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        LimitBackMovement = true;
        limitBackMovement = true;
    }

    private void OnDrawGizmos()
    {
        bool playerCrossed = playerCrossedCount < 1 ? false : true;
        Gizmos.color = playerCrossed ? Color.green : Color.red;

        Gizmos.DrawWireCube(transform.position,transform.lossyScale);
    }
}
