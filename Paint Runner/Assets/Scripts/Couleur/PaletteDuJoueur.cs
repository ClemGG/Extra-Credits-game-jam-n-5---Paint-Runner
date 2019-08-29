using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PaletteDuJoueur
{
    public enum TypeDePalette { Peintre, Développeur};
    public static TypeDePalette paletteDuJoueur = TypeDePalette.Peintre;

    public static Palette paletteUtilisée;




    public static void SelectionnerPalette(TypeDePalette nouvellePalette)
    {
        paletteDuJoueur = nouvellePalette;
        string nomDeLaPalette = null;

        nomDeLaPalette = (paletteDuJoueur == TypeDePalette.Peintre) ? nomDeLaPalette = "Palette Peintre" : "Palette Développeur";
        paletteUtilisée = Resources.Load<Palette>(nomDeLaPalette);
    }



    public static void GetColorFromTag(Ecran ecran)
    {
        Color c = Color.white;
        string renderTextureName = null;
        
        renderTextureName = GetRenderTextureNameFromColor(ecran.colorTag);
        ecran.imgMesh.material.SetTexture("_mainTexture", Resources.Load<RenderTexture>($"rt {renderTextureName}"));

        for (int i = 0; i < ecran.renderersAColorier.Length; i++)
        {
            ecran.renderersAColorier[i].material.SetColor("_EmissionColor", GetMaterialColorFromTag(ecran.colorTag));
            ecran.renderersAColorier[i].material.SetColor("_haloColor", GetMaterialColorFromTag(ecran.colorTag));
        }

        ParticleSystem.MainModule ma = ecran.bullesSystem.main;
        ma.startColor = GetMaterialColorFromTag(ecran.colorTag);

        ma = ecran.carréSystem.main;
        ma.startColor = GetMaterialColorFromTag(ecran.colorTag);

        ParticleSystemRenderer psr = ecran.bullesSystem.GetComponent<ParticleSystemRenderer>();
        psr.material.SetColor("_BaseColor", GetMaterialColorFromTag(ecran.colorTag));
        psr = ecran.carréSystem.GetComponent<ParticleSystemRenderer>();
        psr.material.SetColor("_EmissionColor", GetMaterialColorFromTag(ecran.colorTag));



        for (int i = 0; i < ecran.objetsASpawner.Length; i++)
        {
            ecran.objetsASpawner[i].SetActive(ecran.objetsASpawner[i].name == renderTextureName);
        }
    }

    public static Color GetMaterialColorFromTag(Couleur.ColorTag colorTag)
    {
        for (int i = 0; i < paletteUtilisée.couleursPossibles.Length; i++)
        {
            //Debug.Log(colorTag.ToString() + ", " + paletteUtilisée.couleursPossibles[i].couleur.tag.ToString());

            if(colorTag == paletteUtilisée.couleursPossibles[i].couleur.tag)
            {
                return paletteUtilisée.couleursPossibles[i].couleur.c;
            }
            else
            {
                if (colorTag == Couleur.ColorTag.Orange)
                {
                    if (paletteDuJoueur == TypeDePalette.Développeur)
                        return Color.cyan;
                }
                else if (colorTag == Couleur.ColorTag.Bleu)
                {
                    if (paletteDuJoueur == TypeDePalette.Peintre)
                        return Color.cyan;
                }
                else if(colorTag == Couleur.ColorTag.Magenta || colorTag == Couleur.ColorTag.Rouge)
                {
                    if (paletteDuJoueur == TypeDePalette.Peintre)
                        return new Color(1f, 0f, .5f, 1f);
                }
                else if(colorTag == Couleur.ColorTag.Rouge)
                {
                    if (paletteDuJoueur == TypeDePalette.Développeur)
                        return new Color(1f, 0f, .5f, 1f);
                }
            }
        }

        return Color.white;
    }

    

    private static string GetRenderTextureNameFromColor(Couleur.ColorTag colorTag)
    {
        switch (colorTag)
        {
            case Couleur.ColorTag.Rouge:
                return "rouge";
            case Couleur.ColorTag.Magenta:
                return "rouge";

            case Couleur.ColorTag.Jaune:
                return "jaune";

            case Couleur.ColorTag.Bleu:
                return "bleu";
            case Couleur.ColorTag.Cyan:
                if (paletteDuJoueur == TypeDePalette.Peintre)
                    return "bleu";
                else
                    return "orange";

            case Couleur.ColorTag.Vert:
                return "vert";
            case Couleur.ColorTag.Orange:
                return "orange";
            case Couleur.ColorTag.Violet:
                return "violet";
            case Couleur.ColorTag.Noir:
                return "noir";
            case Couleur.ColorTag.Blanc:
                return "blanc";

            default:
                return null;

        }
    }



    public static Couleur.ColorTag GetColorTagFromList(List<Couleur.ColorTag> couleursDansLeTrou)
    {
        switch (couleursDansLeTrou.Count)
        {
            case 0:
                return paletteDuJoueur == TypeDePalette.Peintre ? Couleur.ColorTag.Blanc : Couleur.ColorTag.Noir;

            case 1:
                return couleursDansLeTrou[0];

            case 3:
                if (couleursDansLeTrou.Contains(paletteDuJoueur == TypeDePalette.Peintre ? Couleur.ColorTag.Magenta : Couleur.ColorTag.Rouge) &&
                    couleursDansLeTrou.Contains(paletteDuJoueur == TypeDePalette.Peintre ? Couleur.ColorTag.Jaune : Couleur.ColorTag.Vert) &&
                    couleursDansLeTrou.Contains(paletteDuJoueur == TypeDePalette.Peintre ? Couleur.ColorTag.Cyan : Couleur.ColorTag.Bleu))
                    return paletteDuJoueur == TypeDePalette.Peintre ? Couleur.ColorTag.Noir : Couleur.ColorTag.Blanc;
                else
                    return couleursDansLeTrou[2];

            case 2:
                return GetCombinaisonFromList(couleursDansLeTrou);

            default:
                Debug.Log("Erreur : Il y a plus de 3 couleurs dans le trou ! La couleur a été remise par défaut à magenta.");
                return Couleur.ColorTag.Magenta;

        }

    }

    private static Couleur.ColorTag GetCombinaisonFromList(List<Couleur.ColorTag> couleursDansLeTrou)
    {
        foreach (Combinaison couleurADéterminer in paletteUtilisée.couleursPossibles)
        {
            if(couleurADéterminer.couleursNécessaires.Length == 2)
            {
                if(couleursDansLeTrou.Contains(couleurADéterminer.couleursNécessaires[0].tag) && couleursDansLeTrou.Contains(couleurADéterminer.couleursNécessaires[1].tag))
                {
                    return couleurADéterminer.couleur.tag;
                }
            }
        }


        return Couleur.ColorTag.Blanc;
    }
}
