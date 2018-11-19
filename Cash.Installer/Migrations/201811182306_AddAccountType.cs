using FluentMigrator;

namespace Cash.Installer.Migrations
{
    [Migration(201811182306)]
    public class AddAccountType : Migration
    {
        public override void Up()
        {
            Create.Column("Type").OnTable("Account").AsAnsiString(50).NotNullable().WithDefaultValue("Ignore");
            Execute.Sql("update Account set Type = 'Cash' where [Account Name] != 'Cash'");
        }

        public override void Down()
        {
            Delete.Column("Type").FromTable("Account");
        }
    }
}
