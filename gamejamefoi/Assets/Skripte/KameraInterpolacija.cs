using UnityEngine;

public class KameraPratiIgraca : MonoBehaviour
{
    [SerializeField] private Transform igrac;
    [SerializeField] private float brzinaPracenja = 5f;
    [SerializeField] private Vector3 pomak = new Vector3(0f, 0f, -10f);

    private void LateUpdate()
    {
        if (igrac == null) return;

        Vector3 zeljenaPozicija = igrac.position + pomak;
        transform.position = Vector3.Lerp(transform.position, zeljenaPozicija, brzinaPracenja * Time.deltaTime);
    }
}

