using System.Collections.Generic;

public class Solution {
    public bool IsIsomorphic (string s, string t) {
        Dictionary<char, char> map = new Dictionary<char, char> ();

        for (int i = 0; i < s.Length; i++) {
            if (map.ContainsKey (s[i])) {
                if (map[s[i]] == t[i]) continue;
                else return false;
            } else {
                if (!map.ContainsValue (t[i]))
                    map.Add (s[i], t[i]);
                else
                    return false;
            }
        }

        return true;

    }

    public bool IsIsomorphic (string s, string t) {
        Dictionary<char, int> map1 = new Dictionary<char, int> ();
        Dictionary<char, int> map2 = new Dictionary<char, int> ();

        for (int i = 0; i < s.Length + 1; i++) {
            if (map1.Count > 0 && (map1[s[i - 1]] != map2[t[i - 1]])) return false;
            if (i == s.Length) return true;
            map1.TryAdd (s[i], i);
            map2.TryAdd (t[i], i);
        }
        return true;
    }

    public bool IsIsomorphic (string s, string t) {
        int[] m1 = new int[256];
        int[] m2 = new int[256];

        for (int i = 0; i < s.Length; i++) {
            if (m1[(int) s[i]] != m2[(int) t[i]]) return false;
            m1[(int) s[i]] = i + 1;
            m2[(int) t[i]] = i + 1;
        }
        return true;
    }
}