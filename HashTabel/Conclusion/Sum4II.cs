using System.Collections.Generic;
public class Solution {
    public int FourSumCount (int[] A, int[] B, int[] C, int[] D) {
        var map = new Dictionary<int, int> ();
        for (int i = 0; i < A.Length; i++) {
            for (int j = 0; j < B.Length; j++) {
                int item = A[i] + B[j];
                if (!map.TryAdd (item, 1)) map[item]++;
            }
        }

        int count = 0;
        for (int i = 0; i < C.Length; i++) {
            for (int j = 0; j < D.Length; j++) {
                int item = (C[i] + D[j]) * -1;
                if (map.ContainsKey (item)) count += map[item];
            }
        }

        return count;
    }

}