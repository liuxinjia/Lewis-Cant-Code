/*
// Definition for a Node.
public class Node {
    public int val;
    public IList<Node> children;

    public Node(){}
    public Node(int _val,IList<Node> _children) {
        val = _val;
        children = _children;
}
*/
public class Solution {
    public IList<int> Preorder (Node root) {

        var rlist = new List<int> ();

        HelperPreOrder (root, rlist);

        return rlist;
    }

    void HelperPreOrder (Node root, List<int> rlist) {
        if (root == null) return;

        rlist.Add (root.val);

        foreach (var item in root.children) {
            HelperPreOrder (item, rlist);
        }

    }
}