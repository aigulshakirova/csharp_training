using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace addressbook_web_tests3
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;

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

        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string AllPhones {
            get{
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone).Trim();
                }
            }
            set{
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            // return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string AllEmails {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return Email1 + "\r\n" + Email2 + "\r\n" + Email3;
                }
            }
            set
            {
                allEmails = value;
            }
        }

    }
}
