public class Solution {
    public IList<IList<int>> Combine (int n, int k) {
        var rLists = new List<IList<int>> ();
        Solver (n, k, 1, rLists, new HashSet<int> ());
        return rLists;
    }

    void Solver (int n, int k, int i, List<IList<int>> rLists, HashSet<int> set) {
        if (k == 0) {
            rLists.Add (set.ToList ());
            return;
        }
        if (n - i + 1 < k) return;

        for (; i <= n; i++) {
            //use linkedlist instead, set costs space
            if (!set.Contains (i)) {
                set.Add (i);
                Solver (n, k - 1, i + 1, rLists, set);
                set.Remove (i);
            }
        }
    }
}