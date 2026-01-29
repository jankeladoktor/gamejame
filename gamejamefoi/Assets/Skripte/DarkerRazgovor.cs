using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRagovor", menuName = "DarkecRazgovor")]
public class DarkerRazgovor : ScriptableObject
{
    public string nprName;
    public Sprite nprPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    
}
