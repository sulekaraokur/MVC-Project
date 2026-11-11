#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CORE.Services;
using APP.Models;

// Generated from Custom MVC Template.

namespace MVC.Controllers
{
    public class StatusesController : Controller
    {
        // Service injections:
        private readonly IService<StatusRequest, StatusResponse> _statusService;

        /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
        //private readonly IService<EntityRequest, EntityResponse> _EntityService;

        public StatusesController(
			IService<StatusRequest, StatusResponse> statusService

            /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
            //, IService<EntityRequest, EntityResponse> EntityService
        )
        {
            _statusService = statusService;

            /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
            //_EntityService = EntityService;
        }

        private void SetViewData()
        {
            /* 
            ViewBag and ViewData are the same collection (dictionary).
            They carry extra data other than the model from a controller action to its view, or between views.
            */

            // Related items service logic to set ViewData (Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
            //ViewBag.EntityIds = new MultiSelectList(_EntityService.List(), "Id", "Name");
        }

        private void SetTempData(string message, string key = "Message")
        {
            /*
            TempData is used to carry extra data to the redirected controller action's view.
            */

            TempData[key] = message;
        }

        // GET: Statuses
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _statusService.List();
            return View(list); // return response collection as model to the Index view
        }

        // GET: Statuses/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _statusService.Single(id);
            return View(item); // return response item as model to the Details view
        }

        // GET: Statuses/Create
        public IActionResult Create()
        {
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(); // return Create view with no model
        }

        // POST: Statuses/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(StatusRequest status)
        {
            if (ModelState.IsValid) // check data annotation validation errors in the request
            {
                // Insert item service logic:
                var response = _statusService.Add(status);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
                    return RedirectToAction(nameof(Details), new { id = response.Id }); // redirect to Details action with id parameter as response.Id route value
                }
                ModelState.AddModelError("", response.Message); // to display service error message in the validation summary of the view
            }
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(status); // return request as model to the Create view
        }

        // GET: Statuses/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _statusService.Edit(id);
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(item); // return request as model to the Edit view
        }

        // POST: Statuses/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(StatusRequest status)
        {
            if (ModelState.IsValid) // check data annotation validation errors in the request
            {
                // Update item service logic:
                var response = _statusService.Update(status);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
                    return RedirectToAction(nameof(Details), new { id = response.Id }); // redirect to Details action with id parameter as response.Id route value
                }
                ModelState.AddModelError("", response.Message); // to display service error message in the validation summary of the view
            }
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(status); // return request as model to the Edit view
        }

        // GET: Statuses/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _statusService.Single(id);
            return View(item); // return response item as model to the Delete view
        }

        // POST: Statuses/Delete
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var response = _statusService.Remove(id);
            SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
            return RedirectToAction(nameof(Index)); // redirect to the Index action
        }
    }
}
