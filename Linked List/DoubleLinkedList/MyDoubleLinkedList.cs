using System;

public class MyLinkedList
{
    // Definition for singly-linked list
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode prev;
        public ListNode(int x)
        {
            val = x;
        }
    }

    ListNode original;
    int length;

    /** Initialize your data structure here. */
    public MyLinkedList()
    {
    }

    /** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
    public int Get(int index)
    {
        return GetNode(index).val;
    }

    ListNode GetNode(int index)
    {
        int count = 0;
        var start = original;

        //fixed 2
        if (start == null)
            return new ListNode(-1);

        //fixed 1 
        while (start.next != null && count < index)
        {
            start = start.next;
            count++;
        }

        if (count == index) return start;
        else
        {
            return new ListNode(-1);
        }
    }

    /** Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list. */
    public void AddAtHead(int val)
    {
        if (original == null)
        {
            original = new ListNode(val);
        }
        else
        {
            var newstart = new ListNode(val);
            newstart.next = original;
            original.prev = newstart;
            original = newstart;
        }
        length++;
    }

    /** Append a node of value val to the last element of the linked list. */
    public void AddAtTail(int val)
    {
        if (original == null)
        {
            original = new ListNode(val);
        }
        else
        {
            var start = original;
            //fixed 1
            while (start.next != null)
            {
                start = start.next;
            }
            // start = start.prev;
            var newNode = new ListNode(val);
            newNode.prev = start;
            start.next = newNode;
        }
        length++;
    }

    /** Add a node of value val before the index-th node in the linked list. If index equals to the length of linked list, the node will be appended to the end of linked list. If index is greater than the length, the node will not be inserted. */
    public void AddAtIndex(int index, int val)
    {
        //fixed 2
        if (index <= 0)
        {
            AddAtHead(val);
        }
        else if (index == length)
        {
            AddAtTail(val);
        }
        else
        {
            var curNode = GetNode(index);
            if (curNode.val < 0)
            {
                return;
            }

            var prevNode = curNode.prev;
            var insertNode = new ListNode(val);

            insertNode.prev = prevNode;
            insertNode.next = curNode;
            curNode.prev = insertNode;
            //fixed 2
            if (prevNode != null)
                prevNode.next = insertNode;

            // prevNode.next = insertNode;
            // insertNode.prev = prevNode;
            // curNode.prev = insertNode;
            // insertNode.next = curNode;

            //fixed 3
            length++;
        }

    }

    /** Delete the index-th node in the linked list, if the index is valid. */
    public void DeleteAtIndex(int index)
    {
        var deleteNode = GetNode(index);
        if (deleteNode.val == -1) return;

        var prevNode = deleteNode.prev;
        var nextNode = deleteNode.next;

        //fixed 2 
        if (prevNode != null)
        {
            prevNode.next = nextNode;
            //fixed 1
            if (nextNode != null)
            {
                nextNode.prev = prevNode;
            }
        }
        else
        {
            original = nextNode;
        }

        length--;
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
