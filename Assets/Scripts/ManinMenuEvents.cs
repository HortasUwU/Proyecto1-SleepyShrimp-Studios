using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor.PackageManager;


public class ManinMenuEvents : MonoBehaviour
{
    private UIDocument MainMenu;

    //Botones
    Button _newGameButton;
    Button _settings;
    Button _credits;
    Button _exit;

    private void Awake()
    {
  
        MainMenu = GetComponent<UIDocument>();
    }


    private void OnEnable()
    {
        VisualElement root = MainMenu.rootVisualElement;
        _newGameButton = root.Q<Button>("NewGameButton");
        _settings = root.Q<Button>("Settings");
        _credits = root.Q<Button>("Credits");
        _exit = root.Q<Button>("Exit");

        _newGameButton.RegisterCallback<ClickEvent>(startGame);
        //_settings.RegisterCallback<ClickEvent>(startGame);
        //_credits.RegisterCallback<ClickEvent>(startGame);
        _exit.RegisterCallback<ClickEvent>(closeGame);

    }

    private void OnDisable()
    {

    }

    /*
    private void OnPlayGameClick(ClickEvent evt) 
    {

    }
    */

    void startGame(ClickEvent evt)
    {
        SceneManager.LoadScene(1);
    }

    
    void closeGame(ClickEvent evt)
    {
        Application.Quit();
    }
}

