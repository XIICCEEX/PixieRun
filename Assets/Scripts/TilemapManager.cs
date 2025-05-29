using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public Tilemap groundTilemap; // Tilemap สำหรับพื้น
    public Tilemap obstacleTilemap; // Tilemap สำหรับอุปสรรค
    public TileBase[] obstacleTiles; // กำหนดประเภทของอุปสรรค

    public int levelLength = 50; // ความยาวของด่าน
    public int groundHeight = -3; // ตำแหน่งของพื้น

    private int nextXPosition; // ตำแหน่ง X ที่จะสร้างด่านใหม่

    void Start()
    {
        GenerateLevel(); // สร้างด่านเริ่มต้น
    }

    void GenerateLevel()
    {
        // สร้าง Obstacle แบบสุ่ม
        for (int x = nextXPosition; x < nextXPosition + levelLength; x++)
        {
            // ตรวจสอบว่ามี Tile ในพื้นที่ตำแหน่ง (x, groundHeight) หรือไม่
            TileBase groundTile = groundTilemap.GetTile(new Vector3Int(x, groundHeight, 0));
            TileBase obstacleTile = obstacleTilemap.GetTile(new Vector3Int(x, groundHeight + 1, 0)); // ตำแหน่งของอุปสรรค

            // ถ้าไม่มี Tile ในตำแหน่งพื้นและอุปสรรค เราจะสุ่มอุปสรรค
            if (groundTile == null && obstacleTile == null)
            {
                if (Random.Range(0, 10) < 3) // โอกาส 30% ในการสร้าง Obstacle
                {
                    // สุ่มเลือกอุปสรรค
                    int obstacleIndex = Random.Range(0, obstacleTiles.Length);
                    // ตำแหน่ง Y ของ Obstacle
                    int obstacleYPosition = groundHeight + Random.Range(0, 2); // สามารถย้ายได้ระหว่าง groundHeight ถึง groundHeight+1
                    obstacleTilemap.SetTile(new Vector3Int(x, obstacleYPosition, 0), obstacleTiles[obstacleIndex]);
                }
            }
        }

        // อัปเดตตำแหน่งเริ่มต้นของด่านถัดไป
        nextXPosition += levelLength;
    }

    // เมื่อ Player วิ่งไปสุด Tilemap → เรียกสร้างด่านใหม่
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GenerateLevel();
            ClearOldTiles(); // ลบ Tile ที่ผ่านมาแล้ว
        }
    }

    void ClearOldTiles()
    {
        for (int x = nextXPosition - (levelLength * 2); x < nextXPosition - levelLength; x++)
        {
            obstacleTilemap.SetTile(new Vector3Int(x, groundHeight + 1, 0), null);
        }
    }
}
