using BookStoreManager.Interface;
using BookstoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using UserModel;

namespace BookStoreManager.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepo repository;
        public BookManager(IBookRepo repository)
        {
            this.repository = repository;
        }
        public string Register(BookModel userData)
        {
            try
            {
                return this.repository.BookDetail(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteBook(int id)
        {
            try
            {
                return this.repository.DeleteBook(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string UpdateBook(BookModel bookModel)
        {
            try
            {
                return this.repository.UpdateBook(bookModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public object GetBookId(int id)
        {
            try
            {
                return this.repository.GetBookId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<BookModel> DisplayAllBooks()
        {
            try
            {
                return this.repository.DisplayAllBooks();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
