using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class CustomNetworkManager : NetworkManager
{

    private float nextRefreshTime;

    public void StartHost()
    {
        StartMatchMaker();
        matchMaker.CreateMatch("Match 1", 4, true, "", "", "", 0, 0, OnMatchCreated);

    }

    private void OnMatchCreated(bool success, string extendedInfo, MatchInfo responseData)
    {
        base.StartHost(responseData);
        RefreshMatches();
    }

    private void Update()
    {
        if (Time.time >= nextRefreshTime)
            RefreshMatches();
    }

    private void RefreshMatches()
    {
        nextRefreshTime = Time.time + 5f;
        if (matchMaker == null)
            StartMatchMaker();
        matchMaker.ListMatches(0, 10, "", true, 0, 0, HandleListMatchesComplete);
    }

    public void JoinMatch(MatchInfoSnapshot match)
    {

        if (matchMaker == null)
            StartMatchMaker();
        matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, HandleJoinedMatch);
    }

    private void HandleJoinedMatch(bool success, string extendedInfo, MatchInfo responseData)
    {

        StartClient(responseData);
    }

    private void HandleListMatchesComplete(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
    {
        AvailableMatchesList.HandleNewMatchList(responseData);
    }
}
