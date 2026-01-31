using UnityEngine;
using UnityEngine.Playables;

public class CutsceneEndLoader : MonoBehaviour
{
    public string nextSceneName = "SampleScene";
    private PlayableDirector director;
    private bool done = false;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    void OnEnable()
    {
        director.stopped += OnTimelineFinished;
    }

    void OnDisable()
    {
        director.stopped -= OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector d)
    {
        if (done) return;
        done = true;
        SceneTransition.Instance.LoadSceneWithFade(nextSceneName);
    }
}
