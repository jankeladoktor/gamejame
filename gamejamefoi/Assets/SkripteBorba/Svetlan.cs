using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Svetlan : MonoBehaviour
{
    public int nivo = 1;
    public int HPmax = 80;
    public int trenutniHP;
    public string imeSvetlana = "Svetlan";
    public int napad = 15;
    public GameObject lvl1;
    public GameObject lvl2; 
    public GameObject lvl3;
    public GameObject lvl4;
    public GameObject lvl5;
    private void Awake()
    {
        trenutniHP = HPmax;
        lvl1.SetActive(false);
        lvl2.SetActive(false);
        lvl3.SetActive(false);
        lvl4.SetActive(false);
        lvl5.SetActive(false);
        OdabirSvetlana(nivo);
    }
    private void OdabirSvetlana(int nivo)
    {
        switch (nivo)
        {
            case 1:
                lvl1.SetActive(true);
                break;
            case 2:
                lvl2.SetActive(true);
                break;
            case 3:
                lvl3.SetActive(true);
                break;
            case 4:
                lvl4.SetActive(true);
                break;
            case 5:
                lvl5.SetActive(true);
                break;
            default:
                lvl1.SetActive(true);
                break;
        }
    }
    public void LevelUp()
    {
        if (nivo >= 5) return;

        nivo++;

        HPmax += 30;
        napad += 20;

        trenutniHP = HPmax;

        Debug.Log("Svetlan leveled up! Level: " + nivo);
    }
    public void PrimiStetu(int steta)
    {
        trenutniHP -= steta;
        if (trenutniHP <= 0)
        {
            trenutniHP = 0;
        }
    }
    public bool JeHmrl()
    {
        return trenutniHP <= 0;
    }
}
