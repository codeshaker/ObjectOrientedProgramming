using System;

namespace LibraryManagementSystem
{
    public enum AccountStatus { active, closed, blacklisted }
    public enum ReservationStatus { waiting, pending, canceled, completed }
    public enum BookStatus { reserved, avaialble, loaned, lost }

    public class Address
    {
        string streetAddress , zipcode, city, state , country;
    }

    public class Person
    {
        string name;
        string email;
        string phonenp;
        Address Address;
    }

    public class Contants
    {
        public static final int MAX_BOOK_LEND=5;
        public statis final int MAX_LEND_DAYS=15;
        public static final double perdayFine = 50;
    }

    public class Account
    {
        AccountStatus accountStatus;
        string userid;
        string password;
        Person Person;

        public bool resetPassword();
    }

    public class Member extends Account
    {
        Person person;
        DateTime dateofmembership;
        int totalBooksCheckedOut;

        public bool reserveBookItem(BookItem bookitem);

        public bool incrementtotalBooksCheckedOut();

        public bool checkedoutBookItem(BookItem bookitem)
        {
            // First check if quota maxed out
            if (this.gettotalBooksCheckedOut() >= Contants.MAX_BOOK_LEND)
            {
                Console.WriteLine("Lend quota maxed out");
                return false;
            }

            BookReservation reservation = BookReservation.FetchReservationDetails(bookitem.getBarcode());

            if (reservation != null && reservation.getUserId() != this.getUserId())
            {
                Console.WriteLine("Reserved by other user");
                return false;
            }
            else if (reservation != null && reservation.getUserId() == this.getUserId())
            {
                Console.WriteLine("Update reservation status");
                reservation.updateStatus(ReservationStatus.completed);
            }

            if (!bookitem.Checkout(this.getUserId())) {
                return false;
            }

            this.incrementtotalBooksCheckedOut();
            return true;
        }

        public bool checkForFine(BookItem bookitem) {
            BookLending lending = BookLending.FetchLendingDetails(bookitem.getBarcode());
            Date dueDate=lending.dueDate;
            Date today = new Date();

            if (today.CompareTo(dueDate) > 0)
            {
                int extradays=Date.Today() - dueDate;
                Fine.CollectForFine(this.getUserId(), extradays);
            }
        }

        public bool ReturnBook (BookItem bookitem) {
            this.checkForFine(bookitem);
            BookReservation BookReservation = BookReservation.FetchReservationDetails(bookitem.getBarcode());

            if (BookReservation != null)
            {
                BookItem.updateStatus(BookStatus.Reserved);
                BookReservation.SendBookAvailableNotification();
            }

            bookitem.updateStatus(BookStatus.avaialble);
        }

    }

    public class Librarian extends Account
    {
        sPerson person;

        public bool AddBookItem(BookItem bookitem);
        public bool BlockMember(Member member);
        public bool UnlockMember(Member member);
    }

    public class Book {

        string author, subject, title, punlisher;
        int pageCount;
        bool isReferenceOnly;
    }

    public class BookItem extends Book{

        int rackNo;
        string barcode;
        Date borrowedDate, dueDate;
        double price;
        BookStatus BookStatus;
        BookFormat BookFormat;

        public bool Checkout(string usedId)
        {
            if(this.isReferenceOnly)
            {
                return false;
            }

            if (!BookLending.LendBook(this.getBarcode(), usedId)) {
                return false;
            }

            this.updateStatus(BookStatus.loaned);
        }

        public bool Reserve(string usedId);
        public bool renew(string usedId);
    }

    public class Library {
        Address Address;
        List<BookItem> bookitems;
    }

    public class Catalog implements Search{

        public map<string, list<Book>> booksbytitle;
        public map<string, list<Book>> booksbyauthor;
        public map<string, list<Book>> booksbysubject;

        public list<Book> getBooksByTitle(string title){
            if (mp.find(title)!=mp.end())
            {
                return mp[title];
            }

            return list<Book>();
        } 
    }

    public interface Search
    {
        public list<Book> getBooksByTitle(string title);
    }

    public class Notification
    {
        string content;
        public book sent();
    }

    public class BookReservation
    {
        Date creationDate;
        string bookItembarcode;
        string usedId;

        public static BookReservation FetchReservationDetails(string barcode);
    }

     public class BookLending
    {
        Date creationDate, borrowedDate, dueDate;
        string bookItembarcode;
        string usedId;
        
        public bool LendBook(string usedId, string bookItembarcode);
        public static BookLending FetchLendingDetails(string barcode);
    }

    public class Fine
    {
        Date creationDate;
        string userid;
        string bookbarcode;
        Transaction t;

        public bool CollectForFine(string usedId, string extradays)
        {
            t.initiate()
        }
    }

    public class Transaction
    {
        double amt;
        public bool initiate(double);
    }



    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
