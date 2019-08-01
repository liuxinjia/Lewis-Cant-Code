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
using System.Text;

public class Codec {

    // Encodes a tree to a single string.
    public string serialize (TreeNode root) {
        if (root == null) return null;

        List<StringBuilder> strList = new List<StringBuilder> ();
        HelperSerialize (root, strList, 0);

        string treeInfo = null;
        foreach (var item in strList) {
            treeInfo += item.ToString ();
        }
        return treeInfo;

    }

    void HelperSerialize (TreeNode root, List<StringBuilder> strList, int depth) {

        if (depth >= strList.Count) strList.Add (new StringBuilder ());
        if (root == null) {
            strList[depth].Append ("null,");
            return;
        }

        strList[depth].Append (root.val.ToString () + ',');

        HelperSerialize (root.left, strList, depth + 1);
        HelperSerialize (root.right, strList, depth + 1);
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize (string data) {
        if (data == null) return null;

        string[] trees = data.Split (',');
        Queue<TreeNode> queue = new Queue<TreeNode> ();

        var root = new TreeNode (Int32.Parse (trees[0]));
        int i = 1;
        queue.Enqueue (root);

        while (queue.Count > 0) {
            var curNode = queue.Dequeue ();

            // if (trees[i] == "") continue;

            if (trees[i] != "null") {
                if (Int32.TryParse (trees[i], out int num)) {
                    curNode.left = new TreeNode (num);
                    queue.Enqueue (curNode.left);
                }
            } else curNode.left = null;

            if (trees[++i] != "null") {
                if (Int32.TryParse (trees[i], out int num)) {
                    curNode.right = new TreeNode (num);
                    queue.Enqueue (curNode.right);
                }
            } else curNode.right = null;
            i++;
        }

        return root;
    }
}

// Your Codec object will be instantiated and called as such:
// Codec codec = new Codec();
// codec.deserialize(codec.serialize(root));

//failed solution 01
//BFS——Level Traverse
// ------------
public class Codec {

    // Encodes a tree to a single string.
    public string serialize (TreeNode root) {
        if (root == null) return null;

        StringBuilder treeInfo = new StringBuilder ();
        Queue<TreeNode> queue = new Queue<TreeNode> ();
        queue.Enqueue (root);

        while (queue.Count > 0) {
            var node = queue.Dequeue ();
            if (node != null) {
                treeInfo.Append (node.val);
                queue.Enqueue (node.left);
                queue.Enqueue (node.right);
            } else {
                treeInfo.Append ("null");
            }
            treeInfo.Append (",");
        }

        return treeInfo.ToString ();
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize (string data) {
        if (data == null) return null;

        string[] trees = data.Split (',');
        Queue<TreeNode> queue = new Queue<TreeNode> ();

        var root = new TreeNode (Int32.Parse (trees[0]));
        queue.Enqueue (root);

        for (int i = 1; i < trees.Length - 1; i++) {
            var curNode = queue.Dequeue ();

            if (trees[i] != "null") {
                curNode.left = new TreeNode (Int32.Parse (trees[i]));
                queue.Enqueue (curNode.left);
            }

            if (trees[++i] != "null") {
                curNode.right = new TreeNode (Int32.Parse (trees[i]));
                queue.Enqueue (curNode.right);
            }

        }

        return root;
    }
}

// Your Codec object will be instantiated and called as such:
// Codec codec = new Codec();
// codec.deserialize(codec.serialize(root));

//preorder traversal

/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Codec {

    // Encodes a tree to a single string.
    public string serialize (TreeNode root) {
        StringBuilder sb = new StringBuilder ();
        HelperSerialize (root, sb);

        return sb.ToString ();
    }

    void HelperSerialize (TreeNode root, StringBuilder sb) {
        if (root == null) {
            sb.Append ("null,");
            return;
        }

        sb.Append (root.val.ToString () + ",");
        HelperSerialize (root.left, sb);
        HelperSerialize (root.right, sb);
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize (string data) {

        string[] trees = data.Split (",");

        var queue = new Queue<string> ();
        for (int i = 0; i < trees.Length; i++) {
            queue.Enqueue (trees[i]);
        }

        return HelperDeserialize (queue);
    }
    
    TreeNode HelperDeserialize (Queue<string> queue) {
        var str = queue.Dequeue ();
        if (str == "null") {
            return null;
        }

        var node = new TreeNode (Int32.Parse (str));
        node.left = HelperDeserialize (queue);
        node.right = HelperDeserialize (queue);

        return node;
    }
}

// Your Codec object will be instantiated and called as such:
// Codec codec = new Codec();
// codec.deserialize(codec.serialize(root));