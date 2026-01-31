using UnityEngine;
using TMPro;

public class UpraviteljIgre : MonoBehaviour
{
    public static UpraviteljIgre Instanca;

    [Header("UI")]
    [SerializeField] private GameObject panelUpute;
    [SerializeField] private TMP_Text tekstNapretka; // TMP umjesto Legacy Text

    [Header("Player")]
    [SerializeField] private MonoBehaviour skriptaKretanjaIgraca;

    private int skupljeno = 0;
    private int ukupno = 3;
    private bool igraZavrsena = false;

    private void Awake()
    {
        Instanca = this;
    }

    private void Start()
    {
        if (panelUpute != null) panelUpute.SetActive(true);
        if (skriptaKretanjaIgraca != null) skriptaKretanjaIgraca.enabled = false;

        OsvjeziUI();
    }

    private void Update()
    {
        if (panelUpute != null && panelUpute.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                panelUpute.SetActive(false);
                if (skriptaKretanjaIgraca != null) skriptaKretanjaIgraca.enabled = true;
            }
        }
    }

    public void DodajSkupljeniPredmet()
    {
        if (igraZavrsena) return;

        skupljeno++;
        OsvjeziUI();

        if (skupljeno >= ukupno)
        {
            ZavrsIgru();
        }
    }

    private void OsvjeziUI()
    {
        if (tekstNapretka != null)
        {
            tekstNapretka.text = "Skupljeno: " + skupljeno + "/" + ukupno;
        }
    }

    private void ZavrsIgru()
    {
        igraZavrsena = true;

        if (skriptaKretanjaIgraca != null) skriptaKretanjaIgraca.enabled = false;

        var rb = skriptaKretanjaIgraca.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        if (tekstNapretka != null)
        {
            tekstNapretka.text = "Pobjeda! Skupio si majicu, hlače i cipele.";
        }
    }
}



