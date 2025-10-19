using HaimDadon.PeopleManager.web.Helpers;
using HaimDadon.PeopleManager.web.Models;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace HaimDadon.PeopleManager.web.Controllers
{
    public class PeopleController : Controller
    {
        DatabaseContext Database = new DatabaseContext();

        [HttpGet]
        public RedirectResult Index()
        {
            return Redirect("Display");
        }


        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Person person, HttpPostedFileBase imageFile)
        {
            var ErorrJson   = Json(new { IsSucceeded = false, message = MessageConstants.PEOPLE_CREATE_ERROR });
            var SuccessJson = Json(new { IsSucceeded = true, message = MessageConstants.PEOPLE_CREATE_SUCCESS });
            if (
                !ModelState.IsValid ||
                isExistEmail(person.Email) ||
                isExistPhone(person.Phone)
            )  return ErorrJson;
           
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string uploadPath = Server.MapPath("~/Uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                string fileName = "I" + Guid.NewGuid().ToString("N") + Path.GetExtension(imageFile.FileName);
                string fullPath = Path.Combine(uploadPath, fileName);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    imageFile.InputStream.CopyTo(fs);
                }

                person.ImagePath = "/Uploads/" + fileName;
            }
            try
            {
                Database.Person.Add(person);
                Database.SaveChanges();
            }
            catch (Exception e) {
                return ErorrJson;
            }
            return SuccessJson;
        }

        private bool isExistEmail(string email)
        {
            return Database.Person.Any(x => x.Email == email);
        }
        private bool isExistPhone(string phone)
        {
            return Database.Person.Any(x => x.Phone == phone);
        }


        [HttpGet]
        public JsonResult IsEmailAvailable(string email)
        {
            return Json(!isExistEmail(email), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IsPhoneAvailable(string phone)
        {
            return Json(!isExistPhone(phone), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ViewResult Display(string search)
        {
            List<Person> list = string.IsNullOrEmpty(search) ?
                Database.Person.ToList()
                :
                Database.Person
                .Where(p => p.FullName.Contains(search))
                .OrderBy(p => p.FullName)
                .ToList();

            return View(list);
        }





        private string HebrewReverse(string text) {
            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public FileResult ExportPdf()
        {
            List<Person> people = Database.Person.OrderBy(p => p.FullName).ToList();
            BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf",BaseFont.IDENTITY_H,BaseFont.EMBEDDED);
            Font font = new Font(baseFont, 14);

            using (var stream = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    PdfWriter.GetInstance(doc, stream).CloseStream = false;
                    doc.Open();
                    doc.Add(new Paragraph("PDF - People List:", font));
                    foreach (var p in people)
                    {
                        string fullName = p.FullName;
                        if (Regex.IsMatch(fullName, @"\p{IsHebrew}"))
                            fullName = HebrewReverse(fullName);
                        Paragraph para = new Paragraph($"{fullName} | {p.Phone} | {p.Email}", font);
                        doc.Add(para);
                    }
                    doc.Close();
                }
                stream.Position = 0;
                string pdfName = "People_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".pdf";
                return File(stream.ToArray(), "application/pdf", pdfName);
            }
        }

    }
}