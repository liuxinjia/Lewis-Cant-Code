public class Solution {

    //Recursion
    // TLR: Don't use that shit again
    public int NumTrees (int n) {
        int r = 0;
        for (int i = 1; i <= n; i++) {
            r += NumTrees (i - 1) * NumTrees (n - i);
        }
        return n < 2 ? 1 : r;
    }

    // Cantalan tree
    // https: //leetcode.com/problems/unique-binary-search-trees/discuss/31671/A-very-simple-and-straight-ans-based-on-MathCatalan-Number-O(N)-timesO(1)space
    public int NumTrees (int n) {
        long int ans = 1;
        for (int i = n + 1; i <= 2 * n; i++) {
            ans = ans * i / (i - n);
        }
        return ans / (n + 1);
    }

    //more intuitive to the formula
    //Time complexity: O(n2); Space Complexity:(On2);
    public int NumTrees (int n) {
        if (n < 2) return 1;

        int[] G = new int[n + 1];
        G[0] = 1;
        G[1] = 1;
        G[2] = 2;
        for (int i = 3; i <= n; i++) {
            int r = 0;
            for (int j = 1; j <= i; j++) {
                r += G[j - 1] * G[i - j];
            }
            G[i] = r;
        }

        return G[n];
    }

    public int NumTrees (int n) {
        if (n < 1) return 1;

        var Catalana = new List<int> ();
        Catalana.Add (1);
        Catalana.Add (1);

        for (int i = 1; i < n; i++) {
            int r = 0;
            for (int j = 0; j <= i; j++) {
                r += Catalana[j] * Catalana[i - j];
            }
            Catalana.Add (r);
        }

        return Catalana[n];
    }

    public int NumTrees (int n) {
        if (n < 1) return 1;

        var Catalana = new SortedDictionary<int, int> ();
        Catalana.TryAdd (0, 1);
        Catalana.TryAdd (1, 1);

        for (int i = 2; i <= n; i++) {
            int r = 0;
            for (int j = 1; j <= i; j++) {
                r += Catalana[j - 1] * Catalana[i - j];
            }
            Catalana.TryAdd (i, r);
        }

        return Catalana[n];
    }

}