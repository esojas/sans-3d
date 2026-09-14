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
            healthText.text = $"{Mathf.CeilToInt(playerKarmaScript.karmaStackValue)}/{maxHealth}";
        }
        else
        {
            healthText.color = Color.white;
            healthText.text = $"{Mathf.CeilToInt(playerKarmaScript.karmaStackValue)}/{maxHealth}";
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

        if (playerKarmaScript.isDraining)
        {
            float karmaDrainHeal = playerKarmaScript.karmaStackValue - health;
            playerKarmaScript.karmaStackValue += heal;
            playerKarmaScript.karmaStackValue = Mathf.Clamp(playerKarmaScript.karmaStackValue, 0f, maxHealth);
            health = playerKarmaScript.karmaStackValue - karmaDrainHeal;
        }
        else
        {
            Debug.Log("Healed");
            health += heal;
            playerKarmaScript.karmaStackValue = health;
        }

        health = Mathf.Clamp(health, 0f, maxHealth);
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
