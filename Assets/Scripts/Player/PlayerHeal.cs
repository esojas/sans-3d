using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    private PlayerHealth playerHealthScript;

    private void HealPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //playerHealthScript.HealPlayer(41);
        }
    }

    private void Awake()
    {
        playerHealthScript = GetComponent<PlayerHealth>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealPlayer();
    }
}
