using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private int maxHealth;
    public float health { get; private set;}

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

    private void UpdateHealthBar()
    {
        healthBar.value = health;
    }

    private void Awake()
    {
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
    }
}
