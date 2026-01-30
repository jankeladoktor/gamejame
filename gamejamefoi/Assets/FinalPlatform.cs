using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPlatform : MonoBehaviour
{
    public float delayBeforeLoad = 7f;
    public AudioSource audioSource;
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(PlaySoundAndLoad());
        }
    }

    private IEnumerator PlaySoundAndLoad()
    {
        if (audioSource != null)
            audioSource.Play();

        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene("SampleScene");
    }
}
