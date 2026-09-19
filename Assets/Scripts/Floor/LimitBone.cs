using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LimitBone : MonoBehaviour
{
    [SerializeField] private bool isFrontLimitBone;
    [SerializeField] private float yUpOffset;
    [SerializeField] private float lerpSpeed = 2f;
    [SerializeField] private DetectPlayerAttack detectPlayerAttack;

    private Vector3 restPosition;
    private Vector3 raisedPosition;
    private bool isFrontLimit = false;
    private Transform myBlock;

    private void MoveBoneBlockYPos(Vector3 target)
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, lerpSpeed * Time.deltaTime);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        restPosition = transform.localPosition;
        raisedPosition = restPosition + new Vector3(0f, yUpOffset, 0f);
        myBlock = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        bool isSansOnMyBlock = SansAttack.Transform != null && SansAttack.Transform.parent == myBlock;
        bool frontActive = SansAttack.sansAttack && isSansOnMyBlock;

        isFrontLimit = isFrontLimitBone ? frontActive : detectPlayerAttack.limitBackMovement;

        Vector3 target = isFrontLimit ? raisedPosition : restPosition;

        MoveBoneBlockYPos(target);
    }
}
