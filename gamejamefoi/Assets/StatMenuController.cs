using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneTransition.Instance.LoadSceneWithFade("Cutscena");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
