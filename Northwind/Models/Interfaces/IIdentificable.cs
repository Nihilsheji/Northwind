namespace Northwind.Models.Interfaces
{
    public interface IIdentificable<KeyType>
    {
        public KeyType Id { get; }
    }
}
