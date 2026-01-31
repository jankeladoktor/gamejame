using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    [SerializeField] private Animator animator;
    [SerializeField] private float fadeDuration = 0.3f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (animator == null)
            animator = GetComponentInChildren<Animator>(true);
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(DoTransition(sceneName));
    }

    private IEnumerator DoTransition(string sceneName)
    {
        if (animator != null) animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(fadeDuration);

        SceneManager.LoadScene(sceneName);

        yield return null; // da se scena “stabilizira”

        if (animator != null) animator.SetTrigger("FadeIn");
    }
}
