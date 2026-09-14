using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    [SerializeField] private float healAmount = 40f;
    private PlayerHealth playerHealthScript;




    private void HealPlayer()
    {//test
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Heal Key is pressed");
            playerHealthScript.HealPlayer(healAmount);

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
