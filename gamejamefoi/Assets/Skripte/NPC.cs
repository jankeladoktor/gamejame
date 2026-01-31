using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour, IInteractable
{
    public DarkerRazgovor dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image Ikona;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool canInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        // ako panel nije dodijeljen, ne možemo otvoriti dijalog
        if (dialoguePanel == null) return;

        // ako je igra pauzirana iz nekog drugog razloga, ne otvaraj novi dijalog
        if (PauseController.IsGamePaused && !isDialogueActive) return;

        if (isDialogueActive) NextLine();
        else StartDialogue();
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.nprName);
        Ikona.sprite = dialogueData.nprPortrait;

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
    }

    void NextLine() {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length) { 
            StartCoroutine(TypeLine());
        }else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine() {
        isTyping = true;
        dialogueText.SetText("");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex]) 
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        PauseController.SetPause(false);
        SceneTransition.Instance.LoadSceneWithFade("Borba");

    }
}
