using UnityEngine;
using UnityEngine.SceneManagement;

public class TriviaWinUI : MonoBehaviour
{
    [Header("Win Panel (Canvas)")]
    public GameObject winPanel; // Povuci PobjedaCanvas ovdje

    void Start()
    {
        // Win ekran mora biti ugašen na poèetku
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    // Poziva se kad trivia završi
    public void ShowWin()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        // Pauziraj igru dok je win ekran otvoren
        Time.timeScale = 0f;
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
}
