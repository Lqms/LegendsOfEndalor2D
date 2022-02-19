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

    [Header("Settings panel UI objects")]
    [SerializeField] private GameObject _panelSettings;
    [SerializeField] private Slider _sliderSettingsSound;
    private bool isPanelSettingsActive = false;

    [Header("Character portrait and info: warrior, archer, mage ")]
    [SerializeField] private Sprite[] _portraitSprites;
    [SerializeField] private Image _portraitImage;

    [Header("Point bars")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _manaBar;
    [SerializeField] private Slider _energyBar;

    private void Start()
    {
        instance = GetComponent<UIManager>();

        _buttonAcceptChooseAgree.onClick.AddListener(HidePanelAcceptChoose);
        _buttonAcceptChooseCancel.onClick.AddListener(HidePanelAcceptChoose);

        if (_panelSettings != null)
        {
            _sliderSettingsSound.value = AudioListener.volume;
            _UIObjects.Add(_panelSettings);
        }

        if (_panelAcceptChoose != null)
        {
            _UIObjects.Add(_panelAcceptChoose);
        }

        if (_portraitImage != null)
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
        _healthBar.maxValue = PlayerManager.instance.maxHealth;
        _manaBar.maxValue = PlayerManager.instance.maxMana;
        _energyBar.maxValue = PlayerManager.instance.maxEnergy;
    }

    private void Update()
    {
        if (SceneHelper.Instance.SceneIndex > 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) TogglePanelSettings(); // Settings
        }

        if (FindObjectOfType<PlayerManager>() != null)
        {
            _healthBar.value = PlayerManager.instance.currentHealth;
            _manaBar.value = PlayerManager.instance.currentMana;
            _energyBar.value = PlayerManager.instance.currentEnergy;
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
        _panelSettings.SetActive(true);      
    }

    public void HidePanelSettings()
    {
        Time.timeScale = 1;
        CursorChangerScript.Instance.isCursorAtcive = false;

        isPanelSettingsActive = false; 
        _panelSettings.SetActive(false);    
    }

    public void SliderSoundOnChange()
    {
        AudioListener.volume = _sliderSettingsSound.value;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        _panelSettings.SetActive(false);
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
        _portraitImage.sprite = _portraitSprites[(int)PlayerManager.instance._classOfPlayer];
    }
}
