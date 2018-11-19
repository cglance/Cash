using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace Cash.Installer.Migrations
{
    [Migration(0)]
    public class Baseline : AutoReversingMigration
    {
        public override void Up()
        {
            if (Schema.Table("Account").Exists()) return;

            Create.Table("Account")
                .WithColumn("Account Name").AsAnsiString(100).Nullable()
                .WithColumn("OpeningBalance").AsCurrency().Nullable();

            Create.Table("Transactions")
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("Description").AsAnsiString(255).Nullable()
                .WithColumn("Original Description").AsAnsiString(255).Nullable()
                .WithColumn("Amount").AsCurrency().Nullable()
                .WithColumn("Transaction Type").AsAnsiString(50).Nullable()
                .WithColumn("Category").AsAnsiString(50).Nullable()
                .WithColumn("Account Name").AsAnsiString(50).Nullable()
                .WithColumn("Labels").AsAnsiString(50).Nullable()
                .WithColumn("Notes").AsAnsiString(50).Nullable();
        }
    }
}
