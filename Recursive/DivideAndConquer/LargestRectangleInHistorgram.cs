using System.Collections.Generic;

public class Solution {

    /* #region  brute forece */
    // TLR
    public int LargestRectangleArea (int[] heights) {
        int max = 0;
        int length = heights.Length;
        for (int i = 0; i < length; i++) {
            int cur = heights[i];
            int sum = cur;

            for (int j = i + 1; j < length; j++) {
                if (heights[j] >= cur)
                    sum += cur;
                else {
                    break;
                }
            }

            for (int k = i - 1; k >= 0; k--) {
                if (heights[k] >= cur)
                    sum += cur;
                else
                    break;
            }

            max = Math.Max (max, sum);

        }
        return max;
    }

    /* #endregion */

    /* #region   Divide and Conquer*/

    //iteration 
    // o(n2) time
    public int LargestRectangleArea (int[] heights) {
        return DC (heights, 0, heights.Length - 1);
    }

    int DC (int[] heights, int start, int end) {
        if (start == end) return heights[start];
        if (start > end) return 0;

        int min = FindMin (heights, start, end);
        return Math.Max (Math.Max (DC (heights, start, min - 1), DC (heights, min + 1, end)), (end - start + 1) * heights[min]);
    }

    int FindMin (int[] heights, int start, int end) {
        int min = Int32.MaxValue;
        int index = start;
        for (int i = start; i <= end; i++) {
            if (min > heights[i]) {
                index = i;
                min = heights[i];
            }
        }
        return index;
    }

    // Iteration optimized
    // Time Complexity: O(n); 
    // Space Complexity: O(n2);
    public int LargestRectangleArea (int[] heights) {
        int length = heights.Length;
        if (length == 0) return 0;
        int[] lessFromLeft = new int[length];
        int[] lessFromRight = new int[length];

        lessFromLeft[0] = -1;
        for (int i = 1; i < length; i++) {
            int prev = i - 1;
            while (prev >= 0 && heights[prev] >= heights[i]) {
                prev = lessFromLeft[prev];
            }
            lessFromLeft[i] = prev;
        }

        lessFromRight[length - 1] = length;
        for (int i = length - 2; i >= 0; i--) {
            int prev = i + 1;
            while (prev < length && heights[prev] >= heights[i]) {
                prev = lessFromRight[prev];
            }
            lessFromRight[i] = prev;
        }

        int max = 0;
        for (int i = 0; i < length; i++) {
            max = Math.Max (max, heights[i] * (lessFromRight[i] - lessFromLeft[i] - 1));
        }

        return max;
    }

    // https: //www.geeksforgeeks.org/largest-rectangle-under-histogram/
    // Iteration optimized
    // Time Complexity: O(n); 
    // Space Complexity: O(n);
    public int LargestRectangleArea (int[] heights) {
        var stack = new Stack<int> ();
        int i = 0, areaMax = 0;
        while (i < heights.Length) {
            if (stack.Count == 0 || heights[stack.Peek ()] <= heights[i]) {
                stack.Push (i++);
            } else {
                int top = stack.Pop ();
                int areaTop = heights[top] * (stack.Count == 0 ? i : i - stack.Peek () - 1);
                areaMax = Math.Max (areaMax, areaTop);

            }
        }

        while (stack.Count > 0) {
            int top = stack.Pop ();
            int areaTop = heights[top] * (stack.Count == 0 ? i : i - stack.Peek () - 1);
            areaMax = Math.Max (areaMax, areaTop);
        }
        return areaMax;
    }

    /* #endregion */

}