using UnityEngine;

public class DetectPlayerAttack : MonoBehaviour
{
    [SerializeField] private SansAttack sansAttackScript;
    bool playerCrossed;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        playerCrossed = true;
        sansAttackScript.SansStartAttack();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        playerCrossed = false;
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = playerCrossed ? Color.green : Color.red;

        Gizmos.DrawWireCube(transform.position,transform.localScale);
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
