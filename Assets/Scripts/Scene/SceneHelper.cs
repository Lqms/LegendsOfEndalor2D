using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum SceneNames
{
    IntroVideoScene,
    MainMenuScene,
    CreateCharacterScene,
    TutorialScene
}


public class SceneHelper : MonoBehaviour
{
    public static SceneHelper Instance;
    public SceneNames SceneName => _sceneName;
    public int SceneIndex => _sceneIndex;

    [Header("Scene Info")]
    [SerializeField] private SceneNames _sceneName;
    [SerializeField] private int _sceneIndex;
    private bool _sceneIsLoading;

    [Header("Loading scene UI objects")]
    [SerializeField] private GameObject _panelLoadingScene;
    [SerializeField] private Image _imageLoadingSceneProgress;
    [SerializeField] private Text _textLoadingSceneProgress;
    [SerializeField] private Text _textLoadingSceneHint;
    private AsyncOperation asyncOperationForLoadingScene;

    void Start()
    {
        Instance = gameObject.GetComponent<SceneHelper>();
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        _sceneIsLoading = false;
    }

    /// <summary>
    /// Loading a scene and showing the loading panel
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadSceneByName(SceneNames sceneName)
    {
        if (_sceneIsLoading) return;
        _sceneIsLoading = true;
        Time.timeScale = 1;
        StartCoroutine(SceneLoadingCoroutine(sceneName.ToString()));
    }

    private IEnumerator SceneLoadingCoroutine(string sceneName)
    {
        GetNewHintForLoadingPanel();
        _panelLoadingScene.SetActive(true);
        yield return new WaitForSeconds(1f);
        asyncOperationForLoadingScene = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperationForLoadingScene.isDone)
        {
            float progress = asyncOperationForLoadingScene.progress / 0.9f;
            _textLoadingSceneProgress.text = "Loading: " + string.Format("{0:0}%", progress * 100f);
            _imageLoadingSceneProgress.fillAmount = progress;
            yield return 0;
        }
    }

    private void GetNewHintForLoadingPanel()
    {
        TextAsset data = Resources.Load("Loading hints") as TextAsset;
        string[] hints = data.ToString().Split('|');
        int index = Random.Range(0, hints.Length);
        _textLoadingSceneHint.text = hints[index];
    }

    // Scenes Logic
    private void Update()
    {
        IntroVideoSceneLogic();
        MainMenuSceneLogic();
    }

    /// <summary>
    /// Change scene when intro video is over or skipped
    /// </summary>
    private void IntroVideoSceneLogic()
    {
        if (_sceneName != SceneNames.IntroVideoScene) return;

        if (Input.GetKeyDown(KeyCode.Escape) && !UIManager.instance.PanelAcceptChoose.activeInHierarchy)
        {
            UIManager.instance.ShowPanelAcceptChoose(message: "Skip Intro?");
            UIManager.instance.ButtonAcceptChooseAgree.onClick.AddListener(SkipIntroScene);
        }
        if (MainCameraVideoPlayer.Instance.IsVideoSkipped) LoadSceneByName(SceneNames.MainMenuScene);
    }

    private void SkipIntroScene()
    {
        MainCameraVideoPlayer.Instance.SkipVideo();
        LoadSceneByName(SceneNames.MainMenuScene);
    }

    /// <summary>
    /// Change scene on event in ImageBGSceneHelper's curtain animation
    /// </summary>
    private void MainMenuSceneLogic()
    {
        if (_sceneName != SceneNames.MainMenuScene) return;
    }
     

}
