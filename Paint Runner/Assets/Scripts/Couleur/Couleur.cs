using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Couleur
{
    public Color c;
    public enum ColorTag { Rouge, Magenta, Jaune, Bleu, Cyan, Vert, Orange, Violet, Noir, Blanc};
    public ColorTag tag;
}
