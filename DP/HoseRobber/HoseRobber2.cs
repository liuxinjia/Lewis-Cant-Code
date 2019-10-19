public class Solution {
    public int Rob (int[] nums) {
        if (nums.Length < 2) return nums.Length < 1 ? 0 : nums[0];

        int a = 0, b = 0;
        int length = nums.Length;
        int max = 0;
        for (int i = 0; i < length - 1; i++) {
            int c = Math.Max (a + nums[i], b);
            a = b;
            b = c;
        }

        max = Math.Max (a, b);
        
        a = 0;
        b = 0;
        for (int i = 1; i < length; i++) {
            int c = Math.Max (a + nums[i], b);
            a = b;
            b = c;
        }

        return Math.Max (Math.Max (a, b), max);
    }
}