using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Skok : MonoBehaviour
{
    [Header("Movement")]
    public float velocity = 12f;
    private Rigidbody2D rb;

    [Header("Win")]
    public GameObject krunaPrefab;
    public Transform krunaSpawnPoint;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip krunaSound;

    [Header("Scene Transition")]
    public string winNextScene = "SampleScene";
    public float extraDelayAfterSound = 1f;

    private bool isWinMode = false;
    public int score = 0;
    private bool hasWon = false;

    private bool winTriggered = false;
    private bool restarting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * velocity;
        }
    }

    public void PreprekaProsla()
    {
        score++;
        Debug.Log("Score: " + score);

        if (score >= 10 && !hasWon)
        {
            hasWon = true;
            EnterWinMode();
        }
    }

    void EnterWinMode()
    {
        isWinMode = true;

        Prepreka spawner = FindObjectOfType<Prepreka>();
        if (spawner) spawner.StopSpawning();

        if (krunaPrefab && krunaSpawnPoint)
            Instantiate(krunaPrefab, krunaSpawnPoint.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // KRUNA -> zvuk do kraja + 1s -> promjena scene
        if (!winTriggered && other.CompareTag("Kruna") && isWinMode)
        {
            winTriggered = true;
            Destroy(other.gameObject);

            if (audioSource != null && krunaSound != null)
                audioSource.PlayOneShot(krunaSound);

            StartCoroutine(WinSequence());
        }
    }

    IEnumerator WinSequence()
    {
        // čekaj da zvuk završi
        if (krunaSound != null)
            yield return new WaitForSeconds(krunaSound.length);

        // dodatni delay 1s
        yield return new WaitForSeconds(extraDelayAfterSound);
        Svetlan svetlan = FindObjectOfType<Svetlan>();

        if (svetlan != null)
        {

            svetlan.LevelUp();

            PlayerPrefs.SetInt("nivo", svetlan.nivo);
            PlayerPrefs.SetInt("HPmax", svetlan.HPmax);
            PlayerPrefs.SetInt("napad", svetlan.napad);
            PlayerPrefs.Save();
        }

        SceneTransition.Instance.LoadSceneWithFade(winNextScene);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // PREPREKA/PIPELINE -> kompletan restart scene
        if (restarting || winTriggered) return;

        if (collision.gameObject.name.Contains("Prepreka") || collision.gameObject.name.Contains("Pipe"))
        {
            restarting = true;
            SceneTransition.Instance.LoadSceneWithFade(SceneManager.GetActiveScene().name);
        }
    }
}
