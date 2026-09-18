using UnityEngine;

public class GroundScrollManager : MonoBehaviour
{
    public static GroundScrollManager Instance;

    [SerializeField] private float limitPlayerMovementZ;

    public Vector3 FrameDelta { get; private set; }
    private float accumulatedZ;
    private float baselineZ; 
    private bool wasLimiting;
    private void Awake() => Instance = this;

    private void FixedUpdate()
    {
        Vector3 delta = PlayerMovement.Instance.newHorizontal * Time.fixedDeltaTime;

        delta.z = ClampMovement(DetectPlayerAttack.LimitBackMovement, delta.z);

        accumulatedZ += delta.z;
        FrameDelta = delta;
    }

    private float ClampMovement(bool isLimiting, float deltaZ)
    {
        if (isLimiting && !wasLimiting)
        {
            baselineZ = accumulatedZ; // new zone entered — reset the retreat reference point
        }

        if (isLimiting)
        {
            float traveledSinceLimit = (accumulatedZ + deltaZ) - baselineZ;
            if (traveledSinceLimit < -limitPlayerMovementZ)
            {
                deltaZ = (baselineZ - limitPlayerMovementZ) - accumulatedZ;
            }
        }

        wasLimiting = isLimiting;
        return deltaZ;
    }

}
