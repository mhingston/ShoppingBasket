using System;

namespace ShoppingBasketLibrary.Models.Product
{
    public abstract class ProductTypeEnum : IComparable
    {
        private string Name { get; }
        private int Id { get; }

        protected ProductTypeEnum(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;
        
        public int CompareTo(object other) => Id.CompareTo(((ProductTypeEnum)other).Id);
    }
}