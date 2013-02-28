using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BananaSplit.Data;
using BananaSplit.Helpers;
using BananaSplit.Service;
using Kaliko.ImageLibrary;

namespace BananaSplit.Controllers
{
    
    public class TeamController : BaseController
    {
        private TeamRepository repo;

        public TeamController()
        {
            this.repo = new TeamRepository();
        }
        //
        // GET: /Team/

        public ActionResult Index(int teamTypeId = 1)
        {
            ViewBag.TeamTypes = this.GetTeamTypes();
            ViewBag.SelectedTeamType = this.GetTeamType(teamTypeId);
            return View(this.repo.GetAllTeams());
        }

        
        //
        // GET: /Team/Create

        public ActionResult Create()
        {
            ViewBag.States = this.GetStates();
            return View(new Team());
        }

        //
        // POST: /Team/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file_upload, FormCollection collection)
        {

            ImagePaths photoPaths = null;
            ImageDimensions imgDims = null;
            ImagePaths thumbNailPaths = null;

            try
            {
                photoPaths = SaveUploadedFile(file_upload);
                imgDims = ImageHandler.GetImageDimensions(photoPaths.FilePath);

                //Save file 
                var photo = new Photo();
                photo.PhotoUrl = photoPaths.SqlAddress;
                if (imgDims != null)
                {
                    photo.Width = imgDims.Width;
                    photo.Height = imgDims.Height;
                }
                var photoRepo = new PhotoRepository();
                photoRepo.Save(photo);

                var team = new Team {TeamName = collection.Get("TeamName")};

                //Save Location
                var locationRepo = new LocationRepository();
                var location = new Location();
                location.City = collection.Get("City");
                location.StateId = Convert.ToInt32(collection.Get("StateId"));
                locationRepo.Add(location);

                team.LocationId = location.LocationId;
                team.PhotoId = photo.PhotoId;
                
                this.repo.Add(team);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.States = this.GetStates();
                return View();
            }
        }


        private ImagePaths SaveUploadedFile(HttpPostedFileBase fileUpload)
        {
            var dateFolderPath = ImageHandler.GetDatedFolderPath();
            var fileName = String.Concat(dateFolderPath, "_", Guid.NewGuid().ToString().Replace("-", ""), Path.GetExtension(fileUpload.FileName));
            var pngFileName = String.Concat(dateFolderPath, "_", Guid.NewGuid().ToString().Replace("-", ""), ".png");
            var uploadFolder = String.Concat(AppDomain.CurrentDomain.BaseDirectory, @"Uploads\");
            //var uploadFolder = String.Concat(AppDomain.CurrentDomain.BaseDirectory, @"Uploads\", dateFolderPath, slash);
            //if (!Directory.Exists(uploadFolder))
            //{
            //    Directory.CreateDirectory(uploadFolder);
            //}
            var fullSavePath = Path.Combine(uploadFolder, fileName);
            fileUpload.SaveAs(fullSavePath);

            var pngFullImagePath = this.ConvertUploadedImageToPng(fullSavePath, pngFileName);
            //Use Web Address later
            //var webAddress = String.Concat(ConfigurationManager.AppSettings["BaseDomain"], "Uploads/", dateFolderPath, slash, fileName);
            //var sqlAddress = String.Format("Uploads/{0}{1}{2}", dateFolderPath, slash, fileName);
            var webAddress = String.Concat(ConfigurationManager.AppSettings["BaseDomain"], "Uploads/", pngFileName);
            var sqlAddress = String.Format("Uploads/{0}", pngFileName);

            var paths = new ImagePaths(webAddress, sqlAddress, pngFullImagePath);
            return paths;
            //return Tuple.Create(webAddress, sqlAddress);
        }

        private String ConvertUploadedImageToPng(string fullImagePath, string pngFileName)
        {
            var origImg = new KalikoImage(fullImagePath);
            var pathWOFileName = fullImagePath.Replace(Path.GetFileName(fullImagePath), "");
            var newPngPath = String.Format(@"{0}{1}", pathWOFileName, pngFileName);
            origImg.SavePNG(newPngPath, 95L);
            //If successful, delete original image
            try
            {
                GC.Collect();
                System.IO.File.Delete(fullImagePath);
            }
            catch (Exception e)
            {
                //Log Problem
            }

            return newPngPath;
        }

        //
        // GET: /Team/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.States = this.GetStates();
            var team = this.repo.GetById(id);
            return View(team);
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var team = this.repo.GetById(Convert.ToInt32("TeamId"));
                team.TeamName = collection.Get("TeamName");

                var locationRepo = new LocationRepository();

                var city = collection.Get("City");
                var stateId = Convert.ToInt32(collection.Get("StateId"));

                var location = locationRepo.GetByCityStateId(city, stateId);

                if (null == location)
                {
                    location = new Location();
                    location.City = city;
                    location.StateId = stateId;
                }
                locationRepo.Save(location);

                team.LocationId = location.LocationId;


                this.repo.Add(team);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Team/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var team = this.repo.GetById(id);

            if (null != team)
            {
                this.repo.Remove(team);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        //[SessionAuthenticate]
        public ActionResult DeletePhoto(long photoId)
        {
            try
            {
                var photoRepo = new PhotoRepository();
                var photo = photoRepo.GetById(photoId);
                if (null != photo)
                {
                    photoRepo.Remove(photo);
                }
            }
            catch (Exception e)
            {
                return Json(new { Status = "Failed", Description = e.Message });
            }
            return Json(new { Status = "OK", Description = "" });
        }



        private List<TeamType> GetTeamTypes()
        {
            var teamTypeRepo = new TeamTypeRepository();
            return teamTypeRepo.GetAllTeamTypes();
        }

        private TeamType GetTeamType(int id)
        {
            var teamTypeRepo = new TeamTypeRepository();
            return teamTypeRepo.GetById(id);
        }


        private List<State> GetStates()
        {
            var stateRepo = new StateRepository();
            var states = stateRepo.GetAllStates();
            return states;
        }
    }
}
