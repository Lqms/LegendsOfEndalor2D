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

    [Header("Panel Loading")]
    [SerializeField] GameObject panel;
    [SerializeField] Image image;
    [SerializeField] Text textLoading;
    [SerializeField] Text textHint;
    AsyncOperation asyncOperation;



    void Start()
    {
        instance = gameObject.GetComponent<SceneHelper>();
        sceneName = SceneManager.GetActiveScene().name;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIsLoading = false;
    }

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
        yield return new WaitForSeconds(0.1f); //1f
        asyncOperation = SceneManager.LoadSceneAsync(name);
        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            textLoading.text = "Loading: " + string.Format("{0:0}%", progress * 100f);
            image.fillAmount = progress;
            yield return 0;
        }
    }

    private void NewHint()
    {
        TextAsset data = Resources.Load("Loading hints") as TextAsset;
        string hint = data.ToString();
        Debug.Log(hint);
    }

    // Scenes Logic
    private void Update()
    {
        IntroScene();
    }

    /// <summary>
    /// Change scene when intro video is over or skipped
    /// </summary>
    private void IntroScene()
    {
        if (sceneName != "IntroVideoScene") return;
        if (FindObjectOfType<MainCameraVideoPlayer>().videoSkipped) LoadSceneByName("MainMenuScene");
    }

    /// <summary>
    /// Change scene on event in ImageBGSceneHelper's curtain animation
    /// </summary>
    private void MainMenuScene()
    {
        if (sceneName != "MaunMenuScene") return;
    }
     

}
