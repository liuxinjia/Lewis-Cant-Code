public class Solution {

    public int[] SortArray (int[] nums) {
        if (nums.Length < 1) return null;
        MergeSort (0, nums.Length - 1, nums);

        return nums;
    }

    /* #region  MergeSort(Change the parameter) */
    // Time Complexity : O (logn * n);
    // Space Complexity : O (n);
    void MergeSort (int start, int end, int[] nums) {
        if (start == end) return;

        int mid = start + (end - start) / 2;
        MergeSort (start, mid, nums);
        MergeSort (mid + 1, end, nums);

        Merge (nums, start, mid, end);
    }

    void Merge (int[] nums, int start, int mid, int end) {
        int l1 = mid - start + 1;
        int l2 = end - mid;
        int[] left = new int[l1];
        int[] right = new int[l2];

        Array.Copy (nums, start, left, 0, l1);
        Array.Copy (nums, mid + 1, right, 0, l2);

        for (int i = 0, j = 0, k = start; i < l1 || j < l2; k++) {
            if (i == l1 || (j < l2 && left[i] > right[j])) {
                nums[k] = right[j];
                j++;
            } else {
                nums[k] = left[i];
                i++;
            }
        }

    }

    /* #endregion */

    /* #region  Otherwise: don't cahnge the array */
    int[] MergeSort (int start, int end, int[] nums) {
        if (start == end) return new int[] { nums[start] };

        int mid = start + (end - start) / 2;
        return Merge (MergeSort (start, mid, nums), MergeSort (mid + 1, end, nums));
    }

    int[] Merge (int[] left, int[] right) {
        var nums = new int[left.Length + right.Length];

        for (int i = 0, j = 0, cur = 0; i < left.Length || j < right.Length; cur++) {
            if (i >= left.Length || (j < right.Length && left[i] > right[j])) {
                nums[cur] = right[j];
                j++;
            } else {
                nums[cur] = left[i];
                i++;
            }
        }
        return nums;

    }
    /* #endregion */

    /* #region  QuickSort*/
    // Time Complexity : best : O (log2n * n) when it 's a balanced binary search tree:
    // worst:O (n*n) when in a single subtree
    // So average : O (logn * n) 
    // space complexity : O (n)
    void QuickSort (int start, int end, int[] nums) {
        if (start >= end) return;

        int pivot = Partitioning (start, end, nums);
        QuickSort (start, nums, pivot - 1);
        QuickSort (pivot + 1, end, nums);
    }

    int Partitioning (int start, int end, int[] nums) {
        int pivot = FindPivotPoint (end, nums);
        int curIndex = start;
        for (int i = start; i < end; i++) {
            if (nums[i] < pivot) {
                Swap (nums, curIndex, i);
                curIndex++;
            }
        }

        Swap (nums, curIndex, end);
        return curIndex;
    }

    private int FindPivotPoint (int end, int[] nums) {
        return nums[end];
    }

    private void Swap (int[] nums, int curIndex, int i) {
        var temp = nums[curIndex];
        nums[curIndex++] = nums[i];
        nums[i] = temp;
    }

    /* #endregion */

}