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
    public ListNode RemoveElements(ListNode head, int val)
    {
        if (head == null) return null;

        var node = head;
        while (node != null && node.val == val)
        {
            head = node.next;
            node = node.next;
        }
        while (node != null)
        {
            if (node.next == null)
                break;

            node = MoveToNext(node, val);
        }

        return head;
    }

    ListNode MoveToNext(ListNode node, int val)
    {
        var nextNode = node.next;
        if (nextNode.val == val)
        {
            var temp = nextNode.next;
            node.next = temp;
        }
        else
            node = node.next;

        return node;
    }
}