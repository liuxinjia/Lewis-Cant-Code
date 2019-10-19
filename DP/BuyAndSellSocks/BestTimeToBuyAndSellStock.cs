public class Solution {
    public int MaxProfit (int[] prices) {
        int i = 0;
        int profit = 0;
        while (i < prices.Length) {
            while (i < prices.Length - 1 && prices[i] >= prices[i + 1]) {
                i++;
            }
            int min = prices[i++];
            while (i < prices.Length - 1 && prices[i] <= prices[i + 1]) {
                i++;
            }
            profit += i < prices.Length ? prices[i] - min : 0;
        }
        return profit;
    }

    public int MaxProfit (int[] prices) {
        int profit = 0;
        for (int i = 1; i < prices.Length; i++) {
            if (prices[i - 1] < prices[i])
                profit += prices[i] - prices[i - 1];
        }
        return profit;
    }

}