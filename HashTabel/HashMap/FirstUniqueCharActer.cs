public class Solution {
    public int FirstUniqChar (string s) {
        int[] nums = new int[26];

        foreach (var item in s) {
            nums[s[i] - 'a'] += 1;
        }
        for (int i = 0; i < s.Length; i++) {
            if (nums[s[i] - 'a'] == 1) return i;
        }
        return -1;

    }
}