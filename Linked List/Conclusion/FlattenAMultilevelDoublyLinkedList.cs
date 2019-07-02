using System.Collections.Generic;
// // Definition for a Node.
public class Node
{
    public int val;
    public Node prev;
    public Node next;
    public Node child;

    public Node() { }
    public Node(int _val, Node _prev, Node _next, Node _child)
    {
        val = _val;
        prev = _prev;
        next = _next;
        child = _child;
    }
}

public class Solution
{
    #region Recrusive
    public Node Flatten(Node head)
    {
        var startNode = new Node();
        var returnNode = startNode;

        while (head != null)
        {
            head.prev = startNode;
            startNode.next = head;
            startNode = startNode.next;

            if (head.child != null)
            {
                startNode.next = Flatten(head.child);
                head.child = null;
                startNode = startNode.next;
            }

            head = head.next;
        }

        return returnNode.next;
    }

    public Node Flatten(Node head)
    {
        Helper(head);
        return head;
    }

    private Node Helper(Node head)
    {
        Node cur = head, pre = head;
        while (cur != null)
        {
            if (cur.child == null)
            {
                pre = cur;
                cur = cur.next;
            }
            else
            {
                Node tmp = cur.next;
                Node child = Helper(cur.child);
                cur.next = cur.child;
                cur.child.prev = cur;
                cur.child = null;
                child.next = tmp;
                if (tmp != null) tmp.prev = child;
                pre = child;
                cur = tmp;
            }

        }
        return pre;
    }

    Node prev = null;
    public Node Flatten(Node head)
    {
        if (head == null)
        {
            return null;
        }

        if (prev != null)
        {
            prev.next = head;
            head.prev = prev;
        }

        prev = head;

        Node next = head.next;

        Flatten(head.child);
        head.child = null;

        Flatten(next);

        return head;
    }

    #endregion

    #region  DFS

    public Node Flatten(Node head)
    {
        Stack<Node> st = new Stack<Node>();
        Node cur = head;
        while (cur != null)
        {
            if (cur.next == null)
            {
                Node next = st.Count == 0 ? null : st.Pop();
                cur.next = next;
                if (next != null) next.prev = cur;
            }

            if (cur.child == null)
            {
                cur = cur.next;
                continue;
            }

            st.Push(cur.next);
            Node prev = cur;
            cur = cur.child;

            prev.next = cur;
            cur.prev = prev;
            prev.child = null;

        }

        return head;
    }

    public Node Flatten(Node head)
    {
        if (head == null) return null;

        Stack<Node> st = new Stack<Node>();

        st.Push(head);
        Node prev = null;

        while (st.Count > 0)
        {
            var cur = st.Pop();
            var child = cur.child;
            var next = cur.next;

            cur.prev = prev;
            cur.child = null;
            if (prev != null) prev.next = cur;

            prev = cur;

            //right
            if (next != null)
            {
                st.Push(next);
            }

            //left(child)
            if (child != null)
            {
                st.Push(child);
            }

        }

        return head;
    }

    #endregion

}