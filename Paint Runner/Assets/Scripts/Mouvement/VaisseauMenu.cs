using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaisseauMenu : MonoBehaviour
{
    public bool détruireObjet = true;
    public LayerMask ecranMask;

    public ParticleSystem réacteur, bulles;
    public Material vaisseauMat;

    Transform t;

    private void Start()
    {
        t = transform;
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(t.position, t.forward, out RaycastHit hit, 100f, ecranMask, QueryTriggerInteraction.Collide))
        {
            Couleur.ColorTag ct = hit.transform.GetComponent<Ecran>().colorTag;
            vaisseauMat.SetColor("_BaseColor", PaletteDuJoueur.GetMaterialColorFromTag(ct));
            vaisseauMat.SetColor("_EmissionColor", PaletteDuJoueur.GetMaterialColorFromTag(ct));




            ParticleSystem.MainModule ma = bulles.main;
            ma.startColor = PaletteDuJoueur.GetMaterialColorFromTag(ct);

            ma = bulles.main;
            ma.startColor = PaletteDuJoueur.GetMaterialColorFromTag(ct);


            ParticleSystemRenderer psr = réacteur.GetComponent<ParticleSystemRenderer>();
            psr.material.SetColor("_BaseColor", PaletteDuJoueur.GetMaterialColorFromTag(ct));
            psr.material.SetColor("_EmissionColor", PaletteDuJoueur.GetMaterialColorFromTag(ct));
        }
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("ecran"))
        {
            if (détruireObjet)
            {
                Ecran[] e = FindObjectsOfType<Ecran>();

                for (int i = 0; i < e.Length; i++)
                {
                    e[i].DetruireEcran();
                }
                //ColorTag ct = c.GetComponent<Ecran>().colorTag;
            }
        }
    }
}
