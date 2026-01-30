using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Skok : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverScreen;
    public GameObject gameWinCanvas;  // NOVO! Tvoj Canvas s gumbom

    [Header("Win")]
    public GameObject krunaPrefab;
    public Transform krunaSpawnPoint;
    public ParticleSystem victoryParticles;  // NOVO! Particles

    public float velocity = 12f;
    private Rigidbody2D rb;
    private bool isGameOver = false;
    private bool isWinMode = false;
    public int score = 0;
    private bool hasWon = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (gameOverScreen) gameOverScreen.SetActive(false);
        if (gameWinCanvas) gameWinCanvas.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;
        if (gameOverScreen) gameOverScreen.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * velocity;
        }
    }

    public void PreprekaProsla()
    {
        score++;
        Debug.Log("Score: " + score);

        if (score >= 5 && !hasWon)
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Prepreka") && !isGameOver)
            GameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Kruna") && isWinMode)
        {
            Destroy(other.gameObject);
            StartCoroutine(WinSequence());  
        }
    }

    IEnumerator WinSequence()  
    {
    
        if (gameWinCanvas) gameWinCanvas.SetActive(true);

       
    

        
        yield return new WaitForSeconds(2f);

        
      
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverScreen?.SetActive(true);
    }

    public void Dalje()
    {
        Time.timeScale = 1f;

        Svetlan svetlan = FindObjectOfType<Svetlan>();
        if (svetlan != null)
        {
            svetlan.LevelUp();

            PlayerPrefs.SetInt("nivo", svetlan.nivo);
            PlayerPrefs.SetInt("HPmax", svetlan.HPmax);
            PlayerPrefs.SetInt("napad", svetlan.napad);
            PlayerPrefs.Save();
        }

        SceneTransition.Instance.LoadSceneWithFade("SampleScene");
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneTransition.Instance.LoadSceneWithFade(SceneManager.GetActiveScene().name);
    }
}
