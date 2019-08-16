public class Solution {

    // Failed: Iterative solution
    public int KthGrammar (int N, int K) {
        var list = new List<int> ();
        if (N == 0) return 0;
        list.Add (0);

        var SList1 = new List<int> () { 1, 0 };
        var SList0 = new List<int> () { 0, 1 };

        for (int n = 1; n < N; n++) {
            var tList = new List<int> ((int) Math.Pow (2, n - 1));
            foreach (var item in list) {
                tList.AddRange (item == 0 ? SList0 : SList1);
            }
            list = tList;
        }

        return list[K - 1];

    }

    //Solution
    public class Solution {
        public int KthGrammar (int N, int K) {
            if (N == 1) return 0;

            //the same as  ^
            // if (K % 2 == 0) return (KthGrammar (N - 1, K / 2) == 0 ? 1 : 0);
            // else return (KthGrammar (N - 1, (K + 1) / 2) == 0 ? 0 : 1);
            if (K % 2 == 0) return (KthGrammar (N - 1, K / 2) ^ 1);
            else return (KthGrammar (N - 1, (K + 1) / 2) ^ 0);
        }

    }

}