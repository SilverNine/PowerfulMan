using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameOverPopup : MonoBehaviour {
	private const string android_game_id = "2603439";
    private const string ios_game_id = "2603440";
 
    private const string rewarded_video_id = "rewardedVideo";

	[SerializeField]
	private MedalRenderer _medalRenderer = null;

	[SerializeField]
	private NumbersRenderer _score = null;

	[SerializeField]
	private NumbersRenderer _best = null;

	[SerializeField]
	private GameObject _newBestScore = null;

	public void Show () {
		gameObject.SetActive (true);

		_newBestScore.SetActive (Manager.Instance.isCurrentBestScore);
		_medalRenderer.Value = Manager.Instance.Score;
		_score.Value = Manager.Instance.Score;
		_best.Value = Manager.Instance.BestScore;
	}
		
	public void OkButton () {
		PlayerPrefs.SetInt("_lifeCount", PlayerPrefs.GetInt("_lifeCount") + 1);
		ShowRewardedAd();
	}
 
    public void ShowRewardedAd()
    {
		Debug.Log("ShowRewardedAd Start");
		Debug.Log(PlayerPrefs.GetInt("_lifeCount"));

		#if UNITY_ANDROID
				Advertisement.Initialize(android_game_id);
		#elif UNITY_IOS
				Advertisement.Initialize(ios_game_id);
		#else
				gameObject.SetActive (false);
				Manager.Instance.Replay ();
		#endif

        if (Advertisement.IsReady(rewarded_video_id) && PlayerPrefs.GetInt("_lifeCount") >= 5)
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(rewarded_video_id, options);
        } else {
			gameObject.SetActive (false);
			Manager.Instance.Replay ();
		}
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    Debug.Log("The ad was successfully shown.");
 
                    // to do ...
                    // 광고 시청이 완료되었을 때 처리
					gameObject.SetActive (false);
					Manager.Instance.Replay ();
					PlayerPrefs.SetInt ("_lifeCount", 0);
 
                    break;
                }
            case ShowResult.Skipped:
                {
                    Debug.Log("The ad was skipped before reaching the end.");
 
                    // to do ...
                    // 광고가 스킵되었을 때 처리
					gameObject.SetActive (false);
					Manager.Instance.Replay ();
					PlayerPrefs.SetInt ("_lifeCount", 0);
 
                    break;
                }
            case ShowResult.Failed:
                {
                    Debug.LogError("The ad failed to be shown.");
 
                    // to do ...
                    // 광고 시청에 실패했을 때 처리
					gameObject.SetActive (false);
					Manager.Instance.Replay ();
					PlayerPrefs.SetInt ("_lifeCount", 0);
 
                    break;
                }
        }
    }
}
