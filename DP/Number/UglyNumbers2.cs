public class Solution {
    HashSet set = new HashSet<int> (index);

    public int NthUglyNumber (int index) {
        int i = 1, count = 1;
        while (count < index) {
            int num = ++i;
            num = MaxDivide (num, 2);
            num = MaxDivide (num, 3);
            num = MaxDivide (num, 5);

            if (num == 1) { count++; set.Add (i); }
        }
        return i;
    }

    int MaxDivide (int a, int b) {
        while (a % b == 0) {
            if (set.Contains (a)) return 1;
            a /= b;
        }
        return a;
    }

    public int NthUglyNumber (int n) {
        int[] num = new int[n];
        num[0] = 1;
        int aIndex = 0, bIndex = 0, cIndex = 0;
        int min = 1;
        for (int i = 1; i < n; i++) {
            int v = num[aIndex] * 2, v1 = num[bIndex] * 3, v2 = num[cIndex] * 5;
            min = Math.Min (v, Math.Min (v1, v2));
            num[i] = min;
            if (min == v) {
                aIndex++;
            }
            if (min == v1) {
                bIndex++;
            }
            if (min == v2) {
                cIndex++;
            }

        }
        return min;
    }
}

