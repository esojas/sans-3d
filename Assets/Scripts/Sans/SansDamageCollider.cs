using UnityEngine;

public class SansDamageCollider : MonoBehaviour
{
    [SerializeField] private float karmaAttack = 6f;
    [SerializeField] private float normalKarmaDrain = 1f;

    private PlayerKarma playerKarma;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        playerKarma = other.gameObject.GetComponent<PlayerKarma>();
        playerKarma.ApplyKarmaDrainRate(karmaAttack);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        playerKarma = other.gameObject.GetComponent<PlayerKarma>();
        playerKarma.ApplyKarmaDrainRate(normalKarmaDrain);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        playerKarma = other.gameObject.GetComponent<PlayerKarma>();
        playerKarma.isKarmaRefill = true;
    }
}
