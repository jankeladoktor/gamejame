using UnityEngine;

public class SceneTransitionBootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        if (SceneTransition.Instance != null) return;

        GameObject prefab = Resources.Load<GameObject>("SceneTransitionPrefab");
        if (prefab != null)
        {
            Object.Instantiate(prefab);
        }
        else
        {
            Debug.LogError("SceneTransitionPrefab not found in Resources folder!");
        }
    }
}
