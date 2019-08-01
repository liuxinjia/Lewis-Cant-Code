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
    public IList<int> Postorder (Node root) {
        var rList = new List<int> ();

        HelperPost (root, rList);

        return rList;
    }

    void HelperPost (Node root, List<int> rList) {
        if (root == null) return;

        foreach (var item in root.children) {
            HelperPost (item, rList);
        }

        rList.Add (root.val);
    }
}