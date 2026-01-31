using UnityEngine;

public class DeathWall : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // teleport na spawn + reset brzine
        var rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        other.transform.position = spawnPoint.position;
    }
}