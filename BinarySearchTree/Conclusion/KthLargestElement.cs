public class KthLargest {
    public class TreeNode {
        public int val;
        public int count;
        public TreeNode left;
        public TreeNode right;
        public TreeNode (int _val) { val = _val; count = 0; }
    }

    TreeNode rootNode = null;
    int kth = 0;

    public KthLargest (int k, int[] nums) {
        kth = k;
        for (int i = 0; i < nums.Length; i++)
            InsertNode (nums[i]);

    }

    public int Add (int val) {
        InsertNode (val);
        return SearchNode ();
    }

    void InsertNode (int val) {
        var node = new TreeNode (val);
        node.count = 1;
        if (rootNode == null) { rootNode = node; return; }

        var cur = rootNode;
        TreeNode prev = null;
        while (cur != null) {
            prev = cur;
            cur.count += 1;
            cur = val < cur.val? cur.left : cur.right;
        }

        if (prev.val > val) prev.left = node;
        else prev.right = node;
    }

    int SearchNode () {
        var cur = rootNode;
        int k = kth;

        while (k > 0) {
            int pos = 1 + (cur.right != null ? cur.right.count : 0);
            if (pos == k) break;
            if (pos > k) cur = cur.right;
            else {
                k = k - pos;
                cur = cur.left;
            }
        }

        return cur.val;
    }
}
/**
 * Your KthLargest object will be instantiated and called as such:
 * KthLargest obj = new KthLargest(k, nums);
 * int param_1 = obj.Add(val);
 */