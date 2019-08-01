using System.Collections.Generic;
public class Solution {
    public int LengthOfLongestSubstring (string s) {
        int count = 0;
        int max = 0;

        var map = new Dictionary<char, int> ();
        for (int i = 0; i < s.Length; i++) {
            if (!map.TryAdd (s[i], i)) {
                i = map[s[i]];
                max = Math.Max (max, count);
                map = new Dictionary<char, int> ();
                count = 0;
            } else max = Math.Max (max, ++count);
        }

        return max;
    }

    //Optimize 
    public int LengthOfLongestSubstring (string s) {
        int max = 0;

        var map = new Dictionary<char, int> ();
        for (int i = 0, j = 0; i < s.Length; i++) {
            if (!map.TryAdd (s[i], i)) {
                j = Math.Max (j, map[s[i]]);
            }

            max = Math.Max (max, i - j);
        }

        return max;
    }

    //Optimize 1: use array ASCII instead of hash map
    public int LengthOfLongestSubstring (string s) {
        int max = 0;
        int[] array = new int[256];

        for (int i = 0, j = 0; i < s.Length; i++) {
            j = array[(int) s[i]] == 0 ? j : Math.Max (j, array[(int) s[i]]);
            array[(int) s[i]] = i + 1;
            max = Math.Max (max, i - j + 1);
        }

        return max;
    }
}