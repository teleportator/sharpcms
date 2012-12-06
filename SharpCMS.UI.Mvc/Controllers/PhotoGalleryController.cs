using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SharpCMS.UI.Mvc.Controllers
{
    public class PhotoGalleryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(Guid id)
        {
            const string url = "/content/uploads/gallery/55676863.png";
            var model =
                //new PhotoGalleryListModel() {Photos = new List<ImageView>()};
                new PhotoGalleryListModel
                    {
                        Photos = new List<ImageView>
                                     {
                                         new ImageView {Url = url},
                                         new ImageView {Url = url},
                                         new ImageView {Url = url},
                                         new ImageView {Url = url},
                                         new ImageView {Url = url},
                                         new ImageView {Url = url},
                                         new ImageView {Url = url},
                                         new ImageView {Url = url},
                                         new ImageView {Url = url}
                                     }
                    };
            return View(model);
        }

        public ActionResult Upload(Guid id)
        {
            var model = new PhotoGalleryUploadModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(Guid id, string qqFile)
        {
            if (ModelState.IsValid)
                return Json(new {Success = false});

            return RedirectToAction("List", new {id});
        }
    }

    public class PhotoGalleryUploadModel
    {
        public HttpPostedFileBase Image { get; set; }
    }

    public class PhotoGalleryListModel
    {
        public IEnumerable<ImageView> Photos { get; set; }
    }

    public class ImageView
    {
        public string Url { get; set; }
    }
}
