using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_CardKey : MonoBehaviour
{
    public string cardKeyName = "Silver Key";
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Space))
        {
            AcquireCardKey();
            Destroy(gameObject);
        }
    }

    private void AcquireCardKey()
    {
        K_GameManager.Instance.AcquireCardKey(cardKeyName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
