using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Tilemap tilemap;
    public AnimatedTile animatedTile; // Unity�� AnimatedTile
    public TileBase idleTile; // �ִϸ��̼��� ���� ������ �⺻ Ÿ��

    // �ִϸ��̼��� �ӵ��� ������ ���� �������� ����
    public float animationDuration = 30.0f; // �ִϸ��̼� ��ü ���� �ð� (��)

    private void Start()
    {
        // Tilemap���� �⺻ Ÿ�Ϸ� ����
        if (tilemap != null && idleTile != null)
        {
            BoundsInt bounds = tilemap.cellBounds;
            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                if (tilemap.HasTile(pos))
                    tilemap.SetTile(pos, idleTile);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 playerPos = collision.transform.position;
            Vector3Int tilePos = tilemap.WorldToCell(playerPos);

            // ��ó�� Ÿ���� �˻��Ͽ� �ִϸ��̼��� ����
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int checkPos = new Vector3Int(tilePos.x + x, tilePos.y + y, tilePos.z);
                    if (tilemap.HasTile(checkPos))
                    {
                        // Ÿ���� �ִϸ��̼� Ÿ�Ϸ� ����
                        StartCoroutine(PlayTileAnimation(checkPos));
                    }
                }
            }
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 playerPos = collision.transform.position;
            Vector3Int tilePos = tilemap.WorldToCell(playerPos);

            // Player�� Ÿ���� ���� �� �⺻ Ÿ�Ϸ� ����
            if (tilemap.HasTile(tilePos)) { }
                //tilemap.SetTile(tilePos, idleTile);
        }
    }*/

    // �ִϸ��̼��� �� ���� �����ϴ� �ڷ�ƾ
    private IEnumerator PlayTileAnimation(Vector3Int tilePos)
    {
        // �ִϸ��̼� Ÿ�Ϸ� ����
        tilemap.SetTile(tilePos, animatedTile);

        // �ִϸ��̼��� ���� ������ ��� (�ִϸ��̼� ���� �ð�)
        yield return new WaitForSeconds(animationDuration);

        // �ִϸ��̼��� ������ �⺻ Ÿ�Ϸ� ����
        tilemap.SetTile(tilePos, idleTile);
    }
}
