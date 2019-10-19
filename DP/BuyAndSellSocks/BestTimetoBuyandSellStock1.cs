public class Solution {
    public int MaxProfit (int[] prices) {
        //Divide and Conquer
        //Dont' need to do that
        // it cost 
        return GetMaxProfit (prices, 0, prices.Length - 1);

        //straight forward resolution
        int min = Int32.MaxValue, profit = 0;
        for (int i = 0; i < prices.Length; i++) {
            if (prices[i] < min) {
                min = prices[i];
            } else {
                profit = Math.Max (profit, prices[i] - min);
            }
        }

        //Kandana algorithm
        //avoid twist the array  witht minus numbers
        int maxCur = 0;
        int maxSoFar = 0;
        for(int i=1; i<prices.Length; i++){
            maxCur += Math.Max(0, prices[i] - prices[i-1]);
            maxSoFar = Math.Max(maxSoFar, maxCur);
        }
        return maxSoFar;
        return min;
    }

    int GetMaxProfit (int[] prices, int start, int end) {
        if (start >= end) return 0;
        FinMinAndMax (out int min, out int max, prices, start, end);
        if (min < max) return prices[max] - prices[min];
        //should avoid 
        if (min == max) return 0;
        int temp = Math.Max (GetMaxProfit (prices, start, max), GetMaxProfit (prices, min, end));
        return Math.Max (GetMaxProfit (prices, max + 1, min - 1), temp);
    }

    void FinMinAndMax (out int minIndex, out int maxIndex, int[] prices, int start, int end) {
        int min = prices[start];
        int max = min;
        minIndex = maxIndex = start;
        for (; start <= end; start++) {
            if (max < prices[start]) {
                max = prices[start];
                maxIndex = start;
            } else if (min > prices[start]) {
                min = prices[start];
                minIndex = start;
            }
        }
    }
}