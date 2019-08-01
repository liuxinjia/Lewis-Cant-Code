using System.Collections.Generic;

public class Solution {
    public Node Connect (Node root) {
        if (root == null) return root;

        Queue<Node> queue = new Queue<Node> ();
        queue.Enqueue (root);

        while (queue.Count > 0) {
            int length = queue.Count;

            Node prevNode = new Node ();
            for (int i = 0; i < length; i++) {

                var curNode = queue.Dequeue ();
                if (curNode.left != null) queue.Enqueue (curNode.left);
                if (curNode.right != null) queue.Enqueue (curNode.right);

                if (prevNode != null) prevNode.next = curNode;
                prevNode = curNode;
            }

        }
        return root;
    }
}