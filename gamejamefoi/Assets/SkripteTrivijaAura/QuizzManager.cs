using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        [TextArea] public string questionText;
        public string[] answers;     
        public int correctIndex;    
        [TextArea] public string hostCorrectComment; 
    }

    [Header("UI")]
    public TMP_Text questionTMP;        
    public TMP_Text commentatorTMP;      
    public AnswerButton[] answerButtons;
    public TriviaWinUI winUI;
    [Header("Question Images (UI Images)")]
    public Image[] questionImages;       

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color wrongColor = Color.red;
    public Color correctColor = Color.green;

    [Header("Attempts")]
    public int maxAttempts = 2;
    private int attemptsLeft;

    [Header("Flow")]
    public bool autoNextAfterFinish = true;
    public float nextDelaySeconds = 1.7f;

    [Header("Wrong answer commentator lines")]
    public string[] wrongLines =
    {
        "Je kolega… pa kak to ne znate? Ajde no, bomo vam dali još jen pokušaj!",
        "Ajoj… neje ti to to. Ajde kad ste tak lepi, probajte opet!",
        "Ma daj kume … pak ti je lagano! Imaš još jedan pokušaj!"
    };

    private int currentQuestionIndex = 0;
    private bool finishedThisQuestion = false;

    private Question[] questions = new Question[]
    {
        new Question {
            questionText = "Koji je hrvatski grad poznat po Dioklecijanovoj palači?",
            answers = new [] {"Zadar","Split","Pula","Šibenik"},
            correctIndex = 1,
            hostCorrectComment = "Tako je, Split! Dioklecijanova palača jedna je od najvećih i najbolje očuvanih rimskih palača na svijetu, a danas čini samo srce grada."
        },
        new Question {
            questionText = "Koja se tradicionalna čipka izrađuje na otoku Pagu i nalazi se na UNESCO listi?",
            answers = new [] {"Lepoglavska čipka","Paška čipka","Dubrovačka čipka","Istarska mrežica"},
            correctIndex = 1,
            hostCorrectComment = "Točno! Paška čipka je simbol strpljenja i umijeća, a njezina izrada prenosi se generacijama."
        },
        new Question {
            questionText = "Koji običaj iz okolice Kastva, s maskiranim ophodima, nalazi se na UNESCO-voj listi?",
            answers = new [] {"Sinjska alka","Dubrovnik fest","Zvončari","Picokijada"},
            correctIndex = 2,
            hostCorrectComment = "Bravo! Kastavski zvončari poznati su po svojim zvonima i maskama kojima simbolično tjeraju zimu i zle duhove."
        },
        new Question {
            questionText = "Kako se zove poznata glagoljska ploča, jedan od najvažnijih spomenika hrvatske pismenosti?",
            answers = new [] {"Bašćanska ploča","Vinodolski zakonik","Splitski natpis","Krčki zapis"},
            correctIndex = 0,
            hostCorrectComment = "Da! Bašćanska ploča potječe iz oko 1100. godine i jedan je od prvih pisanih tragova hrvatskog jezika."
        },
        new Question {
            questionText = "Koji je nacionalni park poznat po slapovima i jezerima, te je među najposjećenijima u Hrvatskoj?",
            answers = new [] {"Paklenica","Risnjak","Plitvička jezera","Mljet"},
            correctIndex = 2,
            hostCorrectComment = "Tako je! Plitvička jezera su najstariji hrvatski nacionalni park i također pod zaštitom UNESCO-a."
        },
        new Question {
            questionText = "Kako se zove tradicionalni viteški turnir koji se svake godine održava u Sinju?",
            answers = new [] {"Trka na prstenac","Sinjska alka","Moreška","Đakovački vezovi"},
            correctIndex = 1,
            hostCorrectComment = "Točno! Sinjska alka održava se od 1715. godine i slavi pobjedu nad Osmanlijama."
        },
        new Question {
            questionText = "Prema legendi, što su Đurđevčani ispalili iz topa kako bi prevarili Turke?",
            answers = new [] {"Zlatnike","Pijetla (picoka)","Kameni blok","Zastavu"},
            correctIndex = 1,
            hostCorrectComment = "Bravo! Legenda kaže da su ispalili pijetla iz topa kako bi Turci pomislili da grad ima dovoljno hrane – i tako su odustali od opsade."
        }
    };

    void Start()
    {
        ShowQuestion(0);
    }

    void ShowQuestion(int index)
    {
        CancelInvoke(nameof(NextQuestion));
        finishedThisQuestion = false;

        currentQuestionIndex = Mathf.Clamp(index, 0, questions.Length - 1);
        var q = questions[currentQuestionIndex];

        attemptsLeft = maxAttempts;

        if (questionTMP != null) questionTMP.text = q.questionText;
        if (commentatorTMP != null) commentatorTMP.text = "";

    
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].Setup(this, i, q.answers[i]);
            answerButtons[i].SetColor(normalColor);
            answerButtons[i].SetInteractable(true);
        }

      
        UpdateQuestionImages(currentQuestionIndex);
    }

    void UpdateQuestionImages(int activeIndex)
    {
        if (questionImages == null || questionImages.Length == 0) return;

        for (int i = 0; i < questionImages.Length; i++)
        {
            if (questionImages[i] != null)
                questionImages[i].gameObject.SetActive(i == activeIndex);
        }
    }

    public void OnAnswerSelected(int answerIndex)
    {
        if (finishedThisQuestion) return;

        var q = questions[currentQuestionIndex];
        bool correct = (answerIndex == q.correctIndex);

        if (correct)
        {
            finishedThisQuestion = true;

            answerButtons[answerIndex].SetColor(correctColor);
            SetAllButtonsInteractable(false);

            if (commentatorTMP != null)
                commentatorTMP.text = q.hostCorrectComment;


            if (autoNextAfterFinish) Invoke(nameof(NextQuestion), nextDelaySeconds);
            return;
        }

       
        attemptsLeft--;

     
        answerButtons[answerIndex].SetColor(wrongColor);
        answerButtons[answerIndex].SetInteractable(false);

        if (attemptsLeft > 0)
        {
          
            if (commentatorTMP != null)
                commentatorTMP.text = GetRandomWrongLine();
        }
        else
        {
          
            finishedThisQuestion = true;

            answerButtons[q.correctIndex].SetColor(correctColor);
            SetAllButtonsInteractable(false);

            if (commentatorTMP != null)
                commentatorTMP.text = "Eee, kolega… sad si gotov ❌\n\nTočan odgovor je zelen.\n\n" + q.hostCorrectComment;

            if (autoNextAfterFinish) Invoke(nameof(NextQuestion), nextDelaySeconds);
        }
    }

    string GetRandomWrongLine()
    {
        if (wrongLines == null || wrongLines.Length == 0)
            return "Krivo! Imaš još jedan pokušaj.";

        int i = Random.Range(0, wrongLines.Length);
        return wrongLines[i];
    }

    public void NextQuestion()
    {
        int next = currentQuestionIndex + 1;

     
        if (next >= questions.Length)
        {
            if (winUI != null)
                winUI.ShowWin();

            return;
        }

   
        ShowQuestion(next);
    }


    void SetAllButtonsInteractable(bool value)
    {
        for (int i = 0; i < answerButtons.Length; i++)
            answerButtons[i].SetInteractable(value);
    }
}
