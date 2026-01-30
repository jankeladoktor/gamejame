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
        nivo = PlayerPrefs.GetInt("nivo", nivo);
        HPmax = PlayerPrefs.GetInt("HPmax", HPmax);
        napad = PlayerPrefs.GetInt("napad", napad);

        trenutniHP = HPmax;

        if (lvl1 != null) lvl1.SetActive(false);
        if (lvl2 != null) lvl2.SetActive(false);
        if (lvl3 != null) lvl3.SetActive(false);
        if (lvl4 != null) lvl4.SetActive(false);
        if (lvl5 != null) lvl5.SetActive(false);

        OdabirSvetlana(nivo);
    }


    private void OdabirSvetlana(int nivo)
    {
        if (lvl1 != null) lvl1.SetActive(false);
        if (lvl2 != null) lvl2.SetActive(false);
        if (lvl3 != null) lvl3.SetActive(false);
        if (lvl4 != null) lvl4.SetActive(false);
        if (lvl5 != null) lvl5.SetActive(false);

        switch (nivo)
        {
            case 1:
                if (lvl1 != null) lvl1.SetActive(true);
                break;
            case 2:
                if (lvl2 != null) lvl2.SetActive(true);
                break;
            case 3:
                if (lvl3 != null) lvl3.SetActive(true);
                break;
            case 4:
                if (lvl4 != null) lvl4.SetActive(true);
                break;
            case 5:
                if (lvl5 != null) lvl5.SetActive(true);
                break;
            default:
                if (lvl1 != null) lvl1.SetActive(true);
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

        OdabirSvetlana(nivo);

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
