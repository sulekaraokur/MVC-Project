#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CORE.Services;
using APP.Models;

// Generated from Custom MVC Template.

namespace MVC.Controllers
{
    public class UsersController : Controller
    {
        // Service injections:
        private readonly IService<UserRequest, UserResponse> _userService;
        private readonly IService<StatusRequest, StatusResponse> _statusService;

        /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
        private readonly IService<RoleRequest, RoleResponse> _roleService;

        public UsersController(
			IService<UserRequest, UserResponse> userService
            , IService<StatusRequest, StatusResponse> statusService

            /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
            , IService<RoleRequest, RoleResponse> roleService
        )
        {
            _userService = userService;
            _statusService = statusService;

            /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
            _roleService = roleService;
        }

        private void SetViewData()
        {
            /* 
            ViewBag and ViewData are the same collection (dictionary).
            They carry extra data other than the model from a controller action to its view, or between views.
            */

            // Related items service logic to set ViewData (Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["StatusId"] = new SelectList(_statusService.List(), "Id", "Title"); // Second parameter changed from Name to Title.

            /* Can be uncommented and used for many to many relationships, "entity" may be replaced with the related entity name in the controller and views. */
            ViewBag.RoleIds = new MultiSelectList(_roleService.List(), "Id", "Name");
        }

        private void SetTempData(string message, string key = "Message")
        {
            /*
            TempData is used to carry extra data to the redirected controller action's view.
            */

            TempData[key] = message;
        }

        // GET: Users
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _userService.List();
            return View(list); // return response collection as model to the Index view
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _userService.Single(id);
            return View(item); // return response item as model to the Details view
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(); // return Create view with no model
        }

        // POST: Users/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(UserRequest user)
        {
            if (ModelState.IsValid) // check data annotation validation errors in the request
            {
                // Insert item service logic:
                var response = _userService.Add(user);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
                    return RedirectToAction(nameof(Details), new { id = response.Id }); // redirect to Details action with id parameter as response.Id route value
                }
                ModelState.AddModelError("", response.Message); // to display service error message in the validation summary of the view
            }
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(user); // return request as model to the Create view
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _userService.Edit(id);
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(item); // return request as model to the Edit view
        }

        // POST: Users/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(UserRequest user)
        {
            if (ModelState.IsValid) // check data annotation validation errors in the request
            {
                // Update item service logic:
                var response = _userService.Update(user);
                if (response.IsSuccessful)
                {
                    SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
                    return RedirectToAction(nameof(Details), new { id = response.Id }); // redirect to Details action with id parameter as response.Id route value
                }
                ModelState.AddModelError("", response.Message); // to display service error message in the validation summary of the view
            }
            SetViewData(); // set ViewData dictionary to carry extra data other than the model to the view
            return View(user); // return request as model to the Edit view
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _userService.Single(id);
            return View(item); // return response item as model to the Delete view
        }

        // POST: Users/Delete
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var response = _userService.Remove(id);
            SetTempData(response.Message); // set TempData dictionary to carry the message to the redirected action's view
            return RedirectToAction(nameof(Index)); // redirect to the Index action
        }
    }
}
