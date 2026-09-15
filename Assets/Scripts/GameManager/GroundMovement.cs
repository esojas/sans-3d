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
        this.transform.position -= PlayerMovement.Instance.newHorizontal * Time.fixedDeltaTime;
    }
    private void FixedUpdate()
    {
        MoveGround();
    }

}
