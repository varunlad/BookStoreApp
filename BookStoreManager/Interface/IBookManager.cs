using UserModel;

namespace BookStoreManager.Interface
{
    public interface IBookManager
    {
        string Register(BookModel userData);
        string DeleteBook(int id);
        string UpdateBook(BookModel bookModel);
        object GetBookId(int id);
    }
}