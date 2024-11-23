using myApp.DTOs;
using myApp.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace myApp.Controllers
{
    public class ChannelController : Controller
    {
        mydbEntities6 db = new mydbEntities6();

        
        public static Channel Convert(ChannelDTO dto)
        {
            return new Channel
            {
                ChannelId = dto.ChannelId,
                ChannelName = dto.ChannelName,
                EstablishedYear = dto.EstablishedYear,
                Country = dto.Country
            };
        }

        
        public static ChannelDTO Convert(Channel channel)
        {
            return new ChannelDTO
            {
                ChannelId = channel.ChannelId,
                ChannelName = channel.ChannelName,
                EstablishedYear = channel.EstablishedYear,
                Country = channel.Country
            };
        }

      
        public List<ChannelDTO> Convert(List<Channel> channels)
        {
            var dtoList = new List<ChannelDTO>();
            foreach (var channel in channels)
            {
                dtoList.Add(Convert(channel));
            }
            return dtoList;
        }

     
        [HttpGet]
        public ActionResult CreateChannel()
        {
            return View(new ChannelDTO());
        }

       
        [HttpPost]
        public ActionResult CreateChannel(ChannelDTO dto)
        {
            
            if (db.Channels.Any(c => c.ChannelName == dto.ChannelName))
            {
                ModelState.AddModelError("ChannelName", "Channel name must be unique.");
            }

            if (ModelState.IsValid)
            {
                db.Channels.Add(Convert(dto));
                db.SaveChanges();
                return RedirectToAction("ListChannel");
            }

            return View(dto);
        }

        // GET: ListChannel
        public ActionResult ListChannel()
        {
            var channels = db.Channels.ToList();
            var channelDTOs = Convert(channels);
            return View(channelDTOs);
        }

        // GET: EditChannel
  
        public ActionResult Details(int id)
        {

            var data = db.Channels.Find(id);
            return View(data);
        }


        [HttpGet]
        public ActionResult EditChannel(int id)
        {
            var exobj = db.Channels.Find(id);


            return View(exobj);
        }

        // POST: EditChannel
        [HttpPost]
        public ActionResult EditChannel(ChannelDTO dto)
        {
            
            System.Diagnostics.Debug.WriteLine($"ChannelId received in POST: {dto.ChannelId}");
            if (db.Channels.Any(c => c.ChannelName == dto.ChannelName && c.ChannelId != dto.ChannelId))
            {
                ModelState.AddModelError("ChannelName", "Channel name must be unique.");
            }

            if (ModelState.IsValid)
            {
                var existingChannel = db.Channels.Find(dto.ChannelId);
                existingChannel.ChannelName = dto.ChannelName;
                existingChannel.EstablishedYear = dto.EstablishedYear;
                existingChannel.Country = dto.Country;

                db.SaveChanges();       
                return RedirectToAction("ListChannel");
            }

            return View(dto);
        }

    }
}
