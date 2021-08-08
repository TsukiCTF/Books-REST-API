using AutoMapper;
using Books.Dtos;
using Books.Models;

namespace Books.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            // map Source -> Target
            CreateMap<Book, BookReadDto>();
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();
            CreateMap<Book, BookUpdateDto>();
        }
    }
}