using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public Tilemap tilemap;
    public AnimatedTile animatedTile; // Unity의 AnimatedTile
    public TileBase idleTile; // 애니메이션이 꺼진 상태의 기본 타일

    // 애니메이션의 속도와 프레임 수를 수동으로 설정
    public float animationDuration = 30.0f; // 애니메이션 전체 지속 시간 (초)

    private void Start()
    {
        // Tilemap에서 기본 타일로 설정
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

            // 근처의 타일을 검사하여 애니메이션을 적용
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int checkPos = new Vector3Int(tilePos.x + x, tilePos.y + y, tilePos.z);
                    if (tilemap.HasTile(checkPos))
                    {
                        // 타일을 애니메이션 타일로 변경
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

            // Player가 타일을 떠날 때 기본 타일로 변경
            if (tilemap.HasTile(tilePos)) { }
                //tilemap.SetTile(tilePos, idleTile);
        }
    }*/

    // 애니메이션을 한 번만 실행하는 코루틴
    private IEnumerator PlayTileAnimation(Vector3Int tilePos)
    {
        // 애니메이션 타일로 설정
        tilemap.SetTile(tilePos, animatedTile);

        // 애니메이션이 끝날 때까지 대기 (애니메이션 지속 시간)
        yield return new WaitForSeconds(animationDuration);

        // 애니메이션이 끝나면 기본 타일로 변경
        tilemap.SetTile(tilePos, idleTile);
    }
}
