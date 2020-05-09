using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MoneySaver.Activities.Data;
using MoneySaver.Activities.Data.Models;
using MoneySaver.Activities.Models;
using MoneySaver.Activities.Services.Contracts;
using MoneySaver.Activities.Web.Models;

namespace MoneySaver.Activities.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IRepository<Transaction> transactionsRepository;
        private readonly IRabbitMqManager _rabbitManagerService;
        public TransactionController(ILogger<TransactionController> logger, IRepository<Transaction> trans, IRabbitMqManager rabbitManager)
        {
            this._logger = logger;
            this.transactionsRepository = trans;
            this._rabbitManagerService = rabbitManager;
            
        }

        public IActionResult Index()
        {
            this.ViewData["Categories"] = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Group 1", Value = "1"},
                new SelectListItem { Text = "Group 2", Value = "2" }
            };
            this.ViewData["Stores"] = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Store 1", Value = "1" },
                new SelectListItem { Text = "Store 2", Value = "2" }
            };

            var model = new TransactionVM();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionVM model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var databaseModel = new Transaction
            {
                AdditionalNote = model.AdditionalNote,
                Amount = model.Amount,
                MarketId = model.StoreId,
                CategoryId = model.CategoryId,
                UserId = 1,
                CreateOn = DateTime.UtcNow,
                ModifyOn = DateTime.UtcNow
            };

            await this.transactionsRepository.AddAsync(databaseModel);
            await this.transactionsRepository.SaveChangesAsync();

            this._rabbitManagerService.Publish(
                databaseModel,
                "demo.exchange",
                "topic",
                "demo.queue.*");

            return this.RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
