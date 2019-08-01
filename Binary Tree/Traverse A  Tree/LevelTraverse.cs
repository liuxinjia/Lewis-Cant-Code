using System.Collections.Generic;
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
    //BFS with queue
    public IList<IList<int>> LevelOrder (TreeNode root) {
        // List<List<int>> rlist = new List<List<int>> ();
        List<IList<int>> rlist = new List<IList<int>> ();

        if (root == null) return rlist;

        Queue<TreeNode> queue = new Queue<TreeNode> ();
        queue.Enqueue (root);

        while (queue.Count > 0) {
            var list = new List<int> ();
            int length = queue.Count;
            for (int i = 0; i < length; i++) {
                var node = queue.Dequeue ();
                if (node.left != null) {
                    queue.Enqueue (node.left);
                }
                if (node.right != null) {
                    queue.Enqueue (node.right);
                }
                list.Add (node.val);
            }
            rlist.Add (list);
        }

        return rlist;
    }

    //DFS with iteration

    public IList<IList<int>> LevelOrder (TreeNode root) {
        var rlist = new List<IList<int>> ();

        HelperLevelOrder (root, 0, rlist);
        return rlist;
    }

    void HelperLevelOrder (TreeNode root, int depth, List<IList<int>> list) {
        if (root == null) return ;

        if (depth >= list.Count) list.Add (new List<int> ());

        list[depth].Add (root.val);

        HelperLevelOrder (root.left, depth + 1, list);
        HelperLevelOrder (root.right, depth + 1, list);

    }
}