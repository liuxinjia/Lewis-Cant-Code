public class Solution {

    //Iteration 
    public IList<string> GenerateParenthesis (int n) {
        var sb = new StringBuilder (6);
        var stack = new Stack<char> ();
        stack.Push (')');
        sb[0] = '(';

    }

    public IList<string> GenerateParenthesis (int n) {
        var rList = new List<string> ();
        Solver (new StringBuilder (6), rList, n, 0, 0, 0);
        return rList;
    }
    //recursion 01:
    void Solver (StringBuilder stringBuilder, List<string> rList, int n, int i, int open, int left) {
        if (i == n * 2) {
            rList.Add (stringBuilder.ToString ());
            return;
        }

        if (open < n) {
            if (stringBuilder.Length < i + 1)
                stringBuilder.Insert (i, '(');
            // else stringBuilder[i] = '(';
            Solver (stringBuilder, rList, n, i + 1, open + 1, left);
            // the same as stringBuilder[i] = ')';
            // because stringBuilder is a class
            stringBuilder.Remove (i, 1);
        }

        if (left < open) {
            if (stringBuilder.Length < i + 1)
                stringBuilder.Insert (i, ')');
            // else stringBuilder[i] = ')';
            Solver (stringBuilder, rList, n, i + 1, open, left + 1);
            stringBuilder.Remove (i, 1);
        }

    }

    // recursion 03
    public IList<string> GenerateParenthesis (int n) {
        var ans = new List<string> ();

        if (n == 0) ans.Add ("");
        else {
            for (int c = 0; c < n; c++) {
                foreach (var open in GenerateParenthesis (c)) {
                    foreach (var close in GenerateParenthesis (n - c - 1))
                        ans.Add ("(" + open + ")" + close);
                }
            }
        }
        return ans;
    }

    public IList<string> GenerateParenthesis (int n) {
        var ans = new List<List<string>> ();

        for (int i = 0; i <= n; i++) {
            var list = new List<string> ();
            for (int j = 0; j < i; j++) {
                foreach (var open in ans[j]) {
                    foreach (var close in ans[i - j - 1]) {
                        list.Add ("(" + open + ")" + close);
                    }
                }
            }
            if(list.Count == 0) list.Add("");
            ans.Add (list);
        }

        return ans[n - 1];
    }

}