using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void MoveGround()
    {
        rb.MovePosition(rb.position - PlayerMovement.Instance.newHorizontal * Time.fixedDeltaTime);
    }
    private void FixedUpdate()
    {
        MoveGround();
    }

}
