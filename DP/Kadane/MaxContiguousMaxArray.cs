//the evolution from the worst brute force to the best DP

//o(n^2), brute force
public int MaxSubArray (int[] nums) {
    int max = nums[0];
    for (int i = 0; i < nums.Length; i++) {
        int sum = 0;
        for (int j = i; j < nums.Length; j++) {
            sum += nums[j];
            max = Math.Max (max, sum);
        }
    }
    return max;
}

//O(nlogn): Naive divide and  conqueer, 
// comparing three max: leftmost , rightmost, the crossingmost from the middle
public class Solution {
    public int MaxSubArray (int[] nums) {
        return MaxSubArraySum (nums, 0, nums.Length);
    }

    int MaxSubArraySum (int[] nums, int start, int end) {
        if (start == end) return nums[start];
        int mid = start + (end - start) / 2;

        return Math.Max (Math.Max (MaxSubArraySum (nums, start, mid), MaxSubArraySum (nums, mid + 1, end)), MaxCrossingArray (nums, start, mid, end));
    }

    int MaxCrossingArray (int[] nums, int start, int mid, int end) {
        int left = nums[mid], right = nums[mid + 1];
        int sum = left;
        for (int i = mid - 1; i >= 0; i--) {
            sum += nums[i];
            if (sum > left) {
                left = sum;
            }
        }

        sum = right;
        for (int i = mid + 2; i < end; i++) {
            sum += nums[i];
            if (sum > right) {
                right = sum;
            }
        }
        return Math.Max (Math.Max (left, right), left + right);
    }
}

//O(n) :Optimized Divide and conquer
//  reduce the duplicated operation by recording it into sum
public class Solution {
    public int MaxSubArray (int[] nums) {
        return MaxSumSubArray (
            nums, 0, nums.Length - 1, out int left, out int right, out int sum);
    }
    int MaxSumSubArray (int[] nums, int start, int end, out int left, out int right, out int sum) {
        if (start == end) {
            left = right = sum = nums[end];
            return nums[end];
        }

        int mid = (end - start) / 2 + start;
        int leftMax = MaxSumSubArray (nums, start, mid, out int left1, out int right1, out int leftSum);
        int rightMax = MaxSumSubArray (nums, mid + 1, end, out int left2, out int right2, out int rightSum);

        left = Math.Max (left1, leftSum + left2);
        right = Math.Max (right2, rightSum + right1);
        sum = rightSum + leftSum;
        return Math.Max (Math.Max (leftMax, rightMax), right1 + left2);
    }

}

//O(n): Time Complexity; O(1):Space Complexity
//DP: Kadane's Algorithm
public int MaxSubArray (int[] nums) {
    if (nums.Length < 1) return Int32.MinValue;
    int maxSoFar = nums[0], maxEndingHere = nums[0];
    for (int i = 1; i < nums.Length; i++) {
        maxEndingHere = Math.Max (nums[i], maxEndingHere + nums[i]);
        maxSoFar = Math.Max (maxSoFar, maxEndingHere);
    }
    return maxSoFar;
}