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
    public IList<TreeNode> GenerateTrees (int n) {
        if (n == 0) return new List<TreeNode> ();
        var linkList = UniqueTreeMap (1, n, new Dictionary<string, LinkedList<TreeNode>> ());
        return new List<TreeNode> (linkList);
        return UniqueTree (1, n);
    }

    List<TreeNode> UniqueTree (int start, int end) {
        List<TreeNode> childList = new LinkedList<TreeNode> ();
        if (start > end) {
            childList.Add (null);
            return childList;
        }

        for (int i = start; i <= end; i++) {
            var leftSubTree = UniqueTree (start, i - 1);
            var rightSubTree = UniqueTree (i + 1, end);
            foreach (var left in leftSubTree) {
                foreach (var right in rightSubTree) {
                    var node = new TreeNode (i);
                    node.left = left;
                    node.right = right;
                    childList.Add (node);
                }
            }
        }

        return childList;

    }

    //failed - linkedlist;

    LinkedList<TreeNode> UniqueTreeMap (int start, int end, Dictionary<string, LinkedList<TreeNode>> map) {
        LinkedList<TreeNode> list = new LinkedList<TreeNode> ();

        if (start > end) {
            // list.AddLast (null);
            return list;
        }
        string treeInfo = TreeInfoFormat (start, end);
        if (map.ContainsKey (treeInfo)) {
            list.AddLast (map[treeInfo]);
            return list;
        }

        for (int i = start; i <= end; i++) {
            var leftSubTree = UniqueTreeMap (start, i - 1, map);
            var rightSubTree = UniqueTreeMap (i + 1, end, map);

            foreach (var left in leftSubTree) {
                foreach (var right in rightSubTree) {
                    var root = new TreeNode (i);
                    root.right = right;
                    root.left = left;
                    list.AddLast (root);
                }
            }
        }

        map.Add (treeInfo, list);
        return list;
    }

    List<TreeNode> UniqueTreeMap (int start, int end, Dictionary<string, List<TreeNode>> map) {
        List<TreeNode> list = new List<TreeNode> ();

        string treeInfo = TreeInfoFormat (start, end);
        if (map.ContainsKey (treeInfo)) {
            list.AddRange (map[treeInfo]);
            return list;
        }

        if (start > end) {
            list.Add (null);
            return list;
        }

        for (int i = start; i <= end; i++) {
            var leftSubTree = UniqueTreeMap (start, i - 1, map);
            var rightSubTree = UniqueTreeMap (i + 1, end, map);

            foreach (var left in leftSubTree) {
                foreach (var right in rightSubTree) {
                    var root = new TreeNode (i);
                    root.right = right;
                    root.left = left;
                    list.Add (root);
                }
            }
        }

        map.Add (TreeInfoFormat (start, end), list);
        return list;
    }

    string TreeInfoFormat (int i, int j) {
        return i + "," + j;
    }
}