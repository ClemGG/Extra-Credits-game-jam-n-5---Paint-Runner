using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaisseauJeu : MonoBehaviour
{
    [SerializeField] Transform[] rails;
    [SerializeField] AnimationCurve railCurve;
    [SerializeField] float railTranslationSpeed = 2f, railChangementIndexSpeed = .2f;
    int railIndex = 1;
    float moveTimer = 0f, railChangementTimer = 0f;

    [Space(10)]

    [SerializeField] Couleur.ColorTag colorTag;
    [SerializeField] List<Couleur.ColorTag> couleursDansLeTrou;

    [Space(10)]

    [SerializeField] Material lumièreMaterial, anneauMaterial, terrainMaterial;
    [SerializeField] Transform[] peintures;

    Transform t;
    ScoreManager scoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        couleursDansLeTrou = new List<Couleur.ColorTag>();

        t = transform;
        scoreManager = ScoreManager.instance;

        colorTag = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Couleur.ColorTag.Blanc : Couleur.ColorTag.Noir;

        ChangerCouleurDuVaisseau();
        ChangerCouleurDuTerrain();
        ChangerCouleurDesPeintures();
    }

    


    // Update is called once per frame
    void FixedUpdate()
    {
        if (scoreManager.gameHasStarted && !scoreManager.isDead)
        {
            MoveAlongRails();
        }

        if (Mathf.Approximately(t.position.x, rails[railIndex].position.x))
        {
            for (int i = 0; i < peintures.Length; i++)
            {
                peintures[i].parent = null;
            }
        }

    }





    private void MoveAlongRails()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            for (int i = 0; i < peintures.Length; i++)
            {
                peintures[i].parent = t;
            }

            if (Input.GetAxis("Horizontal") < 0f)
            {
                if (railIndex < rails.Length - 1)
                    railIndex++;
            }
            else if (Input.GetAxis("Horizontal") > 0f)
            {
                if (railIndex > 0)
                    railIndex--;
            }

            moveTimer = 0f;
        }
        
        if (moveTimer < 1f)
        {
            moveTimer += Time.deltaTime;
        }
        
        t.position = new Vector3(Mathf.Lerp(t.position.x, rails[railIndex].position.x, railCurve.Evaluate(moveTimer) * railTranslationSpeed), t.position.y, t.position.z);
        //print(t.position + " ; Rail n°" + railIndex + " : " + rails[railIndex].position);
    }

    public Vector3 GetRailPosition()
    {
        return rails[railIndex].position;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("ecran"))
        {

            Couleur.ColorTag ct = c.GetComponent<Ecran>().colorTag;

            if (ct != colorTag)
            {
                scoreManager.Die();
                return;
            }
            else
            {
                ChangerCouleurDuTerrain();

                //Puis, on détruit tous les écrans sur la même rangée
                Ecran[] e = FindObjectsOfType<Ecran>();

                for (int i = 0; i < e.Length; i++)
                {
                    e[i].DetruireEcran();
                }

                RetirerCouleurAuVaisseau();

                if (ScoreManager.instance)
                    ScoreManager.instance.AddPoint();

            }

        }
    }



    public void AjouterCouleurAuVaisseau(Couleur.ColorTag nouvelleCouleur)
    {
        if (couleursDansLeTrou.Count == 3)
            return;

        AudioManager.instance.Play("changementVaisseau");


        couleursDansLeTrou.Add(nouvelleCouleur);
        colorTag = PaletteDuJoueur.GetColorTagFromList(couleursDansLeTrou);
        ChangerCouleurDuVaisseau();
    }

    public void RetirerCouleurAuVaisseau()
    {
        AudioManager.instance.Play("changementVaisseau");

        couleursDansLeTrou.Clear();
        colorTag = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Couleur.ColorTag.Blanc : Couleur.ColorTag.Noir;
        ChangerCouleurDuVaisseau();

    }



    private void ChangerCouleurDuTerrain()
    {
        Color c = (colorTag == Couleur.ColorTag.Blanc) ? Color.cyan : (colorTag == Couleur.ColorTag.Noir) ? Color.white : Color.white;

        anneauMaterial.SetColor("_mainColor", PaletteDuJoueur.GetMaterialColorFromTag(colorTag));
        anneauMaterial.SetColor("_pulseColor", c);
        terrainMaterial.SetColor("_mainColor", PaletteDuJoueur.GetMaterialColorFromTag(colorTag));
        terrainMaterial.SetColor("_pulseColor", c);
    }

    private void ChangerCouleurDuVaisseau()
    {
        Color c = (colorTag == Couleur.ColorTag.Blanc) ? Color.cyan : (colorTag == Couleur.ColorTag.Noir) ? Color.white : Color.white;

        lumièreMaterial.SetColor("_mainColor", PaletteDuJoueur.GetMaterialColorFromTag(colorTag));
        lumièreMaterial.SetColor("_pulseColor", c);
    }



    private void ChangerCouleurDesPeintures()
    {
        //Peinture rouge
        peintures[0].GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? new Color(1f, 0f, .5f, 1f) : Color.red);
        peintures[0].GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? new Color(1f, 0f, .5f, 1f) : Color.red);

        //Peinture jaune
        peintures[1].GetChild(0).gameObject.SetActive(PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre);
        peintures[1].GetChild(1).gameObject.SetActive(PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Développeur);

        //Peinture bleue
        peintures[2].GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Color.cyan : Color.blue);
        peintures[2].GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? Color.cyan : Color.blue);
    }
}
