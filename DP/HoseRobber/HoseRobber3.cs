/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    public int Rob (TreeNode root) {
        int[] sub = RobSub (root);
        return Math.Max (sub[0], sub[1]);
        return Rob (root, new Dictionary<TreeNode, int> ());
    }

    int Rob (TreeNode root, Dictionary<TreeNode, int> map) {
        if (root == null) return 0;
        if (map.ContainsKey (root)) return map[root];

        int left = 0;
        if (root.left != null) {
            left = Rob (root.left.right, map) + Rob (root.left.left, map);
        }
        // if ( root.left != null && !map.TryAdd (root.left, left)) map[root.left] = left;

        int right = 0;
        if (root.right != null) {
            right = Rob (root.right.left, map) + Rob (root.right.right, map);
        }
        // if (root.right != null && !map.TryAdd (root.right, right)) map[root.right] = right;

        int max = Math.Max (left + right + root.val, Rob (root.right, map) + Rob (root.left, map));
        map.Add (root, max);
        return max;
    }

    int[] RobSub (TreeNode root) {
        int[] res = new int[2];
        if (root == null) return res;

        var left = RobSub (root.left);
        var right = RobSub (root.right);

        res[0] = Math.Max (left[0], left[1]) + Math.Max (right[0], right[1]);
        res[1] = left[0] + right[0] + root.val;
        return res;
    }
}