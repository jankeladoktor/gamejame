using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPlatform : MonoBehaviour
{

    [Header("Floating")]
    public float amplitude = 0.25f;
    public float frequency = 1f;
    private Vector3 startPos;

    public float delayBeforeLoad = 7f;
    public AudioSource audioSource;
    private bool triggered = false;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);
    }
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
        Svetlan svetlan = FindObjectOfType<Svetlan>();

        if (svetlan != null)
        {
            svetlan.LevelUp();

            PlayerPrefs.SetInt("nivo", svetlan.nivo);
            PlayerPrefs.SetInt("HPmax", svetlan.HPmax);
            PlayerPrefs.SetInt("napad", svetlan.napad);
            PlayerPrefs.Save();
        }

        SceneTransition.Instance.LoadSceneWithFade("SampleScene");
    }
}
