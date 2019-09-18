﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtworkManager.Models;
using ArtworkManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkManager.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            var list = _authorService.ShowAllAuthors();
            return View(list);
        }

         public IActionResult AddPublicationCode()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPublicationCode(string publicationcode, int id)
        {
            var author = _authorService.FindAuthorById(id);
            _authorService.AddPublicationCode(author, publicationcode);
           return RedirectToAction(nameof(GetCode), new { id = id });
        }
        // Esse método não consegue receber o author!!!



        public IActionResult GetCode(int id)
        {
            var author = _authorService.FindAuthorById(id);
            var obj = _authorService.GetACode(author);
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetCode(Author author)
        {
            _authorService.UseCode(author);
            return RedirectToAction(nameof(GetCode));
        }

        public IActionResult ShowAllCodes(Author author)
        {
            var list = _authorService.ShowAllArtworks(author);
            return View(list);

        }
    }
}