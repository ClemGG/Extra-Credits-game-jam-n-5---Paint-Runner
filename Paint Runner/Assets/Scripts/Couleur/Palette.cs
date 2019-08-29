using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouvelle Palette", menuName = "Palette")]
public class Palette : ScriptableObject
{

    public Combinaison[] couleursPossibles;
}
