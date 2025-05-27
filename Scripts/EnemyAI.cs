using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Path path;
    public float speed = 200f; // ความเร็วในการเคลื่อนที่
    public Transform target;
    public float nextWayPointDistance = 3f; // ระยะห่างสำหรับการไปยัง Waypoint ถัดไป
    private int currentWaypoint = 0;

    private Seeker seeker;
    private Rigidbody2D rb;

    [Header("Attack Settings")]
    public float damage = 10f; // ดาเมจต่อการโจมตี
    public float attackCooldown = 1f; // คูลดาวน์ระหว่างการโจมตี
    private bool canAttack = true;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // เริ่มค้นหาเส้นทางทุก ๆ 0.5 วินาที
        InvokeRepeating("UpdatePath", 1f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (path == null) return;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // คำนวณทิศทางการเคลื่อนที่
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        // ตรวจสอบว่าเดินถึง Waypoint ปัจจุบันหรือยัง
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

        // สลับทิศทางตามการเคลื่อนที่
        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // ฟังก์ชันสำหรับโจมตีเมื่อ Enemy ชนกับ Collider ของ Player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canAttack)
        {
            // ดึง HpController ของ Player แทน PlayerHealth
            HpController playerHealth = other.GetComponent<HpController>();
            if (playerHealth != null)
            {
                StartCoroutine(Attack(playerHealth));
            }
        }
    }

    IEnumerator Attack(HpController playerHealth)
    {
        canAttack = false;

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Enemy Attacking! Damage: " + damage);
        }

        // ดีเลย์ก่อนโจมตีครั้งต่อไป
        yield return new WaitForSeconds(attackCooldown);

        // ทำลาย Enemy หลังจากโจมตีเสร็จ
        Destroy(gameObject);
    }
}
