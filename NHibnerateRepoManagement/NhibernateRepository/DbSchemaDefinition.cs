﻿
namespace NhibernateRepository
{
    public class DbSchemaDefinition
    {
        public DbSchemaDefinition()
        {
            ShowSql = true;
        }

        public string ConnectionStringName { get; set; }
        public bool ShowSql { get; set; }
    }
}
