using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PV260.ArkFundsTracker.Web.Migrations
{
    /// <inheritdoc />
    public partial class FreshMigration : Migration
    {
        private static readonly string[] columns = new[] { "Id", "AdminId", "Company", "Cusip", "Date", "DeletedAt", "Fund", "MarketValue", "Shares", "Ticker", "WeightPercentage" };

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fund_positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Ticker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Fund = table.Column<string>(type: "text", nullable: false, defaultValue: "ARKK"),
                    Company = table.Column<string>(type: "text", nullable: false),
                    Cusip = table.Column<string>(type: "text", nullable: false),
                    Shares = table.Column<decimal>(type: "numeric", nullable: false),
                    MarketValue = table.Column<decimal>(type: "numeric", nullable: false),
                    WeightPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    AdminId = table.Column<int>(type: "integer", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fund_positions", x => x.Id);
                    table.CheckConstraint("ck_fund_positions_fund", "\"Fund\" = 'ARKK'");
                });

            migrationBuilder.InsertData(
                table: "fund_positions",
                columns: columns,
                values: new object[,]
                {
                    { new Guid("00ba0e09-9fa6-41d1-8b63-1151ffae91f6"), null, "ROBINHOOD MARKETS INC - A", "770700102", new DateOnly(2026, 4, 15), null, "ARKK", 305435219.2m, 3861869m, "HOOD", 4.57m },
                    { new Guid("02c6c154-91e7-432b-bfa3-2e11cd096471"), null, "TAIWAN SEMICONDUCTOR-SP ADR", "874039100", new DateOnly(2026, 4, 15), null, "ARKK", 94165233.75m, 247875m, "TSM", 1.41m },
                    { new Guid("032b1743-3020-4fed-b333-c2b5e9cd4627"), null, "BULLISH", "G16910120", new DateOnly(2026, 4, 16), null, "ARKK", 174154681.3m, 4179378m, "BLSH", 2.51m },
                    { new Guid("0c50e251-bb9e-447f-b657-7fa696fe74bf"), null, "CIRCLE INTERNET GROUP INC", "172573107", new DateOnly(2026, 4, 15), null, "ARKK", 302123465.5m, 2864001m, "CRCL", 4.52m },
                    { new Guid("0d4ed431-525c-4559-a8ae-2414e1959568"), null, "KRATOS DEFENSE & SECURITY", "50077B207", new DateOnly(2026, 4, 15), null, "ARKK", 61570994.46m, 835881m, "KTOS", 0.92m },
                    { new Guid("0da43142-0312-44e6-8460-d03c7082bf1d"), null, "VERACYTE INC", "92337F107", new DateOnly(2026, 4, 16), null, "ARKK", 62749676.4m, 1843410m, "VCYT", 0.9m },
                    { new Guid("109512a1-bf9c-4f5f-b78e-1f852f533ca0"), null, "CIRCLE INTERNET GROUP INC", "172573107", new DateOnly(2026, 4, 16), null, "ARKK", 302884713.5m, 2870401m, "CRCL", 4.36m },
                    { new Guid("10ac21c0-3629-416a-8727-eb99844ff634"), null, "BITMINE IMMERSION TECHNOLOGI", "09175A206", new DateOnly(2026, 4, 16), null, "ARKK", 143707523.3m, 6432745m, "BMNR", 2.07m },
                    { new Guid("11860e0b-5240-4e9b-8946-4534ed055d20"), null, "CRISPR THERAPEUTICS AG", "H17182108", new DateOnly(2026, 4, 16), null, "ARKK", 449156070m, 7913250m, "CRSP", 6.47m },
                    { new Guid("15f402ee-3277-4a78-8947-d9e53ed22fbf"), null, "CRISPR THERAPEUTICS AG", "H17182108", new DateOnly(2026, 4, 15), null, "ARKK", 448943702.3m, 7895598m, "CRSP", 6.71m },
                    { new Guid("186f8e3d-d0c7-4d3b-9410-9f346324f150"), null, "BRERA HOLDINGS PLC-CL B", "G13311116", new DateOnly(2026, 4, 15), null, "ARKK", 5618583.6m, 6688790m, "SLMT", 0.08m },
                    { new Guid("19b8e63b-c8e4-418e-9493-b8670c681ff2"), null, "ARCHER AVIATION INC-A", "03945R102", new DateOnly(2026, 4, 15), null, "ARKK", 103952050.7m, 18333695m, "ACHR", 1.55m },
                    { new Guid("1b5c2af5-dd98-4014-a581-64a552e3193f"), null, "SOFI TECHNOLOGIES INC", "83406F102", new DateOnly(2026, 4, 16), null, "ARKK", 42830452.12m, 2279428m, "SOFI", 0.62m },
                    { new Guid("1b78ca9d-1d76-4faa-a230-2312f6a4094a"), null, "BROADCOM INC", "11135F101", new DateOnly(2026, 4, 15), null, "ARKK", 71965896.88m, 188996m, "AVGO", 1.08m },
                    { new Guid("1fe60f8c-26db-4f05-a189-e5fc07ac6ff4"), null, "ILLUMINA INC", "452327109", new DateOnly(2026, 4, 15), null, "ARKK", 83645892m, 641850m, "ILMN", 1.25m },
                    { new Guid("2064cf21-c0e0-48b8-8320-83e6fc857856"), null, "TWIST BIOSCIENCE CORP", "90184D100", new DateOnly(2026, 4, 15), null, "ARKK", 202929366m, 3534739m, "TWST", 3.03m },
                    { new Guid("213e3361-4596-43e0-a241-fc2f2f505874"), null, "INTELLIA THERAPEUTICS INC", "45826J105", new DateOnly(2026, 4, 16), null, "ARKK", 141106342.9m, 9444869m, "NTLA", 2.03m },
                    { new Guid("227bf625-2cc2-4658-8a4f-9746c8d0cd57"), null, "TEMPUS AI INC-CL A", "88023B103", new DateOnly(2026, 4, 16), null, "ARKK", 369688519.6m, 6553599m, "TEM", 5.32m },
                    { new Guid("267f04f8-2e9a-4087-8b03-b7942e060c36"), null, "TWIST BIOSCIENCE CORP", "90184D100", new DateOnly(2026, 4, 16), null, "ARKK", 204126859.2m, 3542639m, "TWST", 2.94m },
                    { new Guid("2f19cbb7-1ec8-4a94-8892-5ccadf2a1677"), null, "COINBASE GLOBAL INC -CLASS A", "19260Q107", new DateOnly(2026, 4, 15), null, "ARKK", 281531186.2m, 1526659m, "COIN", 4.21m },
                    { new Guid("30f446a0-e44f-406f-a53d-3bee85c3c752"), null, "BULLISH", "G16910120", new DateOnly(2026, 4, 15), null, "ARKK", 169387756m, 4170058m, "BLSH", 2.53m },
                    { new Guid("310dc826-a1d1-4990-8c73-7de6f05bc334"), null, "VERACYTE INC", "92337F107", new DateOnly(2026, 4, 15), null, "ARKK", 63087921.4m, 1839298m, "VCYT", 0.94m },
                    { new Guid("32205279-d803-45eb-a088-e57722caf83d"), null, "ILLUMINA INC", "452327109", new DateOnly(2026, 4, 16), null, "ARKK", 85080477.32m, 643282m, "ILMN", 1.22m },
                    { new Guid("32de7ab3-ecc2-40cc-b560-88561b37a2bc"), null, "DEERE & CO", "244199105", new DateOnly(2026, 4, 15), null, "ARKK", 83365730.64m, 139866m, "DE", 1.25m },
                    { new Guid("35538618-53d5-405f-bd4c-1bb26a5f6ae7"), null, "BWX TECHNOLOGIES INC", "05605H100", new DateOnly(2026, 4, 15), null, "ARKK", 74812729.41m, 313983m, "BWXT", 1.12m },
                    { new Guid("37044bae-49b2-48f8-ae22-50b5e44bb7c8"), null, "SOFI TECHNOLOGIES INC", "83406F102", new DateOnly(2026, 4, 15), null, "ARKK", 40733501.04m, 2274344m, "SOFI", 0.61m },
                    { new Guid("512b95a1-6dc3-4288-8853-a518a4a9ea03"), null, "10X GENOMICS INC-CLASS A", "88025U109", new DateOnly(2026, 4, 16), null, "ARKK", 180183211.7m, 7107819m, "TXG", 2.59m },
                    { new Guid("531586c8-2fd3-4178-b7ac-e53f91cb9e99"), null, "GENEDX HOLDINGS CORP", "81663L200", new DateOnly(2026, 4, 15), null, "ARKK", 58072322.76m, 868566m, "WGS", 0.87m },
                    { new Guid("541c9aae-1f39-47a3-9c34-6aa089de56f9"), null, "FIGMA INC-CL A", "316841105", new DateOnly(2026, 4, 15), null, "ARKK", 33762312.72m, 1832916m, "", 0.5m },
                    { new Guid("5a0cc849-256f-4aac-8201-26cd06755fe9"), null, "BAIDU INC - SPON ADR", "56752108", new DateOnly(2026, 4, 15), null, "ARKK", 65506395.57m, 554059m, "BIDU", 0.98m },
                    { new Guid("5a7e4695-80fb-4554-9350-93753b2fcf6a"), null, "PALANTIR TECHNOLOGIES INC-A", "69608A108", new DateOnly(2026, 4, 15), null, "ARKK", 207008857.3m, 1525489m, "PLTR", 3.09m },
                    { new Guid("5bd03ed8-08c0-4251-b649-3db899c149ed"), null, "PALANTIR TECHNOLOGIES INC-A", "69608A108", new DateOnly(2026, 4, 16), null, "ARKK", 217332708.6m, 1528897m, "PLTR", 3.13m },
                    { new Guid("5f2c0ff4-43fe-4a38-ac9c-50393c8594aa"), null, "CERUS CORP", "157085101", new DateOnly(2026, 4, 15), null, "ARKK", 20253561.99m, 10076399m, "CERS", 0.3m },
                    { new Guid("6088d77d-13ed-431f-a3f1-d63f159a8a01"), null, "META PLATFORMS INC-CLASS A", "30303M102", new DateOnly(2026, 4, 16), null, "ARKK", 25978729.14m, 38683m, "META", 0.37m },
                    { new Guid("61bff91c-b981-44a6-83c7-9a04df9746a0"), null, "10X GENOMICS INC-CLASS A", "88025U109", new DateOnly(2026, 4, 15), null, "ARKK", 175880682.4m, 7091963m, "TXG", 2.63m },
                    { new Guid("62745e98-7269-48c5-bc21-50d2c65fabf4"), null, "ROBLOX CORP -CLASS A", "771049103", new DateOnly(2026, 4, 16), null, "ARKK", 225863361.7m, 3777611m, "RBLX", 3.25m },
                    { new Guid("62eb5f3f-1cb3-454d-899b-1f3347510ea0"), null, "ALPHABET INC-CL C", "02079K107", new DateOnly(2026, 4, 16), null, "ARKK", 59123258.49m, 176767m, "GOOG", 0.85m },
                    { new Guid("71ac8de7-0ce3-440d-b2cd-c6d391facdcc"), null, "DEERE & CO", "244199105", new DateOnly(2026, 4, 16), null, "ARKK", 80832241.92m, 140178m, "DE", 1.16m },
                    { new Guid("76307d70-1af7-45ac-97fc-7d79002a7870"), null, "COREWEAVE INC-CL A", "21873S108", new DateOnly(2026, 4, 15), null, "ARKK", 187053895.6m, 1596023m, "CRWV", 2.8m },
                    { new Guid("7b1c662f-c47e-47a3-8be5-200839472f03"), null, "NATERA INC", "632307104", new DateOnly(2026, 4, 15), null, "ARKK", 82842239.76m, 385959m, "NTRA", 1.24m },
                    { new Guid("7b55da78-9fc2-4501-949c-0949b3835440"), null, "CERUS CORP", "157085101", new DateOnly(2026, 4, 16), null, "ARKK", 20399832.54m, 10098927m, "CERS", 0.29m },
                    { new Guid("7c2fb67a-23d7-4f3b-a63d-87fa677a2dc9"), null, "SHOPIFY INC - CLASS A", "82509L107", new DateOnly(2026, 4, 16), null, "ARKK", 312373900m, 2451722m, "SHOP", 4.5m },
                    { new Guid("7c42e6d3-80ce-483f-84f3-9fa39a367074"), null, "ROBLOX CORP -CLASS A", "771049103", new DateOnly(2026, 4, 15), null, "ARKK", 219743602.1m, 3769187m, "RBLX", 3.28m },
                    { new Guid("7f2f14df-6105-4f57-be61-9dfe2e909ccd"), null, "ADVANCED MICRO DEVICES", "7903107", new DateOnly(2026, 4, 16), null, "ARKK", 298464414.1m, 1156301m, "AMD", 4.3m },
                    { new Guid("7f91c0a3-61ed-4bed-a664-5a77ba973b81"), null, "OpenAI Group PBC - Series C", "PPOPENAIC", new DateOnly(2026, 4, 16), null, "ARKK", 174999811.6m, 254476m, "", 2.52m },
                    { new Guid("816f1bd2-9e82-4cae-91e2-a836f2cef2a6"), null, "NVIDIA CORP", "67066G104", new DateOnly(2026, 4, 15), null, "ARKK", 79218486.77m, 403127m, "NVDA", 1.18m },
                    { new Guid("8691a819-2406-4552-8fdb-bb53c64985f1"), null, "BRERA HOLDINGS PLC WTS", "BREADUMMY", new DateOnly(2026, 4, 16), null, "ARKK", 2417103.92m, 4316257m, "", 0.03m },
                    { new Guid("8b452585-90cf-4bbe-9850-28654014b99d"), null, "TAIWAN SEMICONDUCTOR-SP ADR", "874039100", new DateOnly(2026, 4, 16), null, "ARKK", 93184967.7m, 248427m, "TSM", 1.34m },
                    { new Guid("8bcab2e9-57b6-49a9-aa67-ed75d1a8a5f6"), null, "BWX TECHNOLOGIES INC", "05605H100", new DateOnly(2026, 4, 16), null, "ARKK", 75026720.86m, 314683m, "BWXT", 1.08m },
                    { new Guid("8c364f18-4f19-479c-80bc-d99eda640bca"), null, "ADVANCED MICRO DEVICES", "7903107", new DateOnly(2026, 4, 15), null, "ARKK", 294280635.8m, 1153725m, "AMD", 4.4m },
                    { new Guid("8df70514-3382-4ff2-953e-ff6beef7b617"), null, "ROKU INC", "77543R102", new DateOnly(2026, 4, 16), null, "ARKK", 275717031.1m, 2521879m, "ROKU", 3.97m },
                    { new Guid("8e26f2d1-fb1e-44d4-91a5-89cca0dd1d8b"), null, "GENEDX HOLDINGS CORP", "81663L200", new DateOnly(2026, 4, 16), null, "ARKK", 58724334.76m, 870506m, "WGS", 0.85m },
                    { new Guid("900edb95-0b56-47ae-8b53-bdd5066fde68"), null, "RECURSION PHARMACEUTICALS-A", "75629V104", new DateOnly(2026, 4, 16), null, "ARKK", 85358508.75m, 22762269m, "RXRX", 1.23m },
                    { new Guid("933eec18-d24a-4393-b17c-455c9b2758cc"), null, "AMAZON.COM INC", "23135106", new DateOnly(2026, 4, 15), null, "ARKK", 149175680m, 599051m, "AMZN", 2.23m },
                    { new Guid("944b74b8-be10-492e-b176-71df3627f423"), null, "FIGMA INC-CL A", "316841105", new DateOnly(2026, 4, 16), null, "ARKK", 37364824.08m, 1837012m, "", 0.54m },
                    { new Guid("954e40fe-60b1-49be-ab07-bc5dfb39eb49"), null, "AMAZON.COM INC", "23135106", new DateOnly(2026, 4, 16), null, "ARKK", 149196169.5m, 600387m, "AMZN", 2.15m },
                    { new Guid("95a97c9c-e237-409b-9387-5873a2c170e1"), null, "TESLA INC", "88160R101", new DateOnly(2026, 4, 16), null, "ARKK", 678786065.1m, 1731818m, "TSLA", 9.77m },
                    { new Guid("966ee5eb-cac2-485f-8582-9b2b67fafcd5"), null, "ALPHABET INC-CL C", "02079K107", new DateOnly(2026, 4, 15), null, "ARKK", 58306047.5m, 176375m, "GOOG", 0.87m },
                    { new Guid("9e3200d3-a35b-45fd-bdd8-3782ff10169d"), null, "BRERA HOLDINGS PLC WTS", "BREADUMMY", new DateOnly(2026, 4, 15), null, "ARKK", 2373941.35m, 4316257m, "", 0.04m },
                    { new Guid("a02defa6-70da-418e-8c8e-c334fb907267"), null, "ALIBABA GROUP HOLDING-SP ADR", "01609W102", new DateOnly(2026, 4, 15), null, "ARKK", 61969879.2m, 471792m, "BABA", 0.93m },
                    { new Guid("a9225ef6-5286-4783-81eb-50f01a8a1550"), null, "TERADYNE INC", "880770102", new DateOnly(2026, 4, 15), null, "ARKK", 140858781.8m, 385376m, "TER", 2.11m },
                    { new Guid("b072f570-ef89-4c97-93d7-f39644ffae12"), null, "GOLDMAN FS TRSY OBLIG INST 468", "X9USDGSFT", new DateOnly(2026, 4, 15), null, "ARKK", 4425986.19m, 4425986m, "", 0.07m },
                    { new Guid("b269d881-afeb-40a0-9e33-a42214d1be95"), null, "META PLATFORMS INC-CLASS A", "30303M102", new DateOnly(2026, 4, 15), null, "ARKK", 25571451.51m, 38599m, "META", 0.38m },
                    { new Guid("b2f436b3-e8d5-496e-a818-d42aa0564e91"), null, "NATERA INC", "632307104", new DateOnly(2026, 4, 16), null, "ARKK", 80145028.61m, 386819m, "NTRA", 1.15m },
                    { new Guid("b54a0bfa-49bb-405f-988d-8c12c26cc4ed"), null, "BEAM THERAPEUTICS INC", "07373V105", new DateOnly(2026, 4, 16), null, "ARKK", 261836189.7m, 8534426m, "BEAM", 3.77m },
                    { new Guid("b9a4167c-e67d-4426-9644-4ba770272991"), null, "OpenAI Group PBC - Series C", "PPOPENAIC", new DateOnly(2026, 4, 15), null, "ARKK", 174999811.6m, 254476m, "", 2.62m },
                    { new Guid("bb30a7a7-d2e0-4fd5-a886-8e2c66fc26fe"), null, "SHOPIFY INC - CLASS A", "82509L107", new DateOnly(2026, 4, 15), null, "ARKK", 287777320.6m, 2446254m, "SHOP", 4.3m },
                    { new Guid("bc17f814-5692-4942-b0de-b5f8b173f049"), null, "BROADCOM INC", "11135F101", new DateOnly(2026, 4, 16), null, "ARKK", 75145115.52m, 189416m, "AVGO", 1.08m },
                    { new Guid("bc9d0d95-39bc-4cd5-a4c8-b8b96c887a31"), null, "BLOCK INC", "852234103", new DateOnly(2026, 4, 15), null, "ARKK", 97545517.65m, 1474611m, "XYZ", 1.46m },
                    { new Guid("bcc7cbf3-d6c8-409d-a3c5-f75163f3c5af"), null, "KRATOS DEFENSE & SECURITY", "50077B207", new DateOnly(2026, 4, 16), null, "ARKK", 62546340.34m, 837749m, "KTOS", 0.9m },
                    { new Guid("bdbe7d0e-a89f-4a3c-b895-258bf7f5d4ea"), null, "COINBASE GLOBAL INC -CLASS A", "19260Q107", new DateOnly(2026, 4, 16), null, "ARKK", 299740908.9m, 1530071m, "COIN", 4.32m },
                    { new Guid("be955f9d-12c9-44aa-b16f-5f5209de9dbe"), null, "BLOCK INC", "852234103", new DateOnly(2026, 4, 16), null, "ARKK", 100453338.8m, 1477907m, "XYZ", 1.45m },
                    { new Guid("bfd116a1-5d22-4909-869b-804f145e8df1"), null, "BEAM THERAPEUTICS INC", "07373V105", new DateOnly(2026, 4, 15), null, "ARKK", 258016317m, 8515390m, "BEAM", 3.86m },
                    { new Guid("c2295709-5773-4989-879e-29726ecedb11"), null, "ALIBABA GROUP HOLDING-SP ADR", "01609W102", new DateOnly(2026, 4, 16), null, "ARKK", 63020648.32m, 472844m, "BABA", 0.91m },
                    { new Guid("c523aa8a-0d79-4134-b608-4de7c17e8b0a"), null, "INTELLIA THERAPEUTICS INC", "45826J105", new DateOnly(2026, 4, 15), null, "ARKK", 139283778.8m, 9423801m, "NTLA", 2.08m },
                    { new Guid("cb0ee9f8-ef36-4968-aa6d-01ac809335c9"), null, "TESLA INC", "88160R101", new DateOnly(2026, 4, 15), null, "ARKK", 629322303.6m, 1727958m, "TSLA", 9.41m },
                    { new Guid("cc33c213-4202-46e1-bbe6-922020b30bd3"), null, "RECURSION PHARMACEUTICALS-A", "75629V104", new DateOnly(2026, 4, 15), null, "ARKK", 80398671.06m, 22711489m, "RXRX", 1.2m },
                    { new Guid("cc64203c-8d34-446a-98b2-7a70c7506901"), null, "TERADYNE INC", "880770102", new DateOnly(2026, 4, 16), null, "ARKK", 140960690.6m, 386236m, "TER", 2.03m },
                    { new Guid("d4aa97e4-6016-42a2-acf7-d63ad5dabae6"), null, "BRERA HOLDINGS PLC-CL B", "G13311116", new DateOnly(2026, 4, 16), null, "ARKK", 5733071.32m, 6666362m, "SLMT", 0.08m },
                    { new Guid("d5ee4bb1-c785-4e5b-bfbd-175134577b21"), null, "TEMPUS AI INC-CL A", "88023B103", new DateOnly(2026, 4, 15), null, "ARKK", 323875629.9m, 6538979m, "TEM", 4.84m },
                    { new Guid("e149a541-ba89-41ff-97a6-0bbf40132ff6"), null, "COREWEAVE INC-CL A", "21873S108", new DateOnly(2026, 4, 16), null, "ARKK", 189855455.8m, 1599591m, "CRWV", 2.73m },
                    { new Guid("ed3e4a3c-644b-452e-8642-058b82ba2240"), null, "PACIFIC BIOSCIENCES OF CALIF", "69404D108", new DateOnly(2026, 4, 15), null, "ARKK", 33098984.92m, 21082156m, "PACB", 0.49m },
                    { new Guid("eda5b868-abbc-4168-ba00-14b9d67b543f"), null, "ROKU INC", "77543R102", new DateOnly(2026, 4, 15), null, "ARKK", 268081807.7m, 2516255m, "ROKU", 4.01m },
                    { new Guid("f485d50a-762e-4d2b-98d1-10dd36a3c5d9"), null, "ROBINHOOD MARKETS INC - A", "770700102", new DateOnly(2026, 4, 16), null, "ARKK", 337972147.3m, 3870501m, "HOOD", 4.87m },
                    { new Guid("f4cb92ba-c628-4576-a6a0-30bd5e779bcb"), null, "BAIDU INC - SPON ADR", "56752108", new DateOnly(2026, 4, 16), null, "ARKK", 67157377.3m, 555295m, "BIDU", 0.97m },
                    { new Guid("f5f57c97-de7e-464b-b8c3-145b949edcba"), null, "NVIDIA CORP", "67066G104", new DateOnly(2026, 4, 16), null, "ARKK", 80348849.49m, 404027m, "NVDA", 1.16m },
                    { new Guid("f739e1ca-706f-402a-970f-da7ad06b2c75"), null, "PACIFIC BIOSCIENCES OF CALIF", "69404D108", new DateOnly(2026, 4, 16), null, "ARKK", 33806867.2m, 21129292m, "PACB", 0.49m },
                    { new Guid("fac54e9a-b2a1-47cd-b56e-534444c3e169"), null, "GOLDMAN FS TRSY OBLIG INST 468", "X9USDGSFT", new DateOnly(2026, 4, 16), null, "ARKK", 9419424.62m, 9419425m, "", 0.14m },
                    { new Guid("fb9c4573-e603-43de-b878-519399ca0286"), null, "BITMINE IMMERSION TECHNOLOGI", "09175A206", new DateOnly(2026, 4, 15), null, "ARKK", 137867167.6m, 6418397m, "BMNR", 2.06m },
                    { new Guid("fcfcaadc-680a-4010-ad61-de339bd88ad8"), null, "ARCHER AVIATION INC-A", "03945R102", new DateOnly(2026, 4, 16), null, "ARKK", 111350603.2m, 18374687m, "ACHR", 1.6m }
                });

            migrationBuilder.CreateIndex(
                name: "idx_fund_positions_ticker",
                table: "fund_positions",
                column: "Ticker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fund_positions");
        }
    }
}
