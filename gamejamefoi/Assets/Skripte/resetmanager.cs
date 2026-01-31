using UnityEngine;

public class MainMenuReset : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteKey("nivo");
        PlayerPrefs.DeleteKey("HPmax");
        PlayerPrefs.DeleteKey("napad");

        Debug.Log("Svetlan statsi resetovani!");
    }
}