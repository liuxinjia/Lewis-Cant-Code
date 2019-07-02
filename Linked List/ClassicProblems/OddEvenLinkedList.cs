/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode OddEvenList(ListNode head)
    {
        if (head == null) return head;

        ListNode oddList = head;
        ListNode evenList = head.next;
        ListNode eFirst = evenList;

        while (evenList != null && evenList.next != null)
        {
            // oddList.next = oddList.next.next;
            // evenList.next = evenList.next.next;
            // oddList = oddList.next;
            // evenList = evenList.next;

            oddList.next = evenList.next;
            oddList = oddList.next;
            evenList.next = oddList.next;
            evenList = evenList.next;
        }

        oddList.next = eFirst;

        return head;

    }
}