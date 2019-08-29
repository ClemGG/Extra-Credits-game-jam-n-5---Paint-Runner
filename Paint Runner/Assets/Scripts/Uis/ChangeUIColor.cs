using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeUIColor : MonoBehaviour
{
    public Color couleurEnModePeintre, couleurEnModeDéveloppeur;
    Color couleurSélectionnée;

    private void Start()
    {
        ChangerCouleurDeCetUI();
    }

    public void ChangerCouleurDeCetUI()
    {

        couleurSélectionnée = (PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre) ? couleurEnModePeintre : couleurEnModeDéveloppeur;
        Image i = GetComponent<Image>();
        TextMeshProUGUI t = GetComponent<TextMeshProUGUI>();

        if (i)
        {
            i.color = couleurSélectionnée;
        }
        else if (t)
        {
            t.color = couleurSélectionnée;
        }
    }
}
