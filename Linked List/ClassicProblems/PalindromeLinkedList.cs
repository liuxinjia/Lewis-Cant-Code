/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */

using System.Collections.Generic;

public class Solution
{
    public bool IsPalindrome(ListNode head)
    {
        if (head == null) return true;

        //1. with extra space : stack reversing;
        ListNode startNode = head;
        ListNode endNode = head;
        int length = 0;
        Stack<int> stack = new Stack<int>();
        while (endNode != null)
        {
            stack.Push(endNode.val);
            endNode = endNode.next;
            length++;
        }

        int eLength = 0;

        while (eLength <= length)
        {
            if (startNode.val != stack.Pop())
            {
                return false;
            }

            startNode = startNode.next;
            eLength += 2;
        }

        return true;


        //2. without extra space: little strick

        var fast = head;
        var slow = head;

        while (fast != null && fast.next != null)
        {
            fast = fast.next.next;
            slow = slow.next;
        }

        if (fast != null)
        {
            slow = slow.next;
        }

        fast = head;
        slow = Reverse(slow);

        while (slow != null)
        {
            if (fast.val != slow.val)
            {
                return false;
            }
            fast = fast.next;
            slow = slow.next;
        }

        return true;
    }

    ListNode Reverse(ListNode node)
    {
        ListNode prev = null;

        while (node != null)
        {
            var next = node.next;
            node.next = prev;
            prev = node;
            node = next;
        }
        return prev;
    }

}