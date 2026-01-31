using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class UpraviteljIgre : MonoBehaviour
{
    public static UpraviteljIgre Instanca;

    [Header("UI")]
    [SerializeField] private GameObject panelUpute;
    [SerializeField] private TMP_Text tekstNapretka;

    [Header("Player")]
    [SerializeField] private MonoBehaviour skriptaKretanjaIgraca;

    [Header("Audio")]
    [SerializeField] private AudioSource glazbaPozadine;

    [Header("Scene")]
    [SerializeField] private string imeSljedeceScene = "SampleScene";
    [SerializeField] private float kasnjenjeUcitanja = 2.0f;
    [SerializeField] private float trajanjeFadeOuta = 2.0f;

    private int skupljeno = 0;
    private int ukupno = 3;
    private bool igraZavrsena = false;

    private void Awake()
    {
        Instanca = this;

        if (glazbaPozadine == null)
        {
            glazbaPozadine = GetComponent<AudioSource>();
        }
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

                if (glazbaPozadine != null && !glazbaPozadine.isPlaying)
                {
                    glazbaPozadine.Play();
                }
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
            ZavrsIgruIUcitajScenu();
        }
    }

    private void OsvjeziUI()
    {
        if (tekstNapretka != null)
        {
            tekstNapretka.text = "Skupljeno: " + skupljeno + "/" + ukupno;
        }
    }

    private void ZavrsIgruIUcitajScenu()
    {
        igraZavrsena = true;

        if (skriptaKretanjaIgraca != null) skriptaKretanjaIgraca.enabled = false;

        var rb = skriptaKretanjaIgraca.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        if (tekstNapretka != null)
        {
            tekstNapretka.text = "Sve skupljeno! Učitavam sljedeću scenu...";
        }

        StartCoroutine(FadeOutIUcitajScenu());

        Svetlan svetlan = FindObjectOfType<Svetlan>();

        if (svetlan != null)
        {

            svetlan.LevelUp();

            PlayerPrefs.SetInt("nivo", svetlan.nivo);
            PlayerPrefs.SetInt("HPmax", svetlan.HPmax);
            PlayerPrefs.SetInt("napad", svetlan.napad);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator FadeOutIUcitajScenu()
    {
        float pocetnaGlasnoca = 1f;
        if (glazbaPozadine != null) pocetnaGlasnoca = glazbaPozadine.volume;

        float trajanje = Mathf.Max(0.01f, trajanjeFadeOuta); // da ne bude 0
        float cekanje = Mathf.Max(0f, kasnjenjeUcitanja);

        float t = 0f;
        while (t < cekanje)
        {
            t += Time.deltaTime;

            // Fade počinje odmah i završava nakon trajanjeFadeOuta (može biti <= cekanje)
            if (glazbaPozadine != null)
            {
                float omjer = Mathf.Clamp01(t / trajanje);
                glazbaPozadine.volume = Mathf.Lerp(pocetnaGlasnoca, 0f, omjer);
            }

            yield return null;
        }

        if (glazbaPozadine != null)
        {
            glazbaPozadine.volume = 0f;
            glazbaPozadine.Stop();
            glazbaPozadine.volume = pocetnaGlasnoca; // vrati za idući put
        }

        SceneTransition.Instance.LoadSceneWithFade(imeSljedeceScene);
    }
}





