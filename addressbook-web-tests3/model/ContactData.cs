using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests3
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return (Firstname == other.Firstname) && (Lastname == other.Lastname);
        }

          public override int GetHashCode()
          {
              return Firstname.GetHashCode() + Lastname.GetHashCode();
          } 

        public override string ToString()
        {
            return "firstname=" + Firstname + "lastname=" + Lastname;
        }

        public int CompareTo(ContactData other)
        {
           if (Object.ReferenceEquals(other, null))
            {
                return 1;
            } 

           if(Lastname.CompareTo(other.Lastname) == 0)
            {
                if(Firstname.CompareTo(other.Firstname) == 0)
                {
                      return Firstname.CompareTo(other.Firstname);
                }
            }

            return Lastname.CompareTo(other.Lastname);

        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Id { get; set; }
    }
}
