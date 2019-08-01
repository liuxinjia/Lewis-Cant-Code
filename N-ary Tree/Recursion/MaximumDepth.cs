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
    public int MaxDepth (Node root) {
        return HelperBottomUp (root);
        return HelperTopDown (root, 0);
    }

    int HelperTopDown (Node root, int count) {
        if (root == null) return count;

        int max = ++count;
        foreach (var item in root.children) {
            max = Math.Max (max, HelperTopDown (item, count));
        }

        return max;
    }

    int HelperBottomUp (Node root) {
        if (root == null) return 0;

        int max = 0;
        foreach (var item in root.children) {
            max = Math.Max (max, HelperBottomUp (item));
        }

        return max + 1;
    }

}