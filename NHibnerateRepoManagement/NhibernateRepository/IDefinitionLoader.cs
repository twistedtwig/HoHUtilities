
namespace NhibernateRepository
{
    public interface IDefinitionLoader
    {
        DbSchemaDefinition CreateDefinition();
    }
}
