using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prepreka : MonoBehaviour
{
    public float maxVrijeme = 1f;
    private float tajmer = 0f;
    public GameObject preprekaPrefab;
    public float visina = 3f;
    private bool stopped = false;

    void Start()
    {
        SpawnPrepreka();
    }

    void Update()
    {
        if (stopped) return;
        tajmer += Time.deltaTime;
        if (tajmer > maxVrijeme)
        {
            SpawnPrepreka();
            tajmer = 0f;
        }
    }

    public void StopSpawning()
    {
        stopped = true;
    }

    void SpawnPrepreka()
    {
        if (preprekaPrefab == null) return;
        GameObject nova = Instantiate(preprekaPrefab);
        nova.transform.position = transform.position + new Vector3(0, Random.Range(-visina, visina), 0);
        Destroy(nova, 15f);  // 15 SEKUNDI!
    }
}
