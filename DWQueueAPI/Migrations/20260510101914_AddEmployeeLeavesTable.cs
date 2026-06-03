using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWQueueAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeLeavesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Departments",
            //    columns: table => new
            //    {
            //        DepartmentID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Departments", x => x.DepartmentID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EmployeeProjectss",
            //    columns: table => new
            //    {
            //        EmployeeID = table.Column<int>(type: "int", nullable: false),
            //        ProjectID = table.Column<int>(type: "int", nullable: false),
            //        AssignedHours = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EmployeeProjectss", x => new { x.EmployeeID, x.ProjectID });
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Projects",
            //    columns: table => new
            //    {
            //        ProjectID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Projects", x => x.ProjectID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProjectTaskss",
            //    columns: table => new
            //    {
            //        ProjectID = table.Column<int>(type: "int", nullable: false),
            //        TaskID = table.Column<int>(type: "int", nullable: false),
            //        Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProjectTaskss", x => new { x.ProjectID, x.TaskID });
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tasks",
            //    columns: table => new
            //    {
            //        TaskID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EstimatedHours = table.Column<int>(type: "int", nullable: true),
            //        ProjectID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tasks", x => x.TaskID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Employees",
            //    columns: table => new
            //    {
            //        EmployeeID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        DepartmentID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Employees", x => x.EmployeeID);
            //        table.ForeignKey(
            //            name: "FK_Employees_Departments_DepartmentID",
            //            column: x => x.DepartmentID,
            //            principalTable: "Departments",
            //            principalColumn: "DepartmentID");
            //    });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaves",
                columns: table => new
                {
                    LeaveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaves", x => x.LeaveId);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaves_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_EmployeeId",
                table: "EmployeeLeaves",
                column: "EmployeeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Employees_DepartmentID",
            //    table: "Employees",
            //    column: "DepartmentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLeaves");

            //migrationBuilder.DropTable(
            //    name: "EmployeeProjectss");

            //migrationBuilder.DropTable(
            //    name: "Projects");

            //migrationBuilder.DropTable(
            //    name: "ProjectTaskss");

            //migrationBuilder.DropTable(
            //    name: "Tasks");

            //migrationBuilder.DropTable(
            //    name: "Employees");

            //migrationBuilder.DropTable(
            //    name: "Departments");
        }
    }
}
