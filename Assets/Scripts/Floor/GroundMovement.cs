using DG.Tweening;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private DetectPlayerAttack detectPlayerAttack;

    [SerializeField] private float limitPlayerMovementX; //Collision

    //[SerializeField] private float limitPlayerMovementZ; //Collision

    private void Start()
    {
        detectPlayerAttack = GetComponentInChildren<DetectPlayerAttack>();
    }

    private void Awake()
    {

    }

    private void MoveGround()
    {
        transform.position -= GroundScrollManager.Instance.FrameDelta;

        float clampX = Mathf.Clamp(transform.position.x, -limitPlayerMovementX, limitPlayerMovementX);
        transform.position = new Vector3(clampX, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        MoveGround();
    }

}
