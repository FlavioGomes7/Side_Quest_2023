using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, HideInInspector] private List<GameObject> arvoresCena;
    [SerializeField, HideInInspector] private List<GameObject> inimigosCena;
    private int arvoresDerrubadas;
    private int inimigosDestruidos;
    [Header("Menu")]
    [SerializeField] private GameObject menuVitoria;
    [SerializeField] private GameObject menuDerrota;
    [Header("Manipulador Variaveis para Derrota")]
    [SerializeField] private double percentualArvoresDerrubadas=0.6;

    private void Start()
    {
        GameObject[] arvores = GameObject.FindGameObjectsWithTag("Arvore");
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

        arvoresCena.AddRange(arvores);
        inimigosCena.AddRange(inimigos);
    }

    private void Update()
    {
        VerificarArvoresDerrubadas();
        VerificarInimigosDestruidos();
        CondicaoVitoria();
        CondicaoDerrota();
    }

    private void VerificarArvoresDerrubadas()
    {
        int arvoresVivas = 0;

        for (int i = 0; i < arvoresCena.Count; i++)
        {
            if (arvoresCena[i] != null)
            {
                arvoresVivas++;
            }
        }

        arvoresDerrubadas = arvoresCena.Count - arvoresVivas;
    }

    private void VerificarInimigosDestruidos()
    {
        int inimigosVivos = 0;

        for (int i = 0; i < inimigosCena.Count; i++)
        {
            if (inimigosCena[i] != null)
            {
                inimigosVivos++;
            }
        }

        inimigosDestruidos = inimigosCena.Count - inimigosVivos;
    }

    private void CondicaoVitoria()
    {
        if (inimigosDestruidos == inimigosCena.Count && (arvoresDerrubadas / (float)arvoresCena.Count) < percentualArvoresDerrubadas)
        {
            Time.timeScale = 0f;
            menuVitoria.SetActive(true);
        }
        
    }

    private void CondicaoDerrota()
    {
        if ((arvoresDerrubadas / (float)arvoresCena.Count) == percentualArvoresDerrubadas)
        {
            Time.timeScale = 0f;
            menuDerrota.SetActive(true);
        }
    }

}
