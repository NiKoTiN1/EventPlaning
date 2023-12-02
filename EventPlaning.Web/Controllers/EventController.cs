using EventPlanning.BusinessLogic.Interfaces;
using EventPlanning.ViewModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanning.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest("Model error!");
            }
            //var tokenModel = await _accountService.CreateAccount(model);


            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventModelDto model)
        {
            Console.WriteLine(DateTime.Now.ToString());

            if (!ModelState.IsValid)
            {   
                return BadRequest("Model error!");
            }
            await _eventService.CreateEvent(model);

            //var tokenModel = await _accountService.CreateAccount(model);


            return Ok(model);
        }

        // GET: EventController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: EventController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: EventController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: EventController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: EventController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: EventController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: EventController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
