using System.Collections.Generic;
using AutoMapper;
using Books.Data;
using Books.Dtos;
using Books.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepo _repository;    // repo for DB context
        private readonly IMapper _mapper;   // auto mapper for DTOs

        public BooksController(IBooksRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/books
        [HttpGet]
        public ActionResult <IEnumerable<BookReadDto>> GetAllBooks()
        {
            var bookItems = _repository.GetAllBooks();
            return Ok(_mapper.Map<IEnumerable<BookReadDto>>(bookItems));    // return DTO
        }

        // GET api/books/{id}
        [HttpGet("{id}", Name="GetBookById")]
        public ActionResult <BookReadDto> GetBookById(int id)
        {
            // verify book exists
            var bookModelFromRepo = _repository.GetBookById(id);
            if (bookModelFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BookReadDto>(bookModelFromRepo));  // return DTO
        }

        // POST api/books
        [HttpPost]
        public ActionResult <BookReadDto> CreateBook(BookCreateDto bookCreateDto)
        {
            var bookModel = _mapper.Map<Book>(bookCreateDto);
            _repository.CreateBook(bookModel);
            _repository.SaveChanges();

            // return the DTO & URI (according to REST API specification)
            var bookReadDto = _mapper.Map<BookReadDto>(bookModel);
            return CreatedAtRoute(nameof(GetBookById), new {Id = bookReadDto.Id}, bookReadDto);
        }

        // PUT api/books/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, BookUpdateDto bookUpdateDto)
        {
            // verify book exists
            var bookModelFromRepo = _repository.GetBookById(id);
            if (bookModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(bookUpdateDto, bookModelFromRepo);  // mapper updates the model in db context here
            _repository.UpdateBook(bookModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        // PATCH api/books/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialBookUpdate(int id, JsonPatchDocument<BookUpdateDto> patchDoc)
        {
            // verify book exists
            var bookModelFromRepo = _repository.GetBookById(id);
            if (bookModelFromRepo == null)
            {
                return NotFound();
            }
            // generate a new DTO from book model
            var bookToPatch = _mapper.Map<BookUpdateDto>(bookModelFromRepo);
            // apply the patch
            patchDoc.ApplyTo(bookToPatch, ModelState);
            // run validation test
            if (!TryValidateModel(bookToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(bookToPatch, bookModelFromRepo);
            _repository.UpdateBook(bookModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        // DELETE api/books/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            // verify book exists
            var bookModelFromRepo = _repository.GetBookById(id);
            if (bookModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteBook(bookModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}