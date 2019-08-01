using System.Collections.Generic;

public class Solution {
    public bool IsHappy (int n) {
        int rabbit = n, turtle = n;
        do {
            rabbit = SumSquareDigit (rabbit);
            if (rabbit == 1) return true;
            rabbit = SumSquareDigit (rabbit);
            turtle = SumSquareDigit (turtle);
        } while (rabbit != turtle);

        return rabbit == 1;
    }

    int SumSquareDigit (int i) {
        int result = 0;
        while (i > 0) {
            int temp = i % 10;
            result += temp * temp;
            i /= 10;
        }
        return result;
    }

    public bool IsHappy (int n) {
        HashSet<int> set = new HashSet<int> ();

        while (set.Add (n)) {
            if (n == 1) return true;
            n = SumSquareDigit (n);
        }

        return false;
    }
}