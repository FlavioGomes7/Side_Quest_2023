using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private SaveManager saveManager;
    private bool isPaused = false;
    private bool isDesktop;
    [SerializeField] private GameObject volumeObject;
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            GameObject saveManagerObject = new GameObject("SaveManager");
            saveManager = saveManagerObject.AddComponent<SaveManager>();
        }
    }
    void Start()
    {
        Time.timeScale = 1f;
        volumeObject.SetActive(false);
        //Verifica Qual Plataforma o Jogo esta em Execu��o
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // C�digo para ambiente WebGL
            Debug.Log("Executando em WebGL");
            isDesktop = false;
        }
        else
        {
            // C�digo para ambiente de desktop
            Debug.Log("Executando em desktop");
            isDesktop = true;
        }
        float savedVolume = saveManager.LoadVolume();
        SetVolume(savedVolume);
        // Adiciona um ouvinte para o evento OnValueChanged do Slider
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }
    //Menu Principal Botoes
        //Botao q Inicia o Jogo (carrega a cena de Jogo)
    public void PlayButton() 
    {
        SceneManager.LoadScene(1);
        Debug.Log("O jogo Iniciou");
    }
        //Botao Configuracoes do Jogo, abre um menu para manipular o volume
    public void ConfigsButton()
    {
        volumeObject.SetActive(true);
    }
        //Botao Sair do Jogo, sai do Jogo em Aplica��es desktop e em WebGL ele apenas mant�m na tela inicial
    public void QuitButton()
    {
        if (!isDesktop)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            Application.Quit();
        }
    }
    //Definir um Valor para o Volume e o Salva
    public void SetVolume(float volume)
    {
        volumeSlider.value = volume;
        AudioListener.volume = volume;
        // Salva o valor do volume
        saveManager.SaveVolume(volume);
    }
    //Executa a fun��o Set Volume quando o Valor � alterado na fun��o Slider
    public void OnVolumeChanged(float volume)
    {
        SetVolume(volume);
    }
    //Fun��o de Retorno Para o Menu Principal
    public void MenuPrincipal()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            volumeObject.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(0);
            volumeObject.SetActive(false);
        }
    }
    //Fun��o de Continuar para prosseguir com o Jogo Pausado, apenas desativa o Pause;
    public void ContinuarBtn()
    {
            volumeObject.SetActive(false);
    }
    //Verifica a entrada da Tecla "P", e executa um das fun�oes dependendo do estado de isPaused
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    //Pausa o Jogo com Time.timeScale = 0f e ativa o menu de Pause
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        volumeObject.SetActive(true);
    }
    //Retoma o Jogo com Time.timeScale = 1f e Desativa o menu de Pause
    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        ContinuarBtn();
    }
    //Passa para o Proximo N�vel, caso haja um proximo nivel
    public void ProximoNivel()
    {
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex +1)
        {
            Debug.Log("Acabou os Niveis, mas logo traremos mais!");
        }
        else 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            
        }
        
    }
}
