using UnityEngine;

public class SansDodge : MonoBehaviour
{
    public int sansDodgeAmt;
    [SerializeField] private Vector3 dodgeDistance;
    [SerializeField] private float dodgeSpeed;

    public void DodgePlayer() 
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dodgeDistance, dodgeSpeed*Time.deltaTime);
        Debug.LogWarning("DODGING!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //DodgePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
