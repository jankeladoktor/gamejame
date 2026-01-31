using UnityEngine;
using UnityEngine.Playables;

public class CutsceneRestartGame : MonoBehaviour
{
    public string firstGameSceneName = "PocetniScreen"; // scena gdje igra poèinje

    private PlayableDirector director;
    private bool done = false;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    void OnEnable()
    {
        director.stopped += OnCutsceneFinished;
    }

    void OnDisable()
    {
        director.stopped -= OnCutsceneFinished;
    }

    void OnCutsceneFinished(PlayableDirector d)
    {
        if (done) return;
        done = true;

        SceneTransition.Instance.LoadSceneWithFade(firstGameSceneName);
    }
}
