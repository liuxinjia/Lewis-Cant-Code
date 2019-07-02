using System.Collections.Generic;

// Definition for a Node.
public class Node {
    public int val;
    public Node next;
    public Node random;

    public Node () { }
    public Node (int _val, Node _next, Node _random) {
        val = _val;
        next = _next;
        random = _random;
    }
}

public class Solution {
    public Node CopyRandomList (Node head) {
        HashSet<Node> visited = new HashSet<Node> ();
        visited.Add (head);

        Node start = new Node ();
        Node returnNode = start;

        while (head != null) {
            var cNode = new Node (head.val, null, null);

            var randomNode = head.random;
            if (randomNode != null) {
                if (!visited.Contains (randomNode)) {
                    var rNode = new Node (randomNode.val, randomNode.next, randomNode.random);
                    visited.Add (rNode);
                    cNode.random = rNode;
                } else {
                    cNode.random = randomNode;
                }
            }

            var nextNode = head.next;
            if (nextNode != null) {
                if (!visited.Contains (nextNode)) {
                    var nNode = new Node (nextNode.val, nextNode.next, nextNode.random);
                    visited.Add (nNode);
                    cNode.next = nNode;
                } else {
                    cNode.next = nextNode;
                }
            }

            if (!visited.Contains (cNode)) {
                start.next = cNode;
                start = start.next;
            }

            head = nextNode;
        }

        return returnNode.next;
    }

    //iterate the list with hashtable
    public Node CopyRandomList (Node head) {
        if (head == null) return null;

        Dictionary<Node, Node> map = new Dictionary<Node, Node> ();

        var h1 = head;
        while (h1 != null) {
            map.Add (h1, new Node (h1.val, null, null));

            h1 = h1.next;
        }

        h1 = head;
        while (h1 != null) {
            Node next = null;
            Node rand = null;

            // // map.TryGetValue (h1, out Node cur);
            // // if (h1.next != null)
            // //     map.TryGetValue (h1.next, out next);
            // // if (h1.random != null)
            // //     map.TryGetValue (h1.random, out rand);

            // cur.next = next;
            // cur.random = rand;

            var cur = map[h1];
            cur.next = h1.next == null ? null : map[h1.next];
            cur.random = h1.random == null ? null : map[h1.random];

            h1 = h1.next;
        }

        // map.TryGetValue (head, out Node returnNode);
        return map[head];
        return returnNode;
    }

    // optimised solution without hashset
    public Node CopyRandomList (Node head) {
        var iter = head;
        while (iter != null) {
            var next = iter.next;
            // iter.next = iter;
            var copy = new Node (iter.val, null, null);
            iter.next = copy;
            copy.next = next;

            iter = next;
        }

        iter = head;
        while (iter != null) {
            if (iter.random != null)
                iter.next.random = iter.random.next;

            iter = iter.next.next;
        }

        iter = head;
        var pseudoHead = new Node ();
        var copyList = pseudoHead;
        while (iter != null) {
            var next = iter.next.next;

            pseudoHead.next = iter.next;
            pseudoHead = pseudoHead.next;

            iter.next = next;
            iter = iter.next;
        }

        return copyList.next;
    }

}