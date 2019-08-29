using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsControl : MonoBehaviour
{
    public GameObject[] onglets;

    public void OuvrirOnglet(int index)
    {
        AudioManager.instance.Play("ButtonSelected");
        for (int i = 0; i < onglets.Length; i++)
        {
            onglets[i].SetActive(i == index);
        }
    }
}
