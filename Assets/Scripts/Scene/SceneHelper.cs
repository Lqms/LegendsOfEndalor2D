using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHelper : MonoBehaviour
{
    public static SceneHelper instance;

    [Header("Scene Info")]
    public string sceneName;
    public int sceneIndex;
    private bool sceneIsLoading;

    [Header("Loading scene UI objects")]
    [SerializeField] GameObject panel;
    [SerializeField] Image image;
    [SerializeField] Text textProgress;
    [SerializeField] Text textHint;
    AsyncOperation asyncOperation;



    void Start()
    {
        instance = gameObject.GetComponent<SceneHelper>();
        sceneName = SceneManager.GetActiveScene().name;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIsLoading = false;
    }

    // Load scene and show panel loading
    public void LoadSceneByName(string name)
    {
        if (sceneIsLoading) return;
        sceneIsLoading = true;
        StartCoroutine(LoadSceneByNameCoroutine(name));
    }

    private IEnumerator LoadSceneByNameCoroutine(string name)
    {
        NewHint();
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        asyncOperation = SceneManager.LoadSceneAsync(name);
        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            textProgress.text = "Loading: " + string.Format("{0:0}%", progress * 100f);
            image.fillAmount = progress;
            yield return 0;
        }
    }

    private void NewHint()
    {
        TextAsset data = Resources.Load("Loading hints") as TextAsset;
        string[] hints = data.ToString().Split('|');
        int index = Random.Range(0, hints.Length);
        textHint.text = hints[index];
    }

    // Scenes Logic
    private void Update()
    {
        IntroScene();
        MainMenuScene();
    }

    /// <summary>
    /// Change scene when intro video is over or skipped
    /// </summary>
    private void IntroScene()
    {
        if (sceneName != "IntroVideoScene") return;
        if (Input.GetKeyDown(KeyCode.Escape) && !UIManager.instance.panel.activeInHierarchy)
        {
            UIManager.instance.ShowPanelAcceptChoose(message: "Skip Intro?");
            UIManager.instance.buttonAgree.onClick.AddListener(SkipIntroScene);
        }
        if (MainCameraVideoPlayer.instance.videoSkipped) LoadSceneByName("MainMenuScene");
    }

    private void SkipIntroScene()
    {
        MainCameraVideoPlayer.instance.SkipVideo();
        LoadSceneByName("MainMenuScene");
    }

    /// <summary>
    /// Change scene on event in ImageBGSceneHelper's curtain animation
    /// </summary>
    private void MainMenuScene()
    {
        if (sceneName != "MaunMenuScene") return;
    }
     

}
