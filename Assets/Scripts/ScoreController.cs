using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score = 0; // ค่าคะแนนเริ่มต้น
    public TextMeshProUGUI scoreText; // เชื่อมกับ TextMeshPro ใน Unity

    public AudioClip collectSound; // ไฟล์เสียงตอนเก็บไอเทม
    private AudioSource audioSource; // ตัวเล่นเสียง

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // ดึง AudioSource จาก GameObject
        UpdateScoreUI();
    }

    // ฟังก์ชันทำงานเมื่อชนกับ Collider ของไอเทม
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Score")) // ตรวจสอบว่าเป็นไอเทม
        {
            // เพิ่มคะแนนเมื่อเก็บไอเทม
            score += 10;
            UpdateScoreUI();

            // เล่นเสียงเก็บไอเทม
            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            // ทำลายไอเทมเมื่อเก็บแล้ว
            Destroy(other.gameObject);

            // แสดงข้อความใน Console
            Debug.Log("Item Collected! Current Score: " + score);
        }
    }

    // ฟังก์ชันสำหรับอัปเดต TextMeshPro ให้แสดงคะแนน
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
