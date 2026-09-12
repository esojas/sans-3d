using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    private PlayerHealth playerHealthScript;
    private PlayerKarma playerKarmaScript;

    //private void TakeDamage()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        playerHealthScript.TakeDamage(1);
    //        playerKarmaScript.isDraining = true;
    //        Debug.Log("TakeDamage");
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        playerHealthScript.TakeDamage(1);
        if (playerKarmaScript == null)
        {
            Debug.LogWarning("Player karma script is null");
            return;
        }

        playerKarmaScript.isDraining = true;
        Debug.Log("TakeDamage");
    }

    private void Awake()
    {
        playerHealthScript = GetComponent<PlayerHealth>();
        playerKarmaScript = GetComponent<PlayerKarma>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
