using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject controlsPanel;
    ChangeUIColor[] tousLesUIsColorés;
    public Image rouesDev, rouesPeintre;

    [Space(10)]

    public Material matItemRouge, matItemBleu, matItemOrange;

    private void Awake()
    {
        controlsPanel.SetActive(false);

        PaletteDuJoueur.SelectionnerPalette(PaletteDuJoueur.paletteDuJoueur);
        
        rouesPeintre.enabled = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre;
        rouesDev.enabled = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Développeur;

        tousLesUIsColorés = Resources.FindObjectsOfTypeAll(typeof(ChangeUIColor)) as ChangeUIColor[];

        TogglePalette(PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? 0 : 1);
    }

    public void Play()
    {
        AudioManager.instance.Play("ButtonSelected");
        SceneFader.instance.FadeToScene(1);
    }

    public void ToggleInstructions()
    {
        AudioManager.instance.Play("ButtonSelected");
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }

    public void TogglePalette(int i)
    {
        if(AudioManager.instance)
            AudioManager.instance.Play("ButtonSelected");

        //if (i == 0 && PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ||
        //   i != 0 && PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Développeur)
        //    return;

        rouesPeintre.enabled = i == 0;
        rouesDev.enabled = i != 0;

        PaletteDuJoueur.SelectionnerPalette((i == 0) ? PaletteDuJoueur.TypeDePalette.Peintre : PaletteDuJoueur.TypeDePalette.Développeur);
        for (int j = 0; j < tousLesUIsColorés.Length; j++)
        {
            tousLesUIsColorés[j].ChangerCouleurDeCetUI();
        }

        ChangerCouleursDesMaterials();

        
    }

    private void ChangerCouleursDesMaterials()
    {
        matItemRouge.SetColor("_BaseColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? new Color(1f, 0f, .5f, 1f) : Color.red);
        matItemRouge.SetColor("_EmissionColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? new Color(1f, 0f, .5f, 1f) : Color.red);

        matItemBleu.SetColor("_BaseColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Color.cyan : Color.blue);
        matItemBleu.SetColor("_EmissionColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Color.cyan : Color.blue);

        matItemOrange.SetColor("_BaseColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? new Color(.9f, .4f, 0f, 1f) : Color.cyan);
        matItemOrange.SetColor("_EmissionColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? new Color(.9f, .4f, 0f, 1f) : Color.cyan);
    }

    public void Quit()
    {
        AudioManager.instance.Play("ButtonSelected");
        SceneFader.instance.FadeToQuitScene();
    }
}
