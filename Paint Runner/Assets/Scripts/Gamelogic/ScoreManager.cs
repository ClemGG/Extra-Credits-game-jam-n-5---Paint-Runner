using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Gradient gradientPeintre, gradientDéveloppeur;

    [SerializeField] TextMeshProUGUI décompteur, compteurDePoints;
    [SerializeField] GameObject ecranMort;

    [Space(10)]

    public bool isDead = false, gameHasStarted = false;
    [SerializeField] int score = 0;
    [SerializeField] float compteARebours = 3f, délaiAvantDécompte = .75f, invervalleEntreChaquePoint = .6f;
    float reboursTimer, scoreTimer;

    [Space(10)]

    public Animator vaisseauAnimator;

    public static ScoreManager instance;

    public void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }









    // Start is called before the first frame update
    IEnumerator Start()
    {
        ecranMort.SetActive(false);


        décompteur.gameObject.SetActive(false);
        compteurDePoints.gameObject.SetActive(false);

        yield return new WaitForSeconds(délaiAvantDécompte);

        décompteur.gameObject.SetActive(true);


        Gradient gradientToUse = PaletteDuJoueur.paletteDuJoueur == PaletteDuJoueur.TypeDePalette.Peintre ? gradientPeintre : gradientDéveloppeur;
        float startCompteARebours = reboursTimer = (float)compteARebours;
        reboursTimer += 1f;

        décompteur.text = compteARebours.ToString();

        AudioManager.instance.Play("countdown");
        while (!gameHasStarted)
        {
            if(reboursTimer >= (float)compteARebours)
            {
                reboursTimer -= Time.deltaTime;
            }
            else
            {
                compteARebours--;
                if(compteARebours == 0)
                {
                    décompteur.text = "GO!";
                    gameHasStarted = true;
                }
                else
                {
                    décompteur.text = compteARebours.ToString();
                }

                décompteur.color = gradientToUse.Evaluate(compteARebours / startCompteARebours);
            }

            yield return null;
        }

        yield return new WaitForSeconds(.5f);


        décompteur.gameObject.SetActive(false);
        compteurDePoints.gameObject.SetActive(true);
        compteurDePoints.text = score.ToString();

        AudioManager.instance.Play("startup vaisseau");

    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (!gameHasStarted || isDead)
    //        return;

    //    if(scoreTimer < invervalleEntreChaquePoint)
    //    {
    //        scoreTimer += Time.deltaTime;
    //    }
    //    else
    //    {
    //        scoreTimer = 0f;

    //        if (score < 9999)
    //        {
    //            score++;
    //            compteurDePoints.text = score.ToString();
    //        }
    //        else
    //        {
    //            compteurDePoints.text = "TILT";
    //        }
    //    }
    //}

    public void AddPoint()
    {
        if (score < 9999)
        {
            score++;
            compteurDePoints.text = score.ToString();
        }
        else
        {
            compteurDePoints.text = "TILT";
        }
    }



    public void Die()
    {
        AudioManager.instance.Play("destruction ecran 1");
        AudioManager.instance.Play("death");

        isDead = true;
        compteurDePoints.text = "X";
        compteurDePoints.color = Color.red;
        vaisseauAnimator.SetBool("death", true);
        AfficherEcranMort();
    }

    private void AfficherEcranMort()
    {
        ecranMort.SetActive(true);
        StartCoroutine(FaireMonterLeScore());
    }

    private IEnumerator FaireMonterLeScore()
    {

        yield return new WaitForSeconds(1.4f);

        int scoreAffiché = 0;
        float timer = 0f;
        ecranMort.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "0";

        while (scoreAffiché < score)
        {
            if(timer < .03f)
            {
                timer += Time.deltaTime;
                
            }
            else
            {
                scoreAffiché++;
                ecranMort.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = scoreAffiché.ToString();
                AudioManager.instance.Play("comptage score");
                timer = 0f;
            }
            yield return null;
        }

        ecranMort.GetComponent<Animator>().SetTrigger("done");
        //AudioManager.instance.Play("afficher ecran mort");

        if(score > PlayerPrefs.GetInt("highscore"))
            PlayerPrefs.SetInt("highscore", score);

        ecranMort.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("highscore", 0).ToString();
    }

    public void Retry()
    {
        AudioManager.instance.Play("ButtonSelected");
        SceneFader.instance.FadeToScene(1);

    }

    public void ReturnToMainMenu()
    {
        AudioManager.instance.Play("ButtonSelected");
        SceneFader.instance.FadeToScene(0);
    }
}

