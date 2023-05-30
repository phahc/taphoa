using Msys.WebApp.Models;
using Msys.WebApp.Util.Attributes;
using MSys.Interface;
using MSys.Model.Data;
using System;
using System.Web.Mvc;

namespace Msys.WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContact _contactService;


        public ContactController(IContact contactService)
        {
            this._contactService = contactService;
        }

        // GET: Contact
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        // GET: Contact
        [HttpPost]
        [AuthorizedSessionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactModel model)
        {
            Contact data = new Contact();
            data.name = model.name;
            data.email = model.email;
            data.data = model.data;
            data.date = DateTime.Now;
            this._contactService.Register(data);
            return View(model);
        }
    }
}