using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarVitoria : MonoBehaviour
{
    [Header("Balanceamento")]
    [SerializeField] private int limiteInimigosPorCena;
    [SerializeField] private bool isAllSpawned;
    private List<GameObject> inimigosEmCena = new List<GameObject>();
    private int inimigosGerados;
    private int inimigosDestruidos;

    void Start()
    {
        inimigosGerados = 0;
        inimigosDestruidos = 0;
    }

    void Update()
    {
        if (inimigosGerados >= limiteInimigosPorCena && inimigosDestruidos >= limiteInimigosPorCena)
        {
            isAllSpawned = true;
            Debug.Log("Todos os inimigos foram gerados!");
        }

        VerificarInimigosVivos();
    }

    public void AdicionarInimigo(GameObject inimigo)
    {
        inimigosEmCena.Add(inimigo);
        inimigosGerados++;
    }

    public void RemoverInimigo(GameObject inimigo)
    {
        inimigosEmCena.Remove(inimigo);
        inimigosDestruidos++;
    }

    public void VerificarInimigosVivos()
    {
        for (int i = inimigosEmCena.Count - 1; i >= 0; i--)
        {
            if (inimigosEmCena[i] == null)
            {
                inimigosEmCena.RemoveAt(i);
                inimigosDestruidos++;
            }
        }

        if (isAllSpawned && inimigosEmCena.Count == 0)
        {
            Debug.Log("Você Ganhou!");
        }
        else
        {
            Debug.Log("Ainda há inimigos vivos: " + inimigosEmCena.Count);
        }
    }
}
