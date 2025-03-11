using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport otherTeleport;
    private Collider2D col;       

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = otherTeleport.transform.position;

            StartCoroutine(otherTeleport.DisableColliderTemporarily());
        }
    }

    private IEnumerator DisableColliderTemporarily()
    {
        col.enabled = false;
        yield return new WaitForSeconds(1.5f);
        col.enabled = true;
    }
}
