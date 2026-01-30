using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Prepreka : MonoBehaviour
{
    public float maxVrijeme = 1;
    public float tajmer = 0;
    public GameObject preprekaPrefab;
    public float visina;

    void Start()
    {
        GameObject novaprepreka = Instantiate(preprekaPrefab);
        novaprepreka.transform.position = transform.position + new Vector3(0, Random.Range(-visina, visina), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if ( tajmer > maxVrijeme )
        {
            GameObject novaprepreka = Instantiate(preprekaPrefab);
            novaprepreka.transform.position = transform.position + new Vector3(0, Random.Range(-visina, visina), 0);
            Destroy(novaprepreka, 15);
        }
        tajmer += Time.deltaTime;
    }
}
