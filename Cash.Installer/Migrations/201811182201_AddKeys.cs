using FluentMigrator;

namespace Cash.Installer.Migrations
{
    [Migration(201811182201)]
    public class AddKeys : Migration
    {
        public override void Up()
        {
            Execute.Sql("delete from Transactions where [Account Name] = 'Cash'");

            Alter.Column("OpeningBalance").OnTable("Account").AsCurrency().NotNullable();
            Alter.Column("Account Name").OnTable("Account").AsAnsiString(50).NotNullable();
            Create.PrimaryKey("PK_Account").OnTable("Account").Column("Account Name");

            Alter.Column("Date").OnTable("Transactions").AsDateTime().NotNullable();
            Alter.Column("Amount").OnTable("Transactions").AsCurrency().NotNullable();
            Alter.Column("Transaction Type").OnTable("Transactions").AsAnsiString(50).NotNullable();
            Alter.Column("Account Name").OnTable("Transactions").AsAnsiString(50).NotNullable();
            Create.ForeignKey("FK_Transactions_Account").FromTable("Transactions").ForeignColumn("Account Name")
                .ToTable("Account").PrimaryColumn("Account Name");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Transactions_Account").OnTable("Transactions");
            Alter.Column("Account Name").OnTable("Transactions").AsAnsiString(50).Nullable();
            Alter.Column("Transaction Type").OnTable("Transactions").AsAnsiString(50).Nullable();
            Alter.Column("Amount").OnTable("Transactions").AsCurrency().Nullable();
            Alter.Column("Date").OnTable("Transactions").AsDateTime().Nullable();

            Delete.PrimaryKey("PK_Account").FromTable("Account");
            Alter.Column("Account Name").OnTable("Account").AsAnsiString(100).Nullable();
            Alter.Column("OpeningBalance").OnTable("Account").AsCurrency().Nullable();
        }
    }
}
