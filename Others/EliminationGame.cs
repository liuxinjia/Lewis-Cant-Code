public class Solution {

    pseudcode
    public int LastRemaining (int n) {
        if (n == 0) return 0;

        List<int> list = new List<int> ();
        for (int i = 1; i <= n; i++) {
            list.Add (i);
        }

        int end = n - 1, start = 0;
        int sign = 1, index = 0;

        while (list.Count != 1) {
            list.RemoveAt (index);
            index += sign;
            int length = list.Count;

            if (index >= length || index < 0) {
                if (start > end) {
                    start = length - 1;
                    sign = 1;
                } else {
                    end = length - 1;
                    sign = -2;
                }

                var temp = start;
                start = end;
                end = temp;

                index = start;
            }
        }

        return list[0];
    }

    // According fromhttps: https://leetcode.com/problems/elimination-game/discuss/87119/JAVA%3A-Easiest-solution-O(logN)-with-explanation
    // thanks NathanNi, it's an really awesome solution
    public int LastRemaining (int n) {

        if (n == 0) return 0;
        bool left = true;
        int remaining = n;
        int step = 1;
        int head = 1;

        while (remaining > 1) {
            //eliminate value when moving in left direction and there left and odd number of elements

            if (left || remaining % 2 == 1) {
                head += step;
            }
            //Yeah!!! It's the trickest next value part of the solution
            // Start with 1 and double each iteration so that you can calculating the number of steps to reach the next available number
            step *= 2;
            remaining /= 2;
            left = !left;
        }

        return head;
    }

    //recursive solution
    // ML(1...n) + MR(1...n) = 1 + n
    public int LastRemaining (int n) {
        return n == 1 ? 1 : 2*(1+ n/2 - LastRemaining(n/2));
    }
}