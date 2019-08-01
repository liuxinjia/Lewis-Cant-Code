    public class Solution {
        public Node Connect (Node root) {
            if (root == null) return root;

            HelperConnect (root.left, root.right);

            return root;
        }

        public void HelperConnect (Node prev, Node next) {
            if (prev == null) return;

            prev.next = next;

            HelperConnect (prev.left, prev.right);

            HelperConnect (prev.right, next.left);
            HelperConnect (next.left, next.right);
        }
    }