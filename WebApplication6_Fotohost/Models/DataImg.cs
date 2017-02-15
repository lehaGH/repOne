using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;

namespace WebApplication6_Fotohost.Models
{
    public class DataImg
    {
        [Key]
        public int id { get; set; }

        public DateTime time { get; set; }

        public string link { get; set; }

        public string adminLink { get; set; }

        public string name { get; set; }

        public string type {get;set;}

        public byte[] data { get; set; }

        public Bitmap GetBitmap()
        {
            return new Bitmap(new MemoryStream(this.data));
        }

        public void Save()
        {
            using (var connect = new DataContext())
            {
                connect.DataImgs.Add(this);
                connect.SaveChanges();
            }
        }


        public DataImg findByAdminLink(string link)
        {
            using (var connect = new DataContext())
            {
                var buff = connect.DataImgs.Where(x => x.adminLink == link).First();
                return buff;         
            }
        } 

        public DataImg findByLink(string link)
        {
            using (var connect = new DataContext())
            {
                var buff = connect.DataImgs.Where(x => x.link == link).First();
                return buff;         
            }
        } 

        public void GreatUnit(string texts, HttpPostedFileBase uploadImage,int link,int adminLink)
        {
            this.time = DateTime.Today;
            this.link = this.time.ToString() + link.ToString();
            this.link = this.link.Replace(' ', '_').Replace('.', '_').Replace(':', '_');

            this.adminLink = this.link + adminLink.ToString();
            this.name = texts;
            this.type = "image/jpeg";

            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            }

            this.data = imageData;
            this.Save();
        }

        public DataImg FindByID(int id=10)
        {
            using (var connect = new DataContext())
            {
                var buff = connect.DataImgs.Where(x => x.id==id).First();
                return buff;
            }
        }

        public void RemoveByAdminLink(string adminLink)
        {
            using (var connect = new DataContext())
            {
                var buff = connect.DataImgs.Where(x => x.adminLink == adminLink).First();
                if (buff.id != 10) { connect.DataImgs.Remove(buff); connect.SaveChanges(); } else throw new Exception();
            }
        }
       
    }
}