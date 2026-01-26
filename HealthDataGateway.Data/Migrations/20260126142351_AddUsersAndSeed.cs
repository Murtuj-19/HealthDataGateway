using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HealthDataGateway.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acknowledgements_IncomingTransferRequests_IncomingRequestId",
                table: "Acknowledgements");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectorRequests_TransferRequests_TransferRequestId",
                table: "ConnectorRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_Patients_PatientId",
                table: "Diagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingTransferRequests_ConnectorRequests_ConnectorRequestId",
                table: "IncomingTransferRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingTransferRequests_Hospitals_SourceHospitalId",
                table: "IncomingTransferRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingTransferRequests_Hospitals_TargetHospitalId",
                table: "IncomingTransferRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Hospitals_HospitalId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferRequests_Hospitals_SourceHospitalId",
                table: "TransferRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferRequests_Hospitals_TargetHospitalId",
                table: "TransferRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferRequests_Patients_PatientId",
                table: "TransferRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferRequests",
                table: "TransferRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomingTransferRequests",
                table: "IncomingTransferRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hospitals",
                table: "Hospitals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diagnoses",
                table: "Diagnoses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectorRequests",
                table: "ConnectorRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acknowledgements",
                table: "Acknowledgements");

            migrationBuilder.RenameTable(
                name: "TransferRequests",
                newName: "TransferRequest");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "Patient");

            migrationBuilder.RenameTable(
                name: "IncomingTransferRequests",
                newName: "IncomingTransferRequest");

            migrationBuilder.RenameTable(
                name: "Hospitals",
                newName: "Hospital");

            migrationBuilder.RenameTable(
                name: "Diagnoses",
                newName: "Diagnosis");

            migrationBuilder.RenameTable(
                name: "ConnectorRequests",
                newName: "ConnectorRequest");

            migrationBuilder.RenameTable(
                name: "ActivityLogs",
                newName: "ActivityLog");

            migrationBuilder.RenameTable(
                name: "Acknowledgements",
                newName: "Acknowledgement");

            migrationBuilder.RenameIndex(
                name: "IX_TransferRequests_TargetHospitalId",
                table: "TransferRequest",
                newName: "IX_TransferRequest_TargetHospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferRequests_Status",
                table: "TransferRequest",
                newName: "IX_TransferRequest_Status");

            migrationBuilder.RenameIndex(
                name: "IX_TransferRequests_SourceHospitalId",
                table: "TransferRequest",
                newName: "IX_TransferRequest_SourceHospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferRequests_PatientId",
                table: "TransferRequest",
                newName: "IX_TransferRequest_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_HospitalId_LocalPatientId",
                table: "Patient",
                newName: "IX_Patient_HospitalId_LocalPatientId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingTransferRequests_TargetHospitalId",
                table: "IncomingTransferRequest",
                newName: "IX_IncomingTransferRequest_TargetHospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingTransferRequests_SourceHospitalId",
                table: "IncomingTransferRequest",
                newName: "IX_IncomingTransferRequest_SourceHospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingTransferRequests_IncomingStatus",
                table: "IncomingTransferRequest",
                newName: "IX_IncomingTransferRequest_IncomingStatus");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingTransferRequests_ConnectorRequestId",
                table: "IncomingTransferRequest",
                newName: "IX_IncomingTransferRequest_ConnectorRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnoses_PatientId",
                table: "Diagnosis",
                newName: "IX_Diagnosis_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectorRequests_TransferRequestId",
                table: "ConnectorRequest",
                newName: "IX_ConnectorRequest_TransferRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectorRequests_Status",
                table: "ConnectorRequest",
                newName: "IX_ConnectorRequest_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Acknowledgements_IncomingRequestId",
                table: "Acknowledgement",
                newName: "IX_Acknowledgement_IncomingRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferRequest",
                table: "TransferRequest",
                column: "TransferRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient",
                table: "Patient",
                column: "PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomingTransferRequest",
                table: "IncomingTransferRequest",
                column: "IncomingRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hospital",
                table: "Hospital",
                column: "HospitalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diagnosis",
                table: "Diagnosis",
                column: "DiagnosisId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectorRequest",
                table: "ConnectorRequest",
                column: "ConnectorRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityLog",
                table: "ActivityLog",
                column: "LogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acknowledgement",
                table: "Acknowledgement",
                column: "AcknowledgementId");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.UpdateData(
                table: "Hospital",
                keyColumn: "HospitalId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 19, 53, 50, 225, DateTimeKind.Local).AddTicks(9824));

            migrationBuilder.UpdateData(
                table: "Hospital",
                keyColumn: "HospitalId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 19, 53, 50, 225, DateTimeKind.Local).AddTicks(9828));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "IsActive", "LastLogin", "PasswordHash", "UserType", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 26, 19, 53, 50, 225, DateTimeKind.Local).AddTicks(9407), "connector@example.com", true, null, "Password123!", "CONNECTOR", "connector" },
                    { 2, new DateTime(2026, 1, 26, 19, 53, 50, 225, DateTimeKind.Local).AddTicks(9432), "source@example.com", true, null, "Password123!", "SOURCE", "source" },
                    { 3, new DateTime(2026, 1, 26, 19, 53, 50, 225, DateTimeKind.Local).AddTicks(9435), "target@example.com", true, null, "Password123!", "TARGET", "target" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Acknowledgement_IncomingTransferRequest_IncomingRequestId",
                table: "Acknowledgement",
                column: "IncomingRequestId",
                principalTable: "IncomingTransferRequest",
                principalColumn: "IncomingRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectorRequest_TransferRequest_TransferRequestId",
                table: "ConnectorRequest",
                column: "TransferRequestId",
                principalTable: "TransferRequest",
                principalColumn: "TransferRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_Patient_PatientId",
                table: "Diagnosis",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingTransferRequest_ConnectorRequest_ConnectorRequestId",
                table: "IncomingTransferRequest",
                column: "ConnectorRequestId",
                principalTable: "ConnectorRequest",
                principalColumn: "ConnectorRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingTransferRequest_Hospital_SourceHospitalId",
                table: "IncomingTransferRequest",
                column: "SourceHospitalId",
                principalTable: "Hospital",
                principalColumn: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingTransferRequest_Hospital_TargetHospitalId",
                table: "IncomingTransferRequest",
                column: "TargetHospitalId",
                principalTable: "Hospital",
                principalColumn: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Hospital_HospitalId",
                table: "Patient",
                column: "HospitalId",
                principalTable: "Hospital",
                principalColumn: "HospitalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRequest_Hospital_SourceHospitalId",
                table: "TransferRequest",
                column: "SourceHospitalId",
                principalTable: "Hospital",
                principalColumn: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRequest_Hospital_TargetHospitalId",
                table: "TransferRequest",
                column: "TargetHospitalId",
                principalTable: "Hospital",
                principalColumn: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRequest_Patient_PatientId",
                table: "TransferRequest",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acknowledgement_IncomingTransferRequest_IncomingRequestId",
                table: "Acknowledgement");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectorRequest_TransferRequest_TransferRequestId",
                table: "ConnectorRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_Patient_PatientId",
                table: "Diagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingTransferRequest_ConnectorRequest_ConnectorRequestId",
                table: "IncomingTransferRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingTransferRequest_Hospital_SourceHospitalId",
                table: "IncomingTransferRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingTransferRequest_Hospital_TargetHospitalId",
                table: "IncomingTransferRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Hospital_HospitalId",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferRequest_Hospital_SourceHospitalId",
                table: "TransferRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferRequest_Hospital_TargetHospitalId",
                table: "TransferRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferRequest_Patient_PatientId",
                table: "TransferRequest");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferRequest",
                table: "TransferRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient",
                table: "Patient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomingTransferRequest",
                table: "IncomingTransferRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hospital",
                table: "Hospital");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diagnosis",
                table: "Diagnosis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectorRequest",
                table: "ConnectorRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityLog",
                table: "ActivityLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acknowledgement",
                table: "Acknowledgement");

            migrationBuilder.RenameTable(
                name: "TransferRequest",
                newName: "TransferRequests");

            migrationBuilder.RenameTable(
                name: "Patient",
                newName: "Patients");

            migrationBuilder.RenameTable(
                name: "IncomingTransferRequest",
                newName: "IncomingTransferRequests");

            migrationBuilder.RenameTable(
                name: "Hospital",
                newName: "Hospitals");

            migrationBuilder.RenameTable(
                name: "Diagnosis",
                newName: "Diagnoses");

            migrationBuilder.RenameTable(
                name: "ConnectorRequest",
                newName: "ConnectorRequests");

            migrationBuilder.RenameTable(
                name: "ActivityLog",
                newName: "ActivityLogs");

            migrationBuilder.RenameTable(
                name: "Acknowledgement",
                newName: "Acknowledgements");

            migrationBuilder.RenameIndex(
                name: "IX_TransferRequest_TargetHospitalId",
                table: "TransferRequests",
                newName: "IX_TransferRequests_TargetHospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferRequest_Status",
                table: "TransferRequests",
                newName: "IX_TransferRequests_Status");

            migrationBuilder.RenameIndex(
                name: "IX_TransferRequest_SourceHospitalId",
                table: "TransferRequests",
                newName: "IX_TransferRequests_SourceHospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferRequest_PatientId",
                table: "TransferRequests",
                newName: "IX_TransferRequests_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_HospitalId_LocalPatientId",
                table: "Patients",
                newName: "IX_Patients_HospitalId_LocalPatientId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingTransferRequest_TargetHospitalId",
                table: "IncomingTransferRequests",
                newName: "IX_IncomingTransferRequests_TargetHospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingTransferRequest_SourceHospitalId",
                table: "IncomingTransferRequests",
                newName: "IX_IncomingTransferRequests_SourceHospitalId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingTransferRequest_IncomingStatus",
                table: "IncomingTransferRequests",
                newName: "IX_IncomingTransferRequests_IncomingStatus");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingTransferRequest_ConnectorRequestId",
                table: "IncomingTransferRequests",
                newName: "IX_IncomingTransferRequests_ConnectorRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnosis_PatientId",
                table: "Diagnoses",
                newName: "IX_Diagnoses_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectorRequest_TransferRequestId",
                table: "ConnectorRequests",
                newName: "IX_ConnectorRequests_TransferRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectorRequest_Status",
                table: "ConnectorRequests",
                newName: "IX_ConnectorRequests_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Acknowledgement_IncomingRequestId",
                table: "Acknowledgements",
                newName: "IX_Acknowledgements_IncomingRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferRequests",
                table: "TransferRequests",
                column: "TransferRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomingTransferRequests",
                table: "IncomingTransferRequests",
                column: "IncomingRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hospitals",
                table: "Hospitals",
                column: "HospitalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diagnoses",
                table: "Diagnoses",
                column: "DiagnosisId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectorRequests",
                table: "ConnectorRequests",
                column: "ConnectorRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs",
                column: "LogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acknowledgements",
                table: "Acknowledgements",
                column: "AcknowledgementId");

            migrationBuilder.UpdateData(
                table: "Hospitals",
                keyColumn: "HospitalId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 15, 27, 12, 56, DateTimeKind.Local).AddTicks(5925));

            migrationBuilder.UpdateData(
                table: "Hospitals",
                keyColumn: "HospitalId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 15, 27, 12, 56, DateTimeKind.Local).AddTicks(5944));

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "HospitalId", "CreatedAt", "HospitalName", "IsActive" },
                values: new object[] { 3, new DateTime(2026, 1, 16, 15, 27, 12, 56, DateTimeKind.Local).AddTicks(5947), "Regional Trauma Center", true });

            migrationBuilder.AddForeignKey(
                name: "FK_Acknowledgements_IncomingTransferRequests_IncomingRequestId",
                table: "Acknowledgements",
                column: "IncomingRequestId",
                principalTable: "IncomingTransferRequests",
                principalColumn: "IncomingRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectorRequests_TransferRequests_TransferRequestId",
                table: "ConnectorRequests",
                column: "TransferRequestId",
                principalTable: "TransferRequests",
                principalColumn: "TransferRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Patients_PatientId",
                table: "Diagnoses",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingTransferRequests_ConnectorRequests_ConnectorRequestId",
                table: "IncomingTransferRequests",
                column: "ConnectorRequestId",
                principalTable: "ConnectorRequests",
                principalColumn: "ConnectorRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingTransferRequests_Hospitals_SourceHospitalId",
                table: "IncomingTransferRequests",
                column: "SourceHospitalId",
                principalTable: "Hospitals",
                principalColumn: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingTransferRequests_Hospitals_TargetHospitalId",
                table: "IncomingTransferRequests",
                column: "TargetHospitalId",
                principalTable: "Hospitals",
                principalColumn: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Hospitals_HospitalId",
                table: "Patients",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "HospitalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRequests_Hospitals_SourceHospitalId",
                table: "TransferRequests",
                column: "SourceHospitalId",
                principalTable: "Hospitals",
                principalColumn: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRequests_Hospitals_TargetHospitalId",
                table: "TransferRequests",
                column: "TargetHospitalId",
                principalTable: "Hospitals",
                principalColumn: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRequests_Patients_PatientId",
                table: "TransferRequests",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
