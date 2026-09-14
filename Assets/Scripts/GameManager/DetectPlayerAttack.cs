using UnityEngine;

public class DetectPlayerAttack : MonoBehaviour
{
    bool playerCrossed;


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
