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
    int curIndex = 0;

    public TreeNode ConstructFromPrePost (int[] pre, int[] post) {
        int preLength = pre.Length;
        int postLength = post.Length;

        if (preLength == 0 || postLength == 0 || preLength != postLength) return null;
        curIndex = 0;
        return HelperConstruct (pre, post, 0, postLength - 1);

    }

    TreeNode HelperConstruct (int[] pre, int[] post, int start, int end) {
        if (start > end) return null;

        var node = new TreeNode (pre[curIndex]);
        curIndex++;

        if (start == end) return node;
        int index = SearchIndex (post, start, end, pre[curIndex]);

        node.left = HelperConstruct (pre, post, start, index);
        node.right = HelperConstruct (pre, post, index + 1, end - 1);

        return node;

    }

    int SearchIndex (int[] order, int start, int end, int val) {
        int i;
        for (i = start; i <= end; i++) {
            if (val == order[i]) break;
        }

        return i;
    }
}