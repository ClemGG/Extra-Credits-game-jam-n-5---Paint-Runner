using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{

    [SerializeField] private VaisseauJeu vaisseau;
    [SerializeField] private MeshRenderer croixRenderer;



    // Start is called before the first frame update
    void Start()
    {
        croixRenderer.material.SetColor("_BaseColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? new Color(1f, 0f, .5f, 1f) : Color.red);
        croixRenderer.material.SetColor("_EmissionColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? new Color(1f, 0f, .5f, 1f) : Color.red);
    }

    private void OnMouseDown()
    {
        if (!ScoreManager.instance.gameHasStarted || ScoreManager.instance.isDead)
            return;
        
        vaisseau.RetirerCouleurAuVaisseau();
    }
}
