public class Solution {
    public bool IsUgly (int num) {
        while (num >= 2) {
            if (num % 2 == 0) {
                num = num / 2;
            } else if (num % 3 == 0) {
                num = num / 3;
            } else if (num % 5 == 0) {
                num /= 5;
            } else
                return false;
        }

        return num == 1;
    }

    // if I choosed the min one , it will slower
    //of course it will, just image 30 ( 2*3*5)
    public bool IsUgly (int num) {
        while (num >= 2) {
            if (num % 2 == 0) {
                num /= 2;
                if (num % 3 == 0) {
                    num = num / 3;
                    if (num % 5 == 0) {
                        num /= 5;
                    }
                }
            } else if (num % 3 == 0) {
                num = num / 3;
                if (num % 5 == 0) {
                    num /= 5;
                }
            } else if (num % 5 == 0) {
                num /= 5;
            } else
                return false;
        }

        return num == 1;
    }

    public bool IsUgly (int num) {
        for (int i = 2; i < 6 && num > 0; i++) {
            while (num % i == 0) {
                num /= i;
            }
        }
        return num == 1;
    }
}