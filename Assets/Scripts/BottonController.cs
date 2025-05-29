using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottonController : MonoBehaviour
{

    private PlayerCotroller player;

    void Start()
    {
        // ค้นหา PlayerController ในฉากและกำหนดค่าให้ player
        player = FindObjectOfType<PlayerCotroller>();
    }

    // เรียกใช้ฟังก์ชัน Jump() ใน PlayerController
    public void Jump()
    {
        if (player != null)
        {
            player.Jump();
        }
    }

    // เรียกใช้ฟังก์ชัน PerformSlide() ใน PlayerController
    public void Slide()
    {
        if (player != null)
        {
            player.PerformSlide();
        }
    }
}
