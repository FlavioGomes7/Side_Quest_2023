using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> arvoresCena;
    [SerializeField] private List<GameObject> inimigosCena;
    private int arvoresDerrubadas;
    private int inimigosDestruidos;
    [SerializeField] private int inimigosDestruidosPlayer;
    [Header("Menu")]
    [SerializeField] private GameObject menuVitoria;
    [SerializeField] private GameObject menuDerrota;
    [Header("Manipulador Variaveis para Derrota")]
    [SerializeField] private double percentualArvoresDerrubadas = 0.6;
    [SerializeField] private TextMeshProUGUI pontuacao;
    private string arvoreNameDefault;
    private List<(GameObject, bool)> arvoresDerrubadasList;

    private void Start()
    {
        pontuacao = GameObject.Find("PontuacaoGame").GetComponentInChildren<TextMeshProUGUI>();
        pontuacao.SetText("Arvores Derrubadas: " + arvoresDerrubadas + "\nInimigos Caídos: " + inimigosDestruidosPlayer);

        GameObject[] arvores = GameObject.FindGameObjectsWithTag("Arvore");
        arvoresCena.AddRange(arvores);
        arvoresDerrubadasList = new List<(GameObject, bool)>();

        foreach (GameObject arvore in arvoresCena)
        {
            arvoresDerrubadasList.Add((arvore, false));
        }

        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
        inimigosCena.AddRange(inimigos);

        arvoreNameDefault = arvoresCena[0].GetComponent<SpriteRenderer>().sprite.name;
    }

    private void Update()
    {
        ArvoreDerrubada();
        // Verificar apenas quando um objeto for removido
    }

    public void ArvoreDerrubada()
    {
        VerificarArvoresDerrubadas();
        // Verificar condições de vitória e derrota
        CondicaoDerrota();
    }

    public void InimigosMorto()
    {
        VerificarInimigosDestruidos();
        // Verificar condições de vitória e derrota
        CondicaoVitoria();
    }

    private void VerificarArvoresDerrubadas()
    {
        arvoresDerrubadas = 0;
        foreach (var (arvore, derrubada) in arvoresDerrubadasList)
        {
            if (derrubada)
            {
                arvoresDerrubadas++;
            }
            else
            {
                string arvoreSpriteName = arvore.GetComponent<SpriteRenderer>().sprite.name;
                if (arvoreSpriteName != arvoreNameDefault)
                {
                    arvoresDerrubadas++;
                    arvoresDerrubadasList.Remove((arvore, false));
                    arvoresDerrubadasList.Add((arvore, true));
                }
                else
                {
                    Debug.Log("Arvoreio Intacto");
                }
            }
        }
        SetPontuacao();
    }

    private void VerificarInimigosDestruidos()
    {
        inimigosDestruidosPlayer++;
        SetPontuacao();
    }

    private void SetPontuacao()
    {
        pontuacao.SetText("Arvores Derrubadas: " + arvoresDerrubadas + "\nInimigos Caídos: " + inimigosDestruidosPlayer);
    }

    private void CondicaoVitoria()
    {
        if (inimigosDestruidosPlayer == inimigosCena.Count && (arvoresDerrubadas / (float)arvoresCena.Count) < percentualArvoresDerrubadas)
        {
            Time.timeScale = 0f;
            menuVitoria.SetActive(true);
        }
    }

    private void CondicaoDerrota()
    {
        if ((arvoresDerrubadas / (float)arvoresCena.Count) >= percentualArvoresDerrubadas)
        {
            Time.timeScale = 0f;
            menuDerrota.SetActive(true);
        }
    }
}