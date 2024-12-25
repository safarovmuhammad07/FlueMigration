using DoMain.Entities;
using FluentMigrator;

namespace Infrastructure.Migrations;
[Migration(25122401)]
public class CreateBookTable:Migration
{
    public override void Up()
    {
        Create.Table("Books").WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("AuthorId").AsInt32().NotNullable().ForeignKey("Authors", "Id")
            .WithColumn("PublisherYear").AsInt32().NotNullable()
            .WithColumn("Genre").AsString().NotNullable()
            .WithColumn("IsAvialable").AsBoolean().NotNullable().WithDefaultValue(false);
    }

    public override void Down()
    {
        Delete.Table("Books");
    }
}