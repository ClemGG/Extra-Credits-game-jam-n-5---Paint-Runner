using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcranSpawner : MonoBehaviour
{
    public Transform ecranPrefab, terrainPrefab, pointDeRotationTerrain, pointDeDépart;
   [HideInInspector] public Transform[] pointsDeSpawn;   //Rempli par les triggers
    [HideInInspector] public Transform terrainParent;  //Rempli par les triggers

    public static EcranSpawner instance;

    public Couleur nouvelleCouleur, couleurTemp;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }

        instance = this;


        nouvelleCouleur.tag = couleurTemp.tag = (Couleur.ColorTag)Random.Range(0, 8);
    }

    // Start is called before the first frame update
    void Start()
    {
        ObjectPooler.instance.SpawnFromPool(terrainPrefab.name.Replace("(Clone)", null), pointDeDépart.position, pointDeDépart.rotation, pointDeRotationTerrain);

    }
    


    public void SpawnEcrans()
    {
        couleurTemp.tag = (Couleur.ColorTag)Random.Range(0, 10);

        //print(couleurTemp.tag);

        for (int i = 0; i < pointsDeSpawn.Length; i++)
        {
            if(couleurTemp.tag != Couleur.ColorTag.Blanc && couleurTemp.tag != Couleur.ColorTag.Noir)
            {
                nouvelleCouleur.tag = (Couleur.ColorTag)Random.Range(0, 8);
            }
            else
            {
                nouvelleCouleur.tag = couleurTemp.tag;
            }

            ObjectPooler.instance.SpawnFromPool(ecranPrefab.name, pointsDeSpawn[i].position, pointsDeSpawn[i].rotation, terrainParent);
        }
    }


    public Couleur.ColorTag GetRandomTag()
    {
        if (nouvelleCouleur.tag == Couleur.ColorTag.Bleu)
        {
            nouvelleCouleur.tag = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Couleur.ColorTag.Cyan : Couleur.ColorTag.Bleu;
        }

        if (nouvelleCouleur.tag == Couleur.ColorTag.Orange)
        {
            nouvelleCouleur.tag = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Couleur.ColorTag.Orange : Couleur.ColorTag.Cyan;
        }

        if (nouvelleCouleur.tag == Couleur.ColorTag.Rouge)
        {
            nouvelleCouleur.tag = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Couleur.ColorTag.Magenta : Couleur.ColorTag.Rouge;
        }
        
        if (nouvelleCouleur.tag == Couleur.ColorTag.Magenta)
        {
            nouvelleCouleur.tag = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Développeur ? Couleur.ColorTag.Rouge : Couleur.ColorTag.Magenta;
        }

        return nouvelleCouleur.tag;
    }


}
