//Leetcode Problem: 33.
public class Solution {
    public int Search (int[] nums, int target) {
        if (nums.Length == 0) return -1;
        return BinarySearch (nums, target, FindMin (nums, 0, nums.Length - 1), 0, nums.Length - 1);
    }

    int FindMin (int[] nums, int lower, int upper) {
        if (lower == upper) return lower;

        int mid = (lower + upper) / 2;
        if (nums[mid] < nums[upper]) return FindMin (nums, lower, mid);
        return FindMin (nums, mid + 1, upper);
    }

    int BinarySearch (int[] nums, int target, int realStart, int lower, int upper) {
        if (lower > upper) return -1;

        int mid = (upper + lower) / 2;
        int realMid = (realStart + mid) % nums.Length;
        if (nums[realMid] == target) return realMid;
        if (nums[realMid] < target)
            return BinarySearch (nums, target, realStart, mid + 1, upper);

        return BinarySearch (nums, target, realStart, lower, mid - 1);

    }
}