namespace SimpleRESTful.Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);
    }
}