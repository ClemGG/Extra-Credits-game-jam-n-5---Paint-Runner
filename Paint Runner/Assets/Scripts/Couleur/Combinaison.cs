using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Combinaison
{
    public string tag;
    public Couleur couleur;

    [Space(10)]

    public Couleur[] couleursNécessaires;
}
