using FluentMigrator;

namespace Infrastructure.Migrations;
[Migration(25122402)]
public class CreateAuthorTable:Migration
{
    public override void Up()
    {
        Create.Table("Authors").WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Country").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Authors");
    }
}