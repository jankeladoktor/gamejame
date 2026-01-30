using UnityEngine;
using UnityEngine.SceneManagement;

public class Skok : MonoBehaviour
{
    public GameObject gameOverScreen;  

    public float velocity = 12f;  
    private Rigidbody2D rb;
    private bool isGameOver = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
      
        if (isGameOver) return;
        gameOverScreen.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Prepreka") && !isGameOver)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;  
        gameOverScreen.SetActive(true); 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
