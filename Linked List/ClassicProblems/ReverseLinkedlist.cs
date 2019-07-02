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
    public ListNode ReverseList(ListNode head)
    {
        if (head == null) return null;

        var nextNode = head;
        var newList = new ListNode(head.val);
        while (nextNode.next != null)
        {
            nextNode = nextNode.next;
            var temp = new ListNode(nextNode.val);
            temp.next = newList;
            newList = temp;
        }
        return newList;
    }
}