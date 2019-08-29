using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

    public int peintureIndex; //A remplir à la main, permet d'identifier l'ordre des couleurs pour les modifier en fonction de la palette utilisée
    Couleur.ColorTag colorTag; //Sera remplie dans le start
    public Vector3 offset;
    float zCoord;
    bool isDragging;

    public VaisseauJeu vaisseau;
    Transform t;
    Vector3 startPos;
    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        startPos = t.position;
        mainCam = Camera.main;

        offset = startPos - (startPos + vaisseau.GetRailPosition());
        

        switch (peintureIndex)
        {
            case 0:
                colorTag = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Couleur.ColorTag.Magenta : Couleur.ColorTag.Rouge;
                break;

            case 1:
                colorTag = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Couleur.ColorTag.Jaune : Couleur.ColorTag.Vert;
                break;

            case 2:
                colorTag = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Couleur.ColorTag.Cyan : Couleur.ColorTag.Bleu;
                break;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            c.GetComponent<VaisseauJeu>().AjouterCouleurAuVaisseau(colorTag);
            OnMouseUp();
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }


    private void OnMouseDrag()
    {
        if (!isDragging || !ScoreManager.instance.gameHasStarted || ScoreManager.instance.isDead)
            return;

            Vector3 mousePos = Input.mousePosition;
            zCoord = mainCam.WorldToScreenPoint(startPos).z;
            mousePos.z = zCoord;

            t.position = mainCam.ScreenToWorldPoint(mousePos);


    }

    private void OnMouseUp()
    {
        isDragging = false;
        //print(vaisseau.GetRailPosition());
        t.position = startPos + vaisseau.GetRailPosition() + offset;
    }
}
