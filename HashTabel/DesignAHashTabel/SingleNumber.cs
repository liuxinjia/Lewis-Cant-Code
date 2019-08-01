using System.Collections.Generic;

public class Solution {
    public int SingleNumber (int[] nums) {
        HashSet<int> set = new HashSet<int> ();

        foreach (var item in nums) {
            if (!set.Add (item)) set.Remove (item);
        }

        var em = set.GetEnumerator ();
        if (em.MoveNext ()) return em.Current;
        return 0;

        //XOR
        var result = 0;
        foreach (var item in nums) {
            result ^= item;
        }
        return result;
    }

    public int SingleNumber (int[] nums) {
        HashSet<int> set1 = new HashSet<int> ();
        HashSet<int> set2 = new HashSet<int> ();

        foreach (var item in nums) {
            if (set1.Contains (item)) {
                set2.Add (item);
            } else
                set1.Add (item);
        }

        foreach (var item in set1) {
            if (!set2.Contains (item)) return item;
        }

        return 0;
    }
}