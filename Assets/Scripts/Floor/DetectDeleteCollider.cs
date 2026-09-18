using UnityEngine;

public class DetectDeleteCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject,1f);
    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
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
