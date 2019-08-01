using System.Collections.Generic;

public class Solution {
    public int[] TwoSum (int[] nums, int target) {
        int[] result = new int[2];
        Dictionary<int, int> map = new Dictionary<int, int> ();
        for (int i = 0; i < nums.Length; i++) {
            if (map.ContainsValue (nums[i]))
                foreach (var item in map)
                    if (item.Value == nums[i]) {
                        result = new int { item.Key, i };
                        return result;
                    }
            map.Add (i, target - nums[i]);
        }

        return result;

    }
    public int[] TwoSum (int[] nums, int target) {
        Dictionary<int, int> map = new Dictionary<int, int> ();

        for (int i = 0; i < nums.Length; i++) {
            if (map.ContainsKey (nums[i])) {
                return new int[] {
                    (int) map[nums[i]], i };
            }
            map.TryAdd ( target -nums[i], i);
            // map[target - nums[i]] = i;
        }

        return null;
    }
}