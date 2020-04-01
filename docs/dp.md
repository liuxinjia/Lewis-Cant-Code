## DP

###  the fewest (or largest) of combinations
![enter image description here](https://leetcode.com/media/original_images/322_coin_change_table.png)

``` og
    int coinChange(vector<int>& coins, int amount) {
        std::vector<int> dp(amount+1, amount+1);
        dp[0] = 0;

        for(int i=1; i<=amount; i++){
            for(int coin : coins){
                if(coin <= i)
                    dp[i] = std::min(dp[i], dp[i-coin]+1);
            }
        }
        return dp[amount] == amount+1 ? -1 : dp[amount];
        
    }
```

### the number of combinations
https://www.youtube.com/watch?v=xCbYmUPvc2Q
``` og
class Solution {
public:
    int change(int amount, vector<int>& coins) {
        std::vector<int> dp(amount+1);
        dp[0] = 1;
        
        for(int coin : coins){
            for(int i=coin; i<=amount; i++){
                dp[i] += dp[i-coin];
            }
        }
        return dp[amount];
    }
};
```



