using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider hpSlider;
    public GameOverManager gameOverManager;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }

        if (gameOverManager == null)
        {
            Debug.LogWarning("GameOverManager ยังไม่ได้กำหนดใน Inspector");
        }
    }

    void Update()
    {
        if (hpSlider == null || isDead) return;

        // ลดพลังชีวิตต่อเนื่อง
        currentHealth -= Time.deltaTime * 6f;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        hpSlider.value = currentHealth;

        // เช็คว่าตายหรือยัง
        if (!isDead && Mathf.Approximately(currentHealth, 0f))
        {
            TriggerGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("DamageObject"))
        {
            TakeDamage(2f);
        }

        if (other.CompareTag("HealthItem"))
        {
            Heal(10f);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }

        if (!isDead && Mathf.Approximately(currentHealth, 0f))
        {
            TriggerGameOver();
        }
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }
    }

    void TriggerGameOver()
    {
        if (isDead)
        {
            Debug.LogWarning("GameOver ถูกเรียกซ้ำ");
            return;
        }
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGM();
        }

        isDead = true;
        Debug.Log("Player is Dead!");

        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }

        gameObject.SetActive(false);

}
}