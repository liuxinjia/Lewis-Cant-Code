public class Solution {
    public int NthSuperUglyNumber (int n, int[] uglys) {
        if (n == 0 || uglys.Length < 1) return 0;
        int[] nums = new int[n], uglyIndex = new int[uglys.Length];

        nums[0] = 1;
        for (int i = 1; i < n; i++) {
            int min = nums[uglyIndex[0]] * uglys[0];
            for (int j = 1; j < uglys.Length; j++) {
                min = Math.Min (min, nums[uglyIndex[j]] * uglys[j]);
            }
            nums[i] = min;

            for (int j = 0; j < uglys.Length; j++) {
                if (min == nums[uglyIndex[j]] * uglys[j]) {
                    uglyIndex[j]++;
                }
            }
        }
        return nums[n - 1];
    }

    public int NthSuperUglyNumber (int n, int[] uglys) {
        if (n == 0 || uglys.Length < 1) return 0;
        int[] nums = new int[n], uglyIndex = new int[uglys.Length];

        int min = 1;
        for (int i = 0; i < n; ++i) {
            nums[i] = min;

            min = Int32.MaxValue;
            //consolidate int one signle loop
            //avoid two loops iterating  to  find the same number
            for (int j = 0; j < uglys.Length; ++j) {
                if (nums[i] == nums[uglyIndex[j]] * uglys[j]) ++uglyIndex[j];
                min = Math.Min (min, nums[uglyIndex[j]] * uglys[j]);
            }
        }
        return nums[n - 1];
    }

    public int NthSuperUglyNumber (int n, int[] uglys) {
        if (n == 0 || uglys.Length < 1) return 0;

        int[] nums = new int[n];
        int[] uglyIndex = new int[uglys.Length], prev = new int[uglyIndex.Length];
        for (int i = 0; i < uglyIndex.Length; i++) prev[i] = 1;

        int min = 1;
        for (int i = 0; i < n; ++i) {
            nums[i] = min;

            min = Int32.MaxValue;
            for (int j = 0; j < uglys.Length; ++j) {
                // The author did not only focus on how to handle the duplicate number.
                // Though your method also consolidates two loops into a single one,
                //  there are still additive calculations can be avoided by comparing previous array.
                //  That 's why it calls "trade-off space for speed".
                if (nums[i] == prev[j]) prev[j] = nums[uglyIndex[j]++] * uglys[j];
                min = Math.Min (min, prev[j]);
            }
        }
        return nums[n - 1];
    }

}