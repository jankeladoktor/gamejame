using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkecBoss : MonoBehaviour
{
   public string imeDarkeca = "Darkec vladar Tame";
    public int nivo = 5;
    public int HPmax = 200;
    public int trenutniHP;
    public int napad = 35;
    private void Awake()
    {
        trenutniHP = HPmax;
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
