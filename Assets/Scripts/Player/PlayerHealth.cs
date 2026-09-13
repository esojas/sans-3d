using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Color karmaColor;
    [SerializeField] private Slider healthBar;
    [SerializeField] private int maxHealth;
    private PlayerKarma playerKarmaScript;
    public float health { get; private set; }

    private void SetTextHealth()
    {
        if (playerKarmaScript.isDraining)
        {
            healthText.color = karmaColor;
            healthText.text = $"{Mathf.RoundToInt(playerKarmaScript.karmaStackValue)}/{maxHealth}";
        }
        else
        {
            healthText.color = Color.white;
            healthText.text = $"{Mathf.RoundToInt(playerKarmaScript.karmaStackValue)}/{maxHealth}";
        }
    }

    private void SetMaxHealth()
    {
        healthBar.maxValue = maxHealth;
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage * (10 * Time.deltaTime);

        health = Mathf.Clamp(health, 1f, maxHealth);
    }

    public void HealPlayer(float heal)
    {
        float healthBefore = health;

        health += heal;

        health = Mathf.Clamp(health, 0f, maxHealth);

        if (playerKarmaScript.isDraining)
        {
            float karmaAmt = playerKarmaScript.karmaStackValue - healthBefore;
            playerKarmaScript.karmaStackValue += karmaAmt + heal;
            health -= karmaAmt;
        }
        else if (playerKarmaScript.karmaStackValue < healthBefore)
        {
            playerKarmaScript.karmaStackValue = health;
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.value = health;
    }

    private void Awake()
    {
        playerKarmaScript = GetComponent<PlayerKarma>();
        SetMaxHealth();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        SetTextHealth();
    }
}
