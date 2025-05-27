using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HpController hp = other.GetComponent<HpController>();
            if (hp != null)
            {
                hp.TakeDamage(hp.maxHealth); // ฆ่าทันที
            }
        }
    }
}
