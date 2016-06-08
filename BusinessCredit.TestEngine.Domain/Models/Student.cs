using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCredit.TestEngine.Domain.Models
{
    #region Person
    public class Person
    {
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string PrivateNumber { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }

        public ICollection<IDCard> IDCards { get; set; }
    }

    // ContactPersons ??
    // Phone Numbers ??

    public class Student : Person
    {
        public ICollection<DebitCard> DebitCards { get; set; }
        public ICollection<Course> WishList { get; set; }
    }

    public class Teacher : Person
    {
        public ICollection<Student> Students { get; set; }
        public ICollection<Course> Courses { get; set; }
    }

    public class DebitCard
    {
        public int CardID { get; set; }
        public string FriendlyName { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public DateTime ValidThru { get; set; }
        public string CVC { get; set; }
    }

    public class IDCard
    {
        public string PrivateNumber { get; set; }
        public DualString FirstName { get; set; }
        public DualString LastName { get; set; }
        public string CardNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public Gender Gender { get; set; }
        public string CitizenShip { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string IssuingAuthority { get; set; }
        public string CardString { get; set; }
        public byte[] Picture { get; set; }
    }

    public enum Gender
    {
        Unknown = -1,
        Male = 0,
        Female = 1
    }

    public class DualString
    {
        public string Georgian { get; set; }
        public string English { get; set; }
    }

    [ComplexType]
    public class Address
    {
        public AddressType Type { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
    }

    public enum AddressType
    {
        ActualAddress,
        LegalAddress
    }
    #endregion

    public class Course
    {
        public string CourseName { get; set; }
        
    }
}
