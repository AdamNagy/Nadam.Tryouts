using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Nadam.ConsoleTest.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }


        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public Type Type { get; set; }
        public State State { get; set; }
        public DateTime DownloadDate { get; set; }

        // operator ==, !=
        public static bool operator ==(Image a, Image b)
        {
            return a.Rating == b.Rating &&
                    a.Title == b.Title &&
                    a.Color == b.Color &&
                    a.User.Equals(b.User) &&
                    a.Type == b.Type &&
                    a.State == b.State &&
                    a.DownloadDate == b.DownloadDate;
        }

        public static bool operator !=(Image a, Image b)
        {
            return a.Rating == b.Rating &&
                    a.Title == b.Title &&
                    a.Color == b.Color &&
                    a.User.Equals(b.User) &&
                    a.Type == b.Type &&
                    a.State == b.State &&
                    a.DownloadDate == b.DownloadDate;
        }

        // operator <, >
        public static bool operator <(Image a, Image b)
        {
            return true;
        }

        public static bool operator >(Image a, Image b)
        {
            return false;
        }
    }

    public class ImageComparer : IComparer<Image>
    {
        public User CurrUser { get; set; }

        public ImageComparer()
        {
            
        }

        public ImageComparer(User currUser)
        {
            CurrUser = currUser;
        }


        public int Compare(Image x, Image y)
        {
            if (x < y)
                return -1;
            return x > y ? 1 : 0;
        }
    }
}
