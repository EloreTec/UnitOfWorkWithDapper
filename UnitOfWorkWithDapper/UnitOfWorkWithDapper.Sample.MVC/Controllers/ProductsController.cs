using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnitOfWorkWithDapper.Sample.Core.AppService;
using UnitOfWorkWithDapper.Sample.Core.Domain;

namespace UnitOfWorkWithDapper.Sample.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductApp _productApp;

        public ProductsController(IProductApp productApp)
        {
            _productApp = productApp;
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(_productApp.GetAll());
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            var product = _productApp.GetById(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                product.Id = 0;

                _productApp.Save(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _productApp.GetById(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                _productApp.Save(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}