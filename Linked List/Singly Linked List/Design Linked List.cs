public class MyLinkedList
{
    class Node
    {
        public int value;
        public Node nextNode;
        public Node(int val)
        {
            value = val;
        }
    }
    Node First;
    Node Last;
    int Count;
    /** Initialize your data structure here. */
    public MyLinkedList()
    {
        Count = 0;
    }

    /** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
    public int Get(int index)
    {
        if (index >= Count || index < 0) return -1;

        Node n = First;
        for (int i = 0; i < index; i++)
            n = n.nextNode;
        return n.value;
    }

    /** Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list. */
    public void AddAtHead(int val)
    {
        var n = new Node(val);
        if (First != null)
            n.nextNode = First;
        Count++;
        First = n;
        if (Last == null)
        {
            Last = new Node(int.MaxValue);
            First.nextNode = Last;
        }
    }

    /** Append a node of value val to the last element of the linked list. */
    public void AddAtTail(int val)
    {
        if (First == null) { First.value = val; First.nextNode = null; }
        Last.value = val;
        var IPromiseThatsLast = new Node(int.MaxValue);
        Last.nextNode = IPromiseThatsLast;
        Last = IPromiseThatsLast;
        Count++;
    }

    /** Add a node of value val before the index-th node in the linked list. If index equals to the length of linked list, the node will be appended to the end of linked list. If index is greater than the length, the node will not be inserted. */
    public void AddAtIndex(int index, int val)
    {
        if (index > Count || index < -1) return;
        if (index == 0 || index == -1) AddAtHead(val);
        else if (index == Count) AddAtTail(val);
        else
        {
            var prev = First;
            for (int i = 0; i < index - 1; i++)
            {
                prev = prev.nextNode;
            }
            Node cur = new Node(val);
            cur.nextNode = prev.nextNode;
            prev.nextNode = cur;
            Count++;
        }

    }

    /** Delete the index-th node in the linked list, if the index is valid. */
    public void DeleteAtIndex(int index)
    {
        if (index >= Count || index < 0) return;
        if (index == 0) { First = First.nextNode; return; }
        var cur = First;
        for (int i = 0; i < index - 1; i++)
            cur = cur.nextNode;
        cur.nextNode = cur.nextNode.nextNode;
        Count--;
    }
}

/**
 * Your MyLinkedList object will be instantiated and called as such:
 * MyLinkedList obj = new MyLinkedList();
 * int param_1 = obj.Get(index);
 * obj.AddAtHead(val);
 * obj.AddAtTail(val);
 * obj.AddAtIndex(index,val);
 * obj.DeleteAtIndex(index);
 */
