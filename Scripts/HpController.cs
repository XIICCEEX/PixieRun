using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public float maxHealth = 100f; // พลังชีวิตสูงสุด
    private float currentHealth;    // พลังชีวิตปัจจุบัน
    public Slider hpSlider;        // Slider สำหรับแสดงพลังชีวิต
    public GameOverManager gameOverManager;

    void Start()
    {
        // เริ่มต้นพลังชีวิต
        currentHealth = maxHealth;
        hpSlider.maxValue = maxHealth;
        hpSlider.value = currentHealth;
    }

    void Update()
    {
        // ตรวจสอบว่า hpSlider ถูกกำหนดใน Update
        if (hpSlider == null) return;

        // ลดพลังชีวิตลงในแต่ละวินาที
        currentHealth -= Time.deltaTime * 6f; // ปรับค่าคูณตามที่ต้องการให้ลดเร็วหรือช้าลง
        hpSlider.value = currentHealth;

        // ถ้าพลังชีวิตเหลือ 0 จะทำลายตัวละคร
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            TriggerGameOver();
        }
    }

    // ฟังก์ชั่นสำหรับลดพลังชีวิตเมื่อชนสิ่งของที่มี Trigger
    private void OnTriggerEnter2D(Collider2D other) // ใช้ Collider2D สำหรับ 2D
    {
        if (other.CompareTag("DamageObject")) // ตรวจสอบว่าเป็นสิ่งของที่มี tag "DamageObject"
        {
            TakeDamage(2f); // ลดพลังชีวิต 2 เมื่อชน
        }

        if (other.CompareTag("HealthItem")) // ตรวจสอบว่าเป็นไอเทมที่เพิ่มพลังชีวิต
        {
            Heal(10f); // เพิ่มพลังชีวิตเมื่อเก็บไอเทม
            Destroy(other.gameObject); // ทำลายไอเทมหลังจากเก็บ
        }
    }

    // ฟังก์ชั่นสำหรับลดพลังชีวิต
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            TriggerGameOver();
        }
    }

    // ฟังก์ชั่นสำหรับเพิ่มพลังชีวิต
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // ตรวจสอบไม่ให้เกิน maxHealth
        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }
    }
    
    void TriggerGameOver()
    {
        Debug.Log("Player is Dead!");

        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }

        // ปิดตัวละครแทนการ Destroy เพื่อให้ UI ยังใช้ได้
        gameObject.SetActive(false);
    }

}

