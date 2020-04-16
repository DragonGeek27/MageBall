using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

public class MatchListPanel : MonoBehaviour
{

    [SerializeField]
    public JoinButton joinButtonPrefab;
    public JoinButton joinObjPrefab;
    public Transform joinParent;
    private void Awake()
    {
        AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
        joinParent = GameObject.Find("MatchListPanel").transform;
    }

    private void AvailableMatchesList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches)
    {
        clearExistingButtons();
        CreateNewJoinGameButtons(matches);
    }

    private void CreateNewJoinGameButtons(List<MatchInfoSnapshot> matches)
    {
        foreach (var match in matches)
        {
            var button = Instantiate(joinButtonPrefab, gameObject.transform.position, gameObject.transform.rotation);
            button.Initialize(match, transform);

            var obj = Instantiate(joinObjPrefab);
            obj.transform.SetParent(joinParent.transform);
            obj.Initialize(match, transform);
        }
    }

    private void clearExistingButtons()
    {
        var buttons = GetComponentsInChildren<JoinButton>();
        foreach (var button in buttons)
        {
            Destroy(button.gameObject);
        }
        var objs = GetComponentsInChildren<JoinButton>();
        foreach (var obj in objs)
        {
            Destroy(obj.gameObject);
        }
    }


}
