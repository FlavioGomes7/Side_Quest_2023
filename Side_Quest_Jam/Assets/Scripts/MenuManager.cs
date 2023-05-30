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
        volumeObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        volumeObject.SetActive(false);
        //Verifica Qual Plataforma o Jogo esta em Execução
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // Código para ambiente WebGL
            Debug.Log("Executando em WebGL");
            isDesktop = false;
        }
        else
        {
            // Código para ambiente de desktop
            Debug.Log("Executando em desktop");
            isDesktop = true;
        }
        float savedVolume = saveManager.LoadVolume();
        SetVolume(savedVolume);
        // Adiciona um ouvinte para o evento OnValueChanged do Slider
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }
    //Muda para a cena de Jogo
    public void PlayButton() 
    {
        SceneManager.LoadScene("testeStart");
        Debug.Log("O jogo Iniciou");
    }
    public void ConfigsButton()
    {
        volumeObject.SetActive(true);
    }
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
    public void SetVolume(float volume)
    {
        volumeSlider.value = volume;
        AudioListener.volume = volume;
        // Salva o valor do volume
        saveManager.SaveVolume(volume);
    }
    public void OnVolumeChanged(float volume)
    {
        SetVolume(volume);
    }
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
    public void ContinuarBtn()
    {
            volumeObject.SetActive(false);
    }

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
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        volumeObject.SetActive(true);
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        volumeObject.SetActive(false);
        // Adicione aqui qualquer código adicional que você queira executar quando o jogo for despausado
    }
}
