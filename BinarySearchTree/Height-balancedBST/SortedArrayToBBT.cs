using System.Collections.Generic;

public class Solution {
    public TreeNode SortedArrayToBST (int[] nums) {
        if (nums.Length < 1) return null;
        int start = 0;
        int end = nums.Length - 1;
        var indexStack = new Stack<Tuple<int, int>> ();
        var nodeStack = new Stack<TreeNode> ();

        indexStack.Push (new Tuple<int, int> (start, end));
        TreeNode root = new TreeNode (0);
        nodeStack.Push (root);

        while (nodeStack.Count > 0) {
            var index = indexStack.Pop ();
            start = index.Item1;
            end = index.Item2;
            int mid = (end + start) / 2;

            var cur = nodeStack.Pop ();
            cur.val = nums[mid];

            if (mid > start) {
                cur.left = new TreeNode (0);
                nodeStack.Push (cur.left);
                indexStack.Push (new Tuple<int, int> (start, mid - 1));
            }
            if (mid < end) {
                cur.right = new TreeNode (0);
                nodeStack.Push (cur.right);
                indexStack.Push (new Tuple<int, int> (mid + 1, end));
            }
        }

        return root;

        return HelperConstruct (nums, 0, nums.Length - 1);
    }

    //DFS
    public TreeNode SortedArrayToBST (int[] nums) {
        if (nums.Length == 0) return null;

        var nodeQueue = new Queue<TreeNode> ();
        var root = new TreeNode (0);
        nodeQueue.Enqueue (root);
        var indexQueue = new Queue<Tuple<int, int>> ();
        indexQueue.Enqueue (new Tuple<int, int> (0, nums.Length - 1));

        while (nodeQueue.Count > 0) {
            int level = nodeQueue.Count;
            for (int i = 0; i < level; i++) {
                var node = nodeQueue.Dequeue ();
                var index = indexQueue.Dequeue ();
                int start = index.Item1, end = index.Item2;
                int mid = (start + end) / 2;
                node.val = nums[mid];
                if (mid > start) {
                    node.left = new TreeNode (0);
                    nodeQueue.Enqueue (node.left);
                    indexQueue.Enqueue (new Tuple<int, int> (start, mid - 1));
                }
                if (mid < end) {
                    node.right = new TreeNode (0);
                    nodeQueue.Enqueue (node.right);
                    indexQueue.Enqueue (new Tuple<int, int> (mid + 1, end));
                }
            }
        }

        return root;
    }

    //Recursion
    TreeNode HelperConstruct (int[] nums, int start, int end) {
        if (start > end) return null;

        int mid = (start + end) / 2;
        var root = new TreeNode (nums[mid]);
        root.left = HelperConstruct (nums, start, mid - 1);
        root.right = HelperConstruct (nums, mid + 1, end);

        return root;
    }

}