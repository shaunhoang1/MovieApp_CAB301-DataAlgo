//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //make sure members are sorted in dictionary order

    // Properties

    // get the capacity of this member colllection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member colllection 
    // pre-condition: nil
    // post-condition: return the number of members in this member collection and this member collection remains unchanged
    public int Number { get { return count; } }

   


    // Constructor - to create an object of member collection 
    // Pre-condition: capacity > 0
    // Post-condition: an object of this member collection class is created

    public MemberCollection(int capacity)
    {
        if (capacity > 0)
        {
            this.capacity = capacity;
            members = new Member[capacity];
            count = 0;
        }
    }

    // check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull()
    {
        return count == capacity;
    }

    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        // To be implemented by students in Phase 1
        //Checking if there is an existing member with matching names to avoid duplicates
        if (!Search(member))
        {
            //Checking if the array has reached it's capacity limit
            if (!IsFull())
            {
                if (IMember.IsValidContactNumber(member.ContactNumber) || IMember.IsValidContactNumber(member.Pin))
                {
                    int position = count - 1;
                    Member memberInsert = (Member)member; 

                    //With insertion sort, members will be sorted and remain in ascending order throughout run time
                    while((position >= 0) && members[position].CompareTo(memberInsert) > 0)
                    {
                        //Assigning the current element's value to the succeeding element
                        members[position + 1] = members[position];
                        //Decrease position by 1
                        position--;
                    }
                    //Increase count and array capacity by 1 to add memberInsert to the member collection
                    members[position + 1] = memberInsert;
                    count++;
                }
                else
                {
                    Console.WriteLine("Insert Error:\nEither Pin or Contact Number is not valid.");
                }

            } 
            else
            {
                Console.WriteLine("Member collection has reached limit capacity");
            }
        }
        else
        {
            Console.WriteLine("This member already exist. No duplicate allowed.\n");
        }
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
    public void Delete(IMember aMember)
    {
        // To be implemented by students in Phase 1
        Member memberDelete = (Member)aMember;

        for(int i = 0; i < count; i++)
        {
            //If the member is present in the array
            if(members[i].CompareTo(memberDelete) == 0)
            {
                //Assign index position of memberDelete to j
                //Shifting array to the left by reducing array capacity by 1
                for (int j = i; j < count - 1; j++)
                {
                    //Assigning the succeeding element's value to the current element   
                    members[j] = members[j + 1]; 
                }
                count--;
            }
        }
    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        // To be implemented by students in Phase 1
        int min = 0;
        int max = count - 1;
        Member searchMember = (Member)member;

        while (min <= max)
        {
            //The middle point in the array
            int mean = (min + max) / 2;
            if (searchMember.CompareTo(members[mean]) == 0)
            {
                return true;
            }
            else if (searchMember.CompareTo(members[mean]) < 0)
            {
                //If the search key is lower than the mid point, the result lies on the left side of the mid point
                //Decrease max by 1
                max = mean - 1;
            }
            else
            {
                //If the search key is higher than the mid point, the result lies on the right side of the mid point
                //Increase min by 1
                min = mean + 1;
            }
        }
        return false;
    }

    // Find a given member in this member collection 

    // Pre-condition: nil

    // Post-condition: return the reference of the member object in the member collection, if this member is in the member collection; return null otherwise; member collection remains unchanged

    public IMember Find(IMember member)
    {
        Member findMember = null;
        if (Search(member))
        {
            findMember = (Member) member;
        } else
        {
            Console.WriteLine("No member found");
            return null;
        }
        return findMember;
    }

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            this.members[i] = null;
        }
        count = 0;
    }



    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public string ToString()
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n";
        return s;
    }
}

