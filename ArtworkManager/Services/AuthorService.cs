﻿using ArtworkManager.Models;
using ArtworkManager.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Services
{
    public class AuthorService
    {
        private readonly ArtworkManagerContext _context;

        public AuthorService(ArtworkManagerContext context)
        {
            _context = context;
        }

        public List<Author> ShowAllAuthors()
        {
            return _context.Author.ToList();
        }

        

        public List<Artwork> ShowAllArtworks(Author author)
        {
            return _context.Artwork.Where(obj => obj.Owner == author).Include(obj1 => obj1.Owner).ToList();
        }

        public void AddPublicationCode(Author owner, string publicationcode)
        {
            
            var objj = _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);

            var obj1 = objj;

            _context.Artwork.Remove(objj);
            _context.SaveChanges();
            
            
            obj1.PublicationCode = publicationcode;


            _context.Artwork.Add(obj1);
            _context.SaveChanges();

        }

        public Author FindAuthorById (int id)
        {
            return _context.Author.First(obj => obj.Id == id);
            
        }

        public Artwork GetACode (Author owner)
        {
            return _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);

        }

        //This method below uses a code from a existing publication code! (using for reference the last publication code used...)

        public void UseCode (Author owner)
        {
            var objj = _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);
            
            var obj1 = objj;
            
            _context.Artwork.Remove(objj);
            _context.SaveChanges();
            obj1.Status = Models.Enums.ArtworkStatus.Used;
            obj1.BirthDate = DateTime.Now;
            obj1.PublicationCode = _context.Artwork.Last(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.Used).PublicationCode;
                                                       
            
            _context.Artwork.Add(obj1);
            _context.SaveChanges();
        }


        //This method below uses as reference a new publication code...
        public void UseCode2(Author owner)
        {
            var objj = _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);

            var obj1 = objj;

            _context.Artwork.Remove(objj);
            _context.SaveChanges();
            
            obj1.Status = Models.Enums.ArtworkStatus.Used;
            obj1.BirthDate = DateTime.Now;
            



            _context.Artwork.Add(obj1);
            _context.SaveChanges();
        }

        public void Update(Artwork obj)
        {
            if (!_context.Artwork.Any(x=> x.Id == obj.Id && x.Status == Models.Enums.ArtworkStatus.Used))
            {
                throw new NotFoundException("You cannot update information from a non-used artwork.");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
     
        }

        public Artwork FindArtworkById(int id)
        {
            return _context.Artwork.First(obj => obj.Id == id);

        }
    }
}
