using UnityEngine;
using UnityEngine.Video;

public class MainCameraVideoPlayer : MonoBehaviour
{
	public static MainCameraVideoPlayer Instance;
	public bool IsVideoSkipped { get; private set; }

	private VideoPlayer _videoPlayer;
	[SerializeField] private string _basePath = "D:/My Folder/GitHubProjects/LegendsOfEndalor2Dv.0.1a/Assets/Videos/";
	[SerializeField] private string _videoNameToPlay;
	
    private void Start()
    {
		Instance = GetComponent<MainCameraVideoPlayer>();
		if (_videoNameToPlay != null) PlayNewVideo(_videoNameToPlay);
	}

	public void PlayNewVideo(string videoName)
    {
		IsVideoSkipped = false;
		_videoPlayer = gameObject.AddComponent<VideoPlayer>();
		_videoPlayer.playOnAwake = false;
		_videoPlayer.loopPointReached += OnReach;
		_videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
		_videoPlayer.url = _basePath + videoName + ".mp4";
		_videoPlayer.Play();
	}

	private void OnReach(VideoPlayer vp)
    {
		SkipVideo();
    }

	public void SkipVideo()
    {
		if (!IsVideoSkipped)
		{
			IsVideoSkipped = true;
			Destroy(_videoPlayer);
			Debug.Log("Video Skipped: " + IsVideoSkipped.ToString());
		}
	}
}
