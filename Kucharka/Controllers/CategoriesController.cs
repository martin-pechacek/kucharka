﻿using Semestralka.DatabaseModels;
using Semestralka.DataObjects;
using Semestralka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Semestralka.Controllers
{
    public class CategoriesController : Controller
    {
        //
        // GET: /Categories/
        public async Task<ActionResult> Index(CategoryModel model)
        {
            List<CategoryDO> categories = await CategoryDO.GetCategoriesAsync();

            ViewBag.Categories = categories;

            return View(model);
        }

        /**
        *  Method for inserting new category into database
        **/
        [HttpPost]
        public async Task<ActionResult> Index(Category category, string add)
        {
            await Task.Delay(0);

            using (Entities kucharkaEntities = new Entities())
            {
               //sends data into database
               kucharkaEntities.Categories.Add(category);
               //save changes in database
               kucharkaEntities.SaveChanges();
               //initialization variable for error message
               string message = string.Empty;

               //storing information message into variable depending on value returned from databse procedure
               switch (category.id_category)
               {
                    case -1:
                       message = "Kategorie již existuje.\\nVyberte jiné jméno.";
                       break;
                    default:
                        message = "Kategorie přidána.\\nId: " + category.id_category.ToString();
                        break;
                }
            }
           
            return RedirectToAction("Index", "Categories");
        }

        public async Task<ActionResult> Remove(string categoryName)
        {
            await Task.Delay(0);
            
            using (Entities context =
                new Entities())
            {
                //find category
                var category = context.Categories.Single(x => x.name_category == categoryName);
                //remove category in db
                context.Categories.Remove(category);
                //save change in db
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Categories");
        }

        public async Task<ActionResult> Edit(int? id, string add, CategoryModel model) 
        {
            try
            {
                Category category = await CategoryDO.GetCategoryAsync((int)id);

                @ViewBag.Category = category;

                //Control if button for saving change was pressed. If not edit page is normally loaded 
                if (add != null)
                {
                    using (Entities context = new Entities())
                    {
                        //find category
                        var categoryRecord = context.Categories.Single(x => x.id_category == id);
                        //set new category name
                        categoryRecord.name_category = model.name_category;
                        //save change in db
                        context.SaveChanges();

                        return RedirectToAction("Index", "Categories");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.StackTrace.Contains("GetCategoryAsync") || id == null)
                {
                    return RedirectToAction("Index", "Categories");
                }
            }

            return View();   
        }
	}
}