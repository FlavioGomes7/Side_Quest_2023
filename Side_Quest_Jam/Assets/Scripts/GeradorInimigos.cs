using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigos : MonoBehaviour
{
    [SerializeField] private GameObject inimigoPrefab;
    [SerializeField] private Transform pontoSpawn;
    [SerializeField] private VerificarVitoria verificarVitoria;

    [SerializeField] private int limiteInimigos;
    [SerializeField] private float intervaloGeracao;
    private float tempoUltimaGeracao;
    private int inimigosGerados;

    private void Start()
    {
        tempoUltimaGeracao = Time.time;
        inimigosGerados = 0;
    }

    private void Update()
    {
        if (inimigosGerados < limiteInimigos && Time.time - tempoUltimaGeracao >= intervaloGeracao)
        {
            GerarInimigo();
            tempoUltimaGeracao = Time.time;
        }
    }

    private void GerarInimigo()
    {
        GameObject novoInimigo = Instantiate(inimigoPrefab, pontoSpawn.position, Quaternion.identity);
        verificarVitoria.AdicionarInimigo(novoInimigo);
        inimigosGerados++;
    }

    private void OnDestroy()
    {
        verificarVitoria.RemoverInimigo(gameObject);
    }
}