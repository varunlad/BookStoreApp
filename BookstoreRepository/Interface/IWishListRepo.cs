namespace BookstoreRepository.Interface
{
    public interface IWishListRepo
    {
        string connectionString { get; set; }

        string AddToWishList(int UserId, int BookId);
        string DeleteWishListBook(int WishListId);
    }
}