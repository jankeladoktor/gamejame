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
    private void Awake()
    {
        trenutniHP = HPmax;
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
