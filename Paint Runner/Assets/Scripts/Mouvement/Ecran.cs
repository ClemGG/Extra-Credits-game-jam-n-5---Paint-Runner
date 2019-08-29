using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecran : MonoBehaviour
{
    public GameObject[] objetsASpawner;
    public MeshRenderer[] renderersAColorier;
    public MeshRenderer imgMesh;  //Celui qui porte le Material de la TV

    [Space(10)]

    public ParticleSystem carréSystem, bullesSystem;

    public Couleur.ColorTag colorTag;


    //[Space(10)]


    //public Transform[] prefabsToSpawnOnDeath;
    public Transform particulePrefab;


    private Transform t;

    

    private void OnEnable()
    {
        colorTag = EcranSpawner.instance.GetRandomTag();
        PaletteDuJoueur.GetColorFromTag(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        t = transform;
    }

    public void DetruireEcran()
    {

        AudioManager.instance.Play("destruction ecran 1");
        AudioManager.instance.Play("destruction ecran 2");

        //On fait apparaître les particules d'explosion
        GameObject ps = ObjectPooler.instance.SpawnFromPool(particulePrefab.name, t.position, t.rotation);

        ParticleSystemRenderer psr = ps.transform.GetChild(1).GetComponent<ParticleSystemRenderer>();
        psr.material.SetColor("_BaseColor", PaletteDuJoueur.GetMaterialColorFromTag(colorTag));
        psr.material.SetColor("_EmissionColor", PaletteDuJoueur.GetMaterialColorFromTag(colorTag));


        gameObject.SetActive(false);
    }
}
