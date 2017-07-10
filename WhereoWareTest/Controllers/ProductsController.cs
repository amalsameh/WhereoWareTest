using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhereoWareTest.Models;
using PagedList;
using System.Configuration;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WhereoWareTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private WhereoWareDb db = new WhereoWareDb();

        // GET: Products
       
        [AllowAnonymous]
        [OutputCache(CacheProfile = "Strong", VaryByHeader = "X-Requested-With", Location = OutputCacheLocation.Server)]
        public ActionResult Index(int page= 1)
        {
            int pageSize = Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
            var productsList = new List<ProductListViewModel>();
            foreach(Product _product in db.Products)
            {
                var viewItem = new ProductListViewModel();
                viewItem.Name = _product.Name;
                viewItem.ImagePath = "Content/Images/Products/thumbs/" + _product.ImagePath;
                productsList.Add(viewItem);
            }
            IPagedList<ProductListViewModel> products = productsList.ToPagedList(page, pageSize);
            if (Request .IsAjaxRequest())
            {
                return PartialView("_Products", products);
            }

            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [ChildActionOnly]
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase _Image)
        {
            string imageName = Path.GetFileName(_Image.FileName);
            imageName = Path.Combine(Server.MapPath("~/Content/Images"), imageName);
            _Image.SaveAs(imageName);
            var productUpload = new ProductUploadViewModel();
            productUpload.ProductImage = _Image;
            return View("Create", productUpload);
        }
        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductUploadViewModel productUpload)
        {
            //Map ViewModel to Model
            var product = new Product();
            var imageExt = Path.GetExtension(productUpload.ProductImage.FileName).Substring(1);
            string originalName = productUpload.SKU + "." + imageExt;
            product.Name = productUpload.Name;
            product.SKU = productUpload.SKU;
            product.Overview = productUpload.Overview;
            product.ImagePath = originalName;
            string originalFile = Path.Combine(Server.MapPath("~/Content/Images/Products"), originalName);
            var thumbName = Path.Combine(Server.MapPath("~/Content/Images/Products/thumbs"), originalName);
            productUpload.ProductImage.SaveAs(originalFile);
            createThumbnail(originalFile, thumbName);
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [NonAction]
        private  void createThumbnail(string imageFile, string newName) {
            using (var srcImage = Image.FromFile(imageFile))
            using (var newImage = new Bitmap(100, 100))
            using (var graphics = Graphics.FromImage(newImage)) {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(srcImage, new Rectangle(0, 0, 100, 100));
                newImage.Save(newName, ImageFormat.Png);                
            }            
        }
  
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
