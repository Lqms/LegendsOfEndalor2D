using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Button ButtonAcceptChooseAgree => _buttonAcceptChooseAgree;
    public GameObject PanelAcceptChoose => _panelAcceptChoose;


    private List<GameObject> _UIObjects = new List<GameObject>();

    [Header("Accept choose UI objects")]
    [SerializeField] private GameObject _panelAcceptChoose;
    [SerializeField] private Text _textAcceptChoose;
    [SerializeField] private Button _buttonAcceptChooseAgree;
    [SerializeField] private Button _buttonAcceptChooseCancel;

    // тут остановился
    [Header("Settings panel UI objects")]
    [SerializeField] GameObject panelSettings;
    [SerializeField] Slider sliderSettingsSound;
    private bool isPanelSettingsActive = false;

    [Header("character portrait and info: warrior, archer, mage ")]
    [SerializeField] Sprite[] portraitObjects;
    [SerializeField] Image portraitImage;

    [Header("Point bars")]
    [SerializeField] Slider healthBar;
    [SerializeField] Slider manaBar;
    [SerializeField] Slider energyBar;

    private void Start()
    {
        instance = GetComponent<UIManager>();

        _buttonAcceptChooseAgree.onClick.AddListener(HidePanelAcceptChoose);
        _buttonAcceptChooseCancel.onClick.AddListener(HidePanelAcceptChoose);

        if (panelSettings != null)
        {
            sliderSettingsSound.value = AudioListener.volume;
            _UIObjects.Add(panelSettings);
        }

        if (_panelAcceptChoose != null)
        {
            _UIObjects.Add(_panelAcceptChoose);
        }

        if (portraitImage != null)
        {
            Invoke("ChangePortrait", 0.1f);
        }

        if (FindObjectOfType<PlayerManager>() != null)
        {
            Invoke("FindPointsBars", 0.1f);
        }
    }

    private void FindPointsBars()
    {
        healthBar.maxValue = PlayerManager.instance.maxHealth;
        manaBar.maxValue = PlayerManager.instance.maxMana;
        energyBar.maxValue = PlayerManager.instance.maxEnergy;
    }

    private void Update()
    {
        if (SceneHelper.Instance.SceneIndex > 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) TogglePanelSettings(); // Settings
        }

        if (FindObjectOfType<PlayerManager>() != null)
        {
            healthBar.value = PlayerManager.instance.currentHealth;
            manaBar.value = PlayerManager.instance.currentMana;
            energyBar.value = PlayerManager.instance.currentEnergy;
        }
    }

    // Settings panel
    private void TogglePanelSettings()
    {
        foreach (GameObject UIobj in _UIObjects)
        {
            UIobj.SetActive(false);
        }

        if (isPanelSettingsActive) HidePanelSettings();
        else ShowPanelSettings();
    }

    public void ShowPanelSettings()
    {
        Time.timeScale = 0;
        CursorChangerScript.Instance.isCursorAtcive = true;

        isPanelSettingsActive = true;
        panelSettings.SetActive(true);      
    }

    public void HidePanelSettings()
    {
        Time.timeScale = 1;
        CursorChangerScript.Instance.isCursorAtcive = false;

        isPanelSettingsActive = false; 
        panelSettings.SetActive(false);    
    }

    public void SliderSoundOnChange()
    {
        AudioListener.volume = sliderSettingsSound.value;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        panelSettings.SetActive(false);
        if (SceneHelper.Instance.SceneName != SceneNames.MainMenuScene)
        {
            ShowPanelAcceptChoose(message: "Back to Main Menu?");
            _buttonAcceptChooseAgree.onClick.AddListener(BackToMainMenuAccept);
        }
    }

    void BackToMainMenuAccept()
    {
        SceneHelper.Instance.LoadSceneByName(SceneNames.MainMenuScene);
    }


    // Accept panel
    public void ShowPanelAcceptChoose(string message)
    {
        Time.timeScale = 0;
        CursorChangerScript.Instance.isCursorAtcive = true;

        _textAcceptChoose.text = message;
        _panelAcceptChoose.SetActive(true);
    }

    public void HidePanelAcceptChoose()
    {
        Time.timeScale = 1;
        CursorChangerScript.Instance.isCursorAtcive = false;

        _buttonAcceptChooseAgree.onClick.RemoveAllListeners();
        _buttonAcceptChooseAgree.onClick.AddListener(SoundManager.Instance.PlayButtonClickSound);
        _buttonAcceptChooseAgree.onClick.AddListener(HidePanelAcceptChoose);
        _panelAcceptChoose.SetActive(false);
    }


    // Panel character info
    void ChangePortrait()
    {
        portraitImage.sprite = portraitObjects[(int)PlayerManager.instance._classOfPlayer];
    }
}
