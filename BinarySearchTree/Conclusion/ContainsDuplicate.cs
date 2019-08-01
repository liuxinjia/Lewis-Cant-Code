public class Solution {
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode (int _val) { val = _val; }
    }
    TreeNode rootNode = null;

    public bool ContainsNearbyAlmostDuplicate (int[] nums, int k, int t) {
        if (k < 1 || t < 0) return false;

        for (int i = 0; i < nums.Length; i++) {
            if (i - 1 >= k) DeleteNode (nums[Math.Abs (k - i + 1)]);
            // if (SearchNode (AddNode (nums[i]), t)) return true;
            if (AddNeighbour (nums[i])) return true;
            if (SearchNeighbour (nums[i], t)) return true;
        }
        return false;
    }

    void DeleteNode (int val) {
        if (rootNode == null) return;

        var prev = FindMin (out var cur, val);

        if (prev == null) rootNode = DeleteRootNode (cur);
        else if (prev.val > val) prev.left = DeleteRootNode (cur);
        else prev.right = DeleteRootNode (cur);
    }

    TreeNode DeleteRootNode (TreeNode root) {
        if (root.left == null) return root.right;
        if (root.right == null) return root.left;

        var cur = root.right;
        TreeNode prev = null;
        while (cur.left != null) {
            prev = cur;
            cur = cur.left;
        }

        cur.left = root.left;
        if (cur != root.right) {
            prev.left = cur.right;
            cur.right = root.right;
        }
        return cur;
    }

    TreeNode AddNode (int val) {
        var node = new TreeNode (val);
        if (rootNode == null) {
            rootNode = node;
            return null;
        }

        var prev = FindMin (out var cur, val);

        if (cur != null && cur.val == val) {
            node.left = node;
            return node;
        }
        if (prev.val > val) prev.left = node;
        else if (prev.val < val) prev.right = node;

        return prev;
    }

    bool SearchNode (TreeNode prev, int t) {
        if (prev == null) return false;

        bool left = false, right = false;
        if (prev.left != null) {
            left = Math.Abs ((long) prev.val - (long) prev.left.val) <= t;

            if (left) return true;

            var lastPrev = FindMin (out var cur, prev.val);
            if (lastPrev != null)
                left |= Math.Abs ((long) lastPrev.val - (long) prev.left.val) <= t;
        }

        if (prev.right != null && !left) {
            right = Math.Abs ((long) prev.val - (long) prev.right.val) <= t;

            if (right) return true;

            var lastPrev = FindMin (out var cur, prev.val);
            if (lastPrev != null)
                right |= Math.Abs ((long) lastPrev.val - (long) prev.right.val) <= t;
        }

        return left | right;
    }

    bool AddNeighbour (int val) {
        var node = new TreeNode (val);
        if (rootNode == null) {
            rootNode = node;
            return false;
        }

        var prev = FindMin (out var cur, val);

        if (cur != null && cur.val == val) {
            return true;
        }
        if (prev.val > val) prev.left = node;
        else if (prev.val < val) prev.right = node;

        return false;
    }
    bool SearchNeighbour (int val, int t) {
        TreeNode cur = rootNode, Max = null, Caroline = null;
        int i = 0;

        while (cur != null && cur.val != val) {
            if (i == 0) { Max = cur; i++; } else if (i == 1) { Caroline = cur; i = 0; }

            if (cur.val > val) cur = cur.left;
            else if (cur.val < val) cur = cur.right;
        }

        if (cur == rootNode) return false;
        if (Caroline == null) return Math.Abs ((long) Max.val - (long) val) <= t;

        // assume cur has no children, else, ..., it will be more complicated
        return Math.Abs ((long) Max.val - (long) val) <= t |
            Math.Abs ((long) Caroline.val - (long) val) <= t;
    }

    TreeNode FindMin (out TreeNode cur, int val) {
        cur = rootNode;
        TreeNode prev = null;

        while (cur != null && cur.val != val) {
            prev = cur;
            if (cur.val > val) cur = cur.left;
            else if (cur.val < val) cur = cur.right;
        }

        return prev;
    }

}