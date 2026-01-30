using UnityEngine;

public class Brojac : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Brojac hit: " + other.name);
        if (other.CompareTag("Igrac"))
        {
            Debug.Log("Score +1!");
            Skok player = other.GetComponent<Skok>();
            if (player) player.PreprekaProsla();

            // UNIŠTI SAMO BROJAC COLLIDER!
            Destroy(this);
        }
    }
}
