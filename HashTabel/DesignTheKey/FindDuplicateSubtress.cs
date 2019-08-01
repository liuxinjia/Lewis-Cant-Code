/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
using System.Collections.Generic;

public class Solution {
    public IList<TreeNode> FindDuplicateSubtrees (TreeNode root) {
        var rList = new List<TreeNode> ();
        if (root == null) return rList;

        var map = new Dictionary<string, int> ();
        PostOrder (root, map, rList);

        return rList;
    }

    string PostOrder (TreeNode root, Dictionary<string, int> map, List<TreeNode> rList) {
        if (root == null) return "$";

        string str = root.val + PostOrder (root.left, map, rList) + "," + PostOrder (root.right, map, rList) + ",";
        if (!map.TryAdd (str, 1) && map[str] == 1) {
            map[str]--;
            rList.Add (root);
        }

        return str;
    }

}