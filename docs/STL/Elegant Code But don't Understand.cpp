// a simple recusrive solution
// https://leetcode.com/explore/featured/card/recursion-i/250/principle-of-recursion/1681/discuss/11183/C++-solution-with-graph-explanation.
class Solution
{
public:
    ListNode *swapPairs(ListNode *head)
    {
        ListNode **pp = &head, *a, *b;
        while ((a = *pp) && (b = a->next))
        {
            a->next = b->next;
            b->next = a;
            *pp = b;
            pp = &(a->next);
        }
        return head;
    }
};