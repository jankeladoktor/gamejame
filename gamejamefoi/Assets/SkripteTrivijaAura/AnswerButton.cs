using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    private Button btn;

    private QuizManager manager;
    private int myIndex;

    void Awake()
    {
        btn = GetComponent<Button>();
        if (label == null) label = GetComponentInChildren<TMP_Text>();
    }

    public void Setup(QuizManager quizManager, int index, string text)
    {
        manager = quizManager;
        myIndex = index;

        if (label != null) label.text = text;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => manager.OnAnswerSelected(myIndex));
    }

    public void SetColor(Color color)
    {
        if (btn != null && btn.image != null)
            btn.image.color = color;
    }

    public void SetInteractable(bool value)
    {
        if (btn != null) btn.interactable = value;
    }
}
