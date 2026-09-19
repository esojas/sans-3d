using UnityEngine;

public class GroundScrollManager : MonoBehaviour
{
    public static GroundScrollManager Instance;

    [SerializeField] private float limitBackMovementZ;
    [SerializeField] private float limitFrontMovementZ;

    public bool limitFrontMovement = true;

    public Vector3 FrameDelta { get; private set; }
    private float accumulatedZ;
    private float baselineZ; 
    private bool wasLimiting;

    private readonly MovementClamp backClamp = new MovementClamp(direction: -1f);
    private readonly MovementClamp frontClamp = new MovementClamp(direction: 1f);

    private void Awake() => Instance = this;

    private void FixedUpdate()
    {
        Vector3 delta = PlayerMovement.Instance.newHorizontal * Time.fixedDeltaTime;

        delta.z = backClamp.Apply(DetectPlayerAttack.LimitBackMovement, limitBackMovementZ, accumulatedZ, delta.z);
        delta.z = frontClamp.Apply(SansAttack.sansAttack, limitFrontMovementZ, accumulatedZ, delta.z);

        accumulatedZ += delta.z;
        FrameDelta = delta;
    }

    private class MovementClamp
    {
        private readonly float direction;
        private float baselineZ;
        private bool wasLimiting;

        public MovementClamp(float direction) => this.direction = direction;

        public float Apply(bool isLimiting, float limitDistance, float accumulatedZ, float deltaZ)
        {
            if (isLimiting && !wasLimiting)
            {
                baselineZ = accumulatedZ; // new zone entered — reset reference point
            }

            if (isLimiting)
            {
                float signedTravel = direction * (accumulatedZ + deltaZ - baselineZ);
                if (signedTravel > limitDistance)
                {
                    deltaZ = baselineZ + limitDistance * direction - accumulatedZ;
                }
            }

            wasLimiting = isLimiting;
            return deltaZ;
        }
    }

}
