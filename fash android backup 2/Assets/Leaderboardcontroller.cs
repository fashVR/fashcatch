using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Leaderboardcontroller : MonoBehaviour
{
    public InputField MemberID, PlayerScore;
    public int ID;

    private void Start()
    {
        StartCoroutine(LoginRoutine());
    }
    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    public void SubmitScore()
    {
        
        LootLockerSDKManager.SubmitScore(MemberID.text, int.Parse(PlayerScore.text), ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }


}
