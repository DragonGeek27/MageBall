using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking.Match;

namespace Assets.Scripts
{
    public static class AvailableMatchesList
    {
        public static event Action<List<MatchInfoSnapshot>> OnAvailableMatchesChanged = delegate { };
        private static List<MatchInfoSnapshot> matches = new List<MatchInfoSnapshot>();

        public static void HandleNewMatchList(List<MatchInfoSnapshot> matchList)
        {
            matches = matchList;
            OnAvailableMatchesChanged(matches);
        }
    }
}
