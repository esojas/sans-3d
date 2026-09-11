using UnityEngine;

public class SansDodge : MonoBehaviour
{
    public int sansDodgeAmt;

    [SerializeField] private float dodgeSpeed;

    public void DodgePlayer() 
    {
        Vector3.Lerp(transform.position, transform.forward, dodgeSpeed*Time.deltaTime);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DodgePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
