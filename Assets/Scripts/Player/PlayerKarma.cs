using UnityEngine;
using UnityEngine.UI;

public class PlayerKarma : MonoBehaviour
{
    [SerializeField] private Slider poisonedHealthBar;
    [SerializeField] private int drainRate;
    private PlayerHealth playerHealth;
    public bool isDraining;
    [SerializeField] private float karmaMaxStack = 40;
    private float karmaStack;
    private float karmaStackDrainRate;
    public bool isKarmaRefill;

    private void ApplyKarmaDamage()
    {
        Debug.Log("Applying karma damage with drain rate " + drainRate);
        poisonedHealthBar.value -= drainRate * Time.deltaTime;
        poisonedHealthBar.value = Mathf.Max(poisonedHealthBar.value, 0);
        if (poisonedHealthBar.value <= playerHealth.health) isDraining = false;
    }

    public void ApplyKarmaDrainRate(float karma)
    {
        karmaStack += karma;
        Debug.Log("Applied karma: " + karmaStack);
        karmaStack = Mathf.Clamp(karmaStack, 0f, karmaMaxStack);
        if (karmaStack > 30) drainRate = 30;
        else if (karmaStack > 20f) drainRate = 12;  
        else if (karmaStack > 10f) drainRate = 4;  
        else if (karmaStack > 0f) drainRate = 2;
        else drainRate = 0;
    }

    private void ReduceKarmaStack()
    {
        if (karmaStack > 30)
        {
            karmaStackDrainRate = 30f;
        }
        else if (karmaStack > 20f)
        {
            karmaStackDrainRate = 12f;
        }
        else if (karmaStack > 10f)
        {
            karmaStackDrainRate = 4f;
        }
        else
        {
            karmaStackDrainRate = 2f;
        }

        ApplyKarmaDrainRate(0);
        karmaStack -= karmaStackDrainRate * Time.deltaTime;
        karmaStack = Mathf.Clamp(karmaStack, 0f, karmaMaxStack);
        Debug.LogWarning("Applied karmaStackDrainRate: " + karmaStackDrainRate);
        Debug.LogWarning("Applied karma is being reduce to: " + karmaStack);
        if (karmaStack <= 0f) isKarmaRefill = false;
    }


    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poisonedHealthBar.maxValue = playerHealth.health;
        poisonedHealthBar.value = playerHealth.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDraining)
        {
            ApplyKarmaDamage();
        }
        if (isKarmaRefill)
        {
            ReduceKarmaStack();
        }
    }
}
