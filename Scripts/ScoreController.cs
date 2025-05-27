using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int score = 0; // ค่าคะแนนเริ่มต้น
    public TextMeshProUGUI scoreText; // เชื่อมกับ TextMeshPro ใน Unity

    private void Start()
    {
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
            scoreText.text =  score.ToString();
        }
    }
}
