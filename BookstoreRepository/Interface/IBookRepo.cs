using UserModel;

namespace BookstoreRepository.Interface
{
    public interface IBookRepo
    {
        string connectionString { get; set; }

        string BookDetail(BookModel book);
        string DeleteBook(int id);
        string UpdateBook(BookModel bookModel);
    }
}