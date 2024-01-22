using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sys.Migrations
{
    /// <inheritdoc />
    public partial class addtaskschedulertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOT_ScheduleJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobStatus = table.Column<int>(type: "int", nullable: false),
                    JobRunStatus = table.Column<int>(type: "int", nullable: false),
                    LastExecuteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaximumRetries = table.Column<int>(type: "int", nullable: false),
                    IntervalMinutes = table.Column<int>(type: "int", nullable: false),
                    IntervalSeconds = table.Column<int>(type: "int", nullable: false),
                    RunTimes = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cron = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CronRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TriggerType = table.Column<int>(type: "int", nullable: false),
                    TriggerId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NextRunTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JsonParamters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MisfireInstruction = table.Column<int>(type: "int", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RepeatForever = table.Column<bool>(type: "bit", nullable: false),
                    SelectedDaysOfWeek = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    UIBatchSeq = table.Column<int>(type: "int", nullable: false),
                    IsAdhotJob = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOT_ScheduleJobs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOT_ScheduleJobs");
        }
    }
}
