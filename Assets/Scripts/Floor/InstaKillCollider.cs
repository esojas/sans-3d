using UnityEngine;

public class InstaKillCollider : MonoBehaviour
{
    private PlayerHealth playerHealthScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;

        playerHealthScript = other.gameObject.GetComponent<PlayerHealth>();

        playerHealthScript.InstaDeath();
    }
}
