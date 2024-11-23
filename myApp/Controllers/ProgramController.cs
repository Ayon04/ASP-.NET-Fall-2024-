using myApp.DTOs;
using myApp.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace myApp.Controllers
{
    public class ProgramController : Controller
    {
        mydbEntities6 db = new mydbEntities6();

        // Convert ProgramDTO to Program entity
        public static Program Convert(ProgramDTO dto)
        {
            return new Program
            {
                ProgramId = dto.ProgramId,
                ProgramName = dto.ProgramName,
                TRPScore = dto.TRPScore,
                ChannelId = dto.ChannelId,
                AirTime = dto.AirTime
            };
        }

        // Convert Program entity to ProgramDTO
        public static ProgramDTO Convert(Program program)
        {
            return new ProgramDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                TRPScore = program.TRPScore,
                ChannelId = program.ChannelId,
                AirTime = program.AirTime
            };
        }

        [HttpGet]
        public ActionResult CreateProgram(int id)
        {
            var dto = new ProgramDTO
            {
                ChannelId = id 
            };

            ViewBag.Channels = new SelectList(db.Channels, "ChannelId", "ChannelName", id);
            return View(dto);
        }


        
                [HttpPost]
                public ActionResult CreateProgram(ProgramDTO dto,int id )
                {
                    // Validate uniqueness of program name within the channel
                    if (db.Programs.Any(p => p.ProgramName == dto.ProgramName && p.ChannelId == dto.ChannelId))
                    {
                        ModelState.AddModelError("ProgramName", "Program name must be unique within the channel.");
                    }

                    if (ModelState.IsValid)
                    {
                        db.Programs.Add(Convert(dto));
                        db.SaveChanges();
                        return RedirectToAction("ListPrograms");
                    }

                    ViewBag.Channels = new SelectList(db.Channels, "ChannelId", "ChannelName", dto.ChannelId);
                    return View(dto);
                }

       /* [HttpPost]
        public ActionResult CreateProgram(ProgramDTO dto)
        {
            if (db.Programs.Any(p => p.ProgramName == dto.ProgramName && p.ChannelId == dto.ChannelId))
            {
                ModelState.AddModelError("ProgramName", "Program name must be unique within the channel.");
            }

            if (ModelState.IsValid)
            {
                db.Programs.Add(Convert(dto));
                db.SaveChanges();
                return RedirectToAction("ListPrograms");
            }

            ViewBag.Channels = new SelectList(db.Channels, "ChannelId", "ChannelName", dto.ChannelId);
            return View(dto);
        }
*/




        // GET: List of all programs grouped by channels
        public ActionResult ListPrograms()
        {
            var programs = db.Programs.ToList();
            var programDTOs = programs.Select(Convert).ToList();
            return View(programDTOs);
        }


        // GET: Edit program
        [HttpGet]
        public ActionResult EditProgram(int id)
        {
            var program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }

            ViewBag.Channels = new SelectList(db.Channels, "ChannelId", "ChannelName", program.ChannelId);
            return View(Convert(program));
        }

        // POST: Edit program
        [HttpPost]
        public ActionResult EditProgram(ProgramDTO dto)
        {
            if (db.Programs.Any(p => p.ProgramName == dto.ProgramName && p.ChannelId == dto.ChannelId && p.ProgramId != dto.ProgramId))
            {
                ModelState.AddModelError("ProgramName", "Program name must be unique within the channel.");
            }

            if (ModelState.IsValid)
            {
                var existingProgram = db.Programs.Find(dto.ProgramId);
                if (existingProgram != null)
                {
                    existingProgram.ProgramName = dto.ProgramName;
                    existingProgram.TRPScore = dto.TRPScore;
                    existingProgram.ChannelId = dto.ChannelId;
                    existingProgram.AirTime = dto.AirTime;

                    db.SaveChanges();
                }

                return RedirectToAction("ListPrograms");
            }

            ViewBag.Channels = new SelectList(db.Channels, "ChannelId", "ChannelName", dto.ChannelId);
            return View(dto);
        }

        // GET: Delete program
        public ActionResult DeleteProgram(int id)
        {
            var program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }

            return View(Convert(program));
        }

        // POST: Delete program
        [HttpPost, ActionName("DeleteProgram")]
        public ActionResult ConfirmDeleteProgram(int id)
        {
            var program = db.Programs.Find(id);
            if (program != null)
            {
                db.Programs.Remove(program);
                db.SaveChanges();
            }

            return RedirectToAction("ListPrograms");
        }
    }
}
