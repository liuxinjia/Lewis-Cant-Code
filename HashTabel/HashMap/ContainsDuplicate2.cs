using System.Collections.Generic;
public class Solution {
    public bool ContainsNearbyDuplicate (int[] nums, int k) {
        if (nums.Length < 2) return false;
        Dictionary<int, int> map = new Dictionary<int, int> ();

        for (int i = 0; i < nums.Length; i++) {
            if (map.ContainsKey (nums[i])) {
                int changeIndex = -1;
                foreach (var pair in map) {
                    if (pair.Key == nums[i]) {
                        if (i - pair.Value <= k)
                            return true;
                        changeIndex = pair.Key;
                    }
                }
                if (changeIndex != -1) map[changeIndex] = i;
            } else map.Add (nums[i], i);
        }
        return false;
    }

    //Sliding window
    public bool ContainsNearbyDuplicate (int[] nums, int k) {
        if (nums.Length < 2) return false;
        HashSet<int> set = new HashSet<int> ();

        for (int i = 0; i < nums.Length; i++) {
            if (i > k) set.Remove (nums[i - k - 1]);
            if (!set.Add (nums[i])) return true;
        }

        return false;
    }

    public bool ContainsNearbyDuplicate (int[] nums, int k) {
        if (nums.Length < 2) return false;
        SortedList<int, int> map = new SortedList<int, int> ();

        for (int i = 0; i < nums.Length; i++) {
            if (!map.TryAdd (nums[i], i))
                if (i <= k) return true;
                else map[nums[i]] = i;
        }

        return false;
    }
}