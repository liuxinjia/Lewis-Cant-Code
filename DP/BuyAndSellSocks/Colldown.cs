public class Solution {
    public int MaxProfit (int[] prices) {
        if (prices.Length < 1) return 0;

        // int sell = 0, buy = -prices[0], colldown = -1;
        int size = prices.Length;
        int[] sell = new int[size];
        int[] buy = new int[size];
        int[] colldown = new int[size];
        sell[0] = 0;
        buy[0] = -prices[0];
        colldown[0] = -1;
        for (int i = 1; i < size; i++) {
            sell[i] = Math.Max (sell[i - 1], colldown[i - 1]);
            buy[i] = Math.Max (buy[i - 1], sell[i - 1] - prices[i]);
            colldown[i] = buy[i - 1] + prices[i];

        }

        return Math.Max (colldown[size - 1], sell[size - 1]);
    }

    public int MaxProfit (int[] prices) {
        if (prices.Length < 1) return 0;

        int sell = 0, buy = -prices[0], colldown = -1;
        int prevSell = sell, prevBuy = buy, prevColldown = colldown;
        for (int i = 1; i < prices.Length; i++) {
            sell = Math.Max (prevSell, prevColldown);
            buy = Math.Max (prevBuy, prevBuy - prices[i]);
            colldown = prevBuy + prices[i];

            prevSell = sell; prevBuy = buy; prevColldown = colldown;
        }

        return Math.Max (sell, colldown);
    }
}