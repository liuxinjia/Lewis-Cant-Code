using System.Collections.Generic;

public class Solution {
    public IList<IList<string>> GroupAnagrams (string[] strs) {
        List<IList<string>> rList = new List<IList<string>> ();

        int i = 0;
        var map = new Dictionary<string, int> ();
        foreach (var item in strs) {
            int index = Anagram (map, item, i);

            if (i == index) {
                rList.Add (new List<string> ());
                rList[i++].Add (item);
            } else
                rList[index].Add (item);
        }

        return rList;
    }

    //complicated solution 
    public IList<IList<string>> GroupAnagrams (string[] strs) {
        List<IList<string>> rList = new List<IList<string>> ();

        var map = new Dictionary<int, Dictionary<string, int>> ();
        int i = 0;
        foreach (var item in strs) {
            int sum = SumChars (item);
            if (map.ContainsKey (sum)) {
                int index = Anagram (map[sum], item, i);
                if (i == index) {
                    rList.Add (new List<string> ());
                    rList[i++].Add (item);
                } else
                    rList[index].Add (item);
            } else {
                map.Add (sum, new Dictionary<string, int> ());
                map[sum].Add (item, i);
                rList.Add (new List<string> (item));
                rList[i++].Add (item);
            }
        }

        return rList;
    }

    int Anagram (Dictionary<string, int> map2, string str, int i) {
        Dictionary<char, int> map1 = new Dictionary<char, int> ();
        foreach (var item in str) {
            if (map1.ContainsKey (item)) map1[item]++;
            else map1.Add (item, 1);
        }

        foreach (var item in map2) {
            if (item.Key == str) return item.Value;
            if (SumChars (item.Key) != SumChars (str)) continue;
            bool skip = true;
            var tempMap = new Dictionary<char, int> (map1);

            foreach (var c in item.Key) {
                if (tempMap.ContainsKey (c)) {
                    if (tempMap[c] < 1) { skip = false; break; }
                    tempMap[c]--;
                } else { skip = false; break; }
            }
            if (skip) return item.Value;
        }
        map2.Add (str, i);
        return i;
    }

    int SumChars (string str) {
        int result = 0;
        foreach (var item in str)
            result += item;
        return result;
    }
}

//just sort it frst, dumpy.
//sort will not delete the repeat one.

public class Solution {
    public IList<IList<string>> GroupAnagrams (string[] strs) {
        List<IList<string>> rList = new List<IList<string>> ();

        var map = new Dictionary<string, List<string>> ();
        for (int i = 0; i < strs.Length; i++) {
            var temp = SortChars (strs[i]);
            if (map.ContainsKey (temp)) map[temp].Add (strs[i]);
            else {
                map.Add (temp, new List<string> ());
                map[temp].Add (strs[i]);
            }
        }
        foreach (var pair in map) {
            rList.Add (pair.Value);
        }

        return rList;
    }

    string SortChars (string str) {
        var strs = str.ToCharArray ();
        Array.Sort (strs);
        return new String (strs);
    }


}