using UnityEngine;
using UnityEngine.Video;

public class MainCameraVideoPlayer : MonoBehaviour
{
	public static MainCameraVideoPlayer instance;
	VideoPlayer videoPlayer;
	[SerializeField] string introVideoName;

	public bool videoSkipped { get; private set; }

    MainCameraVideoPlayer()
    {
		videoSkipped = false;
    }

    void Start()
    {
		instance = GetComponent<MainCameraVideoPlayer>();
		if (introVideoName != null) PlayNewVideo(introVideoName);
	}


	public void PlayNewVideo(string videoUrl)
    {
		CursorChangerScript.instance.hideCursor = true;
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
			CursorChangerScript.instance.hideCursor = false;
			videoSkipped = true;
			Destroy(videoPlayer);
			Debug.Log("Video Skipped: " + videoSkipped.ToString());
		}
	}
}
