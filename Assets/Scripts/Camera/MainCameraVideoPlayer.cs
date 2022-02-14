using UnityEngine;
using UnityEngine.Video;

public class MainCameraVideoPlayer : MonoBehaviour
{
	VideoPlayer videoPlayer;
	[SerializeField] string introVideoName;

	public bool videoSkipped { get; private set; }
    MainCameraVideoPlayer()
    {
		videoSkipped = false;
    }

    void Start()
    {
		if (introVideoName != null) PlayNewVideo(introVideoName);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !UIManager.instance.panelAccept.activeInHierarchy)
		{
			UIManager.instance.ShowPanelAccept(message: "Skip Intro?");
			UIManager.instance.buttonAgree.onClick.AddListener(SkipVideo);
		}
	}

	public void PlayNewVideo(string videoUrl)
    {
		Cursor.visible = false;
		videoSkipped = false;

		videoPlayer = gameObject.AddComponent<VideoPlayer>();
		videoPlayer.playOnAwake = false;
		videoPlayer.loopPointReached += OnReach;
		videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
		videoPlayer.url = $"D:/My Folder/HelloWorld/Unity/Legends of Endalor 2D/LegendsOfEndalor2D/Assets/Videos/{videoUrl}.mp4";
		videoPlayer.Play();
	}

	void OnReach(VideoPlayer vp)
    {
		SkipVideo();
    }

	public void SkipVideo()
    {
		if (!videoSkipped)
		{
			Cursor.visible = true;
			videoSkipped = true;
			Destroy(videoPlayer);
			Debug.Log("Video Skipped: " + videoSkipped.ToString());
		}
	}
}
