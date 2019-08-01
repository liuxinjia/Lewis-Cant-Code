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
using System.Collections.Generic;

public class Solution {
    public IList<IList<int>> LevelOrder (Node root) {
        var rList = new List<IList<int>> ();
        if (root == null) return rList;

        Queue<Node> queue = new Queue<Node> ();
        queue.Enqueue (root);

        while (queue.Count > 0) {
            var list = new List<int> ();

            int curLength = queue.Count;
            for (int i = 0; i < curLength; i++) {
                var node = queue.Dequeue ();
                list.Add (node.val);

                foreach (var item in node.children) {
                    queue.Enqueue (item);
                }
            }
            rList.Add (list);

        }
        return rList;
    }
}