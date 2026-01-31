using UnityEngine;

public class PredmetZaSkupljanje : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (UpraviteljIgre.Instanca != null)
            {
                UpraviteljIgre.Instanca.DodajSkupljeniPredmet();
            }

            Destroy(gameObject);
        }
    }
}

