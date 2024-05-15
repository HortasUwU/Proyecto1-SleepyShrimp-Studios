using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Nota", menuName ="Nota")]
public class Nota : ScriptableObject
{
    public int Id;
    public string data;
    public string fecha;
}
