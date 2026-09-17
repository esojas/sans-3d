using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    private SansDodge sansDodgeScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 8) return;
        other.gameObject.GetComponent<SansDodge>().StartDodge();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 0.1f);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
