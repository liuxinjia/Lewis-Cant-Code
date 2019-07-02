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
    public ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        if (l1 == null) return l2;
        if (l2 == null) return l1;

        ListNode head = new ListNode(0);
        ListNode newList = head;

        while (l1 != null && l2 != null)
        {
            if (l1.val < l2.val)
            {
                newList.next = l1;
                newList = newList.next;
                l1 = l1.next;
            }
            else
            {
                newList.next = l2;
                newList = newList.next;
                l2 = l2.next;
            }
        }

        if (l1 != null) newList.next = l1;
        if (l2 != null) newList.next = l2;

        return head.next;
    }

    // A recursive solution
    public ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        if (l1 == null) return l2;
        if (l2 == null) return l1;

        ListNode handler;
        if (l1.val < l2.val)
        {
            handler = l1;
            handler.next = MergeTwoLists(l1.next, l2);
        }
        else
        {
            handler = l2;
            handler.next = MergeTwoLists(l1, l2.next);
        }

        return handler;
    }
}