using System.Collections.Generic;

public class Solution {
    public string[] FindRestaurant (string[] list1, string[] list2) {
        var map = new Dictionary<string, int> ();

        for (int i = 0; i < list1.Length; i++) {
            map.TryAdd (list1[i], i);
        }

        var minMap = new Dictionary<string, int> ();
        int min = Int32.MaxValue;
        for (int i = 0; i < list2.Length; i++) {
            if (map.ContainsKey (list2[i])) {
                int index = i + map[list2[i]];
                if (min >= index) {
                    min = index;
                    minMap.Add (list2[i], min);
                }
            }
        }

        var rList = new List<string> ();
        foreach (var pair in minMap) {
            if (pair.Value == min) rList.Add (pair.Key);
        }

        return rList.ToArray ();
    }

    //CLear list automatically

    public string[] FindRestaurant (string[] list1, string[] list2) {
        var map = new Dictionary<string, int> ();

        for (int i = 0; i < list1.Length; i++) {
            map.TryAdd (list1[i], i);
        }

        var rList = new List<string> ();
        int min = Int32.MaxValue;
        for (int i = 0; i < list2.Length; i++) {
            if (map.ContainsKey (list2[i])) {
                int index = i + map[list2[i]];
                if (min >= index) {
                    if (min > index) {
                        min = index;
                        rList.Clear ();
                    }
                    rList.Add (list2[i]);
                }
            }
        }

        return rList.ToArray ();
    }
}