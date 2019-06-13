using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Demo.Models;

namespace Demo.Controllers
{
    public class CampaignController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (MvcCRUDDBEntities db = new MvcCRUDDBEntities())
            {
                List<Campaign> campaignList = db.Campaigns.ToList<Campaign>();
                return Json(new { data = campaignList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            using (MvcCRUDDBEntities db = new MvcCRUDDBEntities())
            {
                if (id == 0)
                {
                    return View(new Campaign());
                }
                else
                {
                    return View(db.Campaigns.Where(x => x.Id == id).FirstOrDefault<Campaign>());
                }
            }
        }

        [HttpPost]  
        public ActionResult AddOrEdit(Campaign campaign)
        {
            using (MvcCRUDDBEntities db = new MvcCRUDDBEntities())
            {
                if (campaign.Id == 0)
                {
                     db.Campaigns.Add(campaign);
                     db.SaveChanges();
                     return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {              
                     db.Entry(campaign).State = EntityState.Modified;
                     db.SaveChanges();
                     return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (MvcCRUDDBEntities db = new MvcCRUDDBEntities())
            {
                Campaign campaign = db.Campaigns.Where(x => x.Id == id).FirstOrDefault<Campaign>();
                db.Campaigns.Remove(campaign);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IsCampaignExists(string Name)
        {
            using (MvcCRUDDBEntities db = new MvcCRUDDBEntities())
            {
                return Json(!db.Campaigns.Any(x => x.Name == Name), JsonRequestBehavior.AllowGet);
            }
        }

        // static database not fully functional while saving data
        
        ////
        //// GET: /Campaign/
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult GetData()
        //{
        //    return Json(new { data = CampaignStore.Instance.GetAll() }, JsonRequestBehavior.AllowGet);
        //}


        //[HttpGet]
        //public ActionResult AddOrEdit(int id = 0)
        //{
        //    if (id == 0)
        //        return View(new Campaign());
        //    else
        //    {
        //        return View(CampaignStore.Instance.Get(id));
        //    }
        //}

        //[HttpPost]
        //public ActionResult AddOrEdit(Campaign campaign)
        //{
        //    if (campaign.Id == 0)
        //    {
        //        campaign.Id = new Random().Next(100);
        //        CampaignStore.Instance.Add(campaign);
        //        return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        CampaignStore.Instance.Edit(campaign);
        //        return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    CampaignStore.Instance.Delete(CampaignStore.Instance.Get(id));
        //    return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        //}
    }
}