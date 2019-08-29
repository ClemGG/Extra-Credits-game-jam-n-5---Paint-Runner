using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelererTerrain : MonoBehaviour
{
    [SerializeField] Vector2 vitesseMinMax = new Vector2(-2f, -5f);
    [SerializeField] float duration = 180f;
    [SerializeField] AnimationCurve accélérationDépartCurve;
    float timer = 0f, startupTimer = 0f;

    RotateAlongAxeses rot;
    ScoreManager scoreManager;

    private void Start()
    {
        rot = GetComponent<RotateAlongAxeses>();
        scoreManager = ScoreManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreManager.gameHasStarted && !scoreManager.isDead)
        {

            if(startupTimer < 1f)
            {
                startupTimer += Time.deltaTime;
                rot.Speed = Mathf.Lerp(0f, vitesseMinMax.x, accélérationDépartCurve.Evaluate(startupTimer));
            }
            else
            {
                timer += Time.deltaTime;
                rot.Speed = Mathf.Lerp(vitesseMinMax.x, vitesseMinMax.y, timer / duration);
            }
        }
        else
        {
            rot.Speed = 0f;
        }
    }
}
