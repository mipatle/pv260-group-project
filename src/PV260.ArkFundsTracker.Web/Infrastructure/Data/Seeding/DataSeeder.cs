using PV260.ArkFundsTracker.Web.Slices.FundPosition;

namespace PV260.ArkFundsTracker.Web.Infrastructure.Data.Seeding;

public static class DataSeeder
{
    public static List<FundPosition> GetInitialPositions()
    {
        return
        [
            new FundPosition
            {
                Id = Guid.Parse("00ba0e09-9fa6-41d1-8b63-1151ffae91f6"), Date = new DateOnly(2026, 4, 15),
                Ticker = "HOOD", Fund = "ARKK", Company = "ROBINHOOD MARKETS INC - A", Cusip = "770700102",
                Shares = 3861869, MarketValue = 305435219.2m, WeightPercentage = 4.57m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("02c6c154-91e7-432b-bfa3-2e11cd096471"), Date = new DateOnly(2026, 4, 15),
                Ticker = "TSM", Fund = "ARKK", Company = "TAIWAN SEMICONDUCTOR-SP ADR", Cusip = "874039100",
                Shares = 247875, MarketValue = 94165233.75m, WeightPercentage = 1.41m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("0c50e251-bb9e-447f-b657-7fa696fe74bf"), Date = new DateOnly(2026, 4, 15),
                Ticker = "CRCL", Fund = "ARKK", Company = "CIRCLE INTERNET GROUP INC", Cusip = "172573107",
                Shares = 2864001, MarketValue = 302123465.5m, WeightPercentage = 4.52m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("0d4ed431-525c-4559-a8ae-2414e1959568"), Date = new DateOnly(2026, 4, 15),
                Ticker = "KTOS", Fund = "ARKK", Company = "KRATOS DEFENSE & SECURITY", Cusip = "50077B207",
                Shares = 835881, MarketValue = 61570994.46m, WeightPercentage = 0.92m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("15f402ee-3277-4a78-8947-d9e53ed22fbf"), Date = new DateOnly(2026, 4, 15),
                Ticker = "CRSP", Fund = "ARKK", Company = "CRISPR THERAPEUTICS AG", Cusip = "H17182108",
                Shares = 7895598, MarketValue = 448943702.3m, WeightPercentage = 6.71m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("186f8e3d-d0c7-4d3b-9410-9f346324f150"), Date = new DateOnly(2026, 4, 15),
                Ticker = "SLMT", Fund = "ARKK", Company = "BRERA HOLDINGS PLC-CL B", Cusip = "G13311116",
                Shares = 6688790, MarketValue = 5618583.6m, WeightPercentage = 0.08m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("19b8e63b-c8e4-418e-9493-b8670c681ff2"), Date = new DateOnly(2026, 4, 15),
                Ticker = "ACHR", Fund = "ARKK", Company = "ARCHER AVIATION INC-A", Cusip = "03945R102",
                Shares = 18333695, MarketValue = 103952050.7m, WeightPercentage = 1.55m, AdminId = null,
                DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("1b78ca9d-1d76-4faa-a230-2312f6a4094a"), Date = new DateOnly(2026, 4, 15),
                Ticker = "AVGO", Fund = "ARKK", Company = "BROADCOM INC", Cusip = "11135F101", Shares = 188996,
                MarketValue = 71965896.88m, WeightPercentage = 1.08m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("1fe60f8c-26db-4f05-a189-e5fc07ac6ff4"), Date = new DateOnly(2026, 4, 15),
                Ticker = "ILMN", Fund = "ARKK", Company = "ILLUMINA INC", Cusip = "452327109", Shares = 641850,
                MarketValue = 83645892m, WeightPercentage = 1.25m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("2064cf21-c0e0-48b8-8320-83e6fc857856"), Date = new DateOnly(2026, 4, 15),
                Ticker = "TWST", Fund = "ARKK", Company = "TWIST BIOSCIENCE CORP", Cusip = "90184D100",
                Shares = 3534739, MarketValue = 202929366m, WeightPercentage = 3.03m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("2f19cbb7-1ec8-4a94-8892-5ccadf2a1677"), Date = new DateOnly(2026, 4, 15),
                Ticker = "COIN", Fund = "ARKK", Company = "COINBASE GLOBAL INC -CLASS A", Cusip = "19260Q107",
                Shares = 1526659, MarketValue = 281531186.2m, WeightPercentage = 4.21m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("30f446a0-e44f-406f-a53d-3bee85c3c752"), Date = new DateOnly(2026, 4, 15),
                Ticker = "BLSH", Fund = "ARKK", Company = "BULLISH", Cusip = "G16910120", Shares = 4170058,
                MarketValue = 169387756m, WeightPercentage = 2.53m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("310dc826-a1d1-4990-8c73-7de6f05bc334"), Date = new DateOnly(2026, 4, 15),
                Ticker = "VCYT", Fund = "ARKK", Company = "VERACYTE INC", Cusip = "92337F107", Shares = 1839298,
                MarketValue = 63087921.4m, WeightPercentage = 0.94m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("32de7ab3-ecc2-40cc-b560-88561b37a2bc"), Date = new DateOnly(2026, 4, 15),
                Ticker = "DE", Fund = "ARKK", Company = "DEERE & CO", Cusip = "244199105", Shares = 139866,
                MarketValue = 83365730.64m, WeightPercentage = 1.25m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("35538618-53d5-405f-bd4c-1bb26a5f6ae7"), Date = new DateOnly(2026, 4, 15),
                Ticker = "BWXT", Fund = "ARKK", Company = "BWX TECHNOLOGIES INC", Cusip = "05605H100", Shares = 313983,
                MarketValue = 74812729.41m, WeightPercentage = 1.12m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("37044bae-49b2-48f8-ae22-50b5e44bb7c8"), Date = new DateOnly(2026, 4, 15),
                Ticker = "SOFI", Fund = "ARKK", Company = "SOFI TECHNOLOGIES INC", Cusip = "83406F102",
                Shares = 2274344, MarketValue = 40733501.04m, WeightPercentage = 0.61m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("531586c8-2fd3-4178-b7ac-e53f91cb9e99"), Date = new DateOnly(2026, 4, 15),
                Ticker = "WGS", Fund = "ARKK", Company = "GENEDX HOLDINGS CORP", Cusip = "81663L200", Shares = 868566,
                MarketValue = 58072322.76m, WeightPercentage = 0.87m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("541c9aae-1f39-47a3-9c34-6aa089de56f9"), Date = new DateOnly(2026, 4, 15), Ticker = "",
                Fund = "ARKK", Company = "FIGMA INC-CL A", Cusip = "316841105", Shares = 1832916,
                MarketValue = 33762312.72m, WeightPercentage = 0.5m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("5a0cc849-256f-4aac-8201-26cd06755fe9"), Date = new DateOnly(2026, 4, 15),
                Ticker = "BIDU", Fund = "ARKK", Company = "BAIDU INC - SPON ADR", Cusip = "56752108", Shares = 554059,
                MarketValue = 65506395.57m, WeightPercentage = 0.98m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("5a7e4695-80fb-4554-9350-93753b2fcf6a"), Date = new DateOnly(2026, 4, 15),
                Ticker = "PLTR", Fund = "ARKK", Company = "PALANTIR TECHNOLOGIES INC-A", Cusip = "69608A108",
                Shares = 1525489, MarketValue = 207008857.3m, WeightPercentage = 3.09m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("5f2c0ff4-43fe-4a38-ac9c-50393c8594aa"), Date = new DateOnly(2026, 4, 15),
                Ticker = "CERS", Fund = "ARKK", Company = "CERUS CORP", Cusip = "157085101", Shares = 10076399,
                MarketValue = 20253561.99m, WeightPercentage = 0.3m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("61bff91c-b981-44a6-83c7-9a04df9746a0"), Date = new DateOnly(2026, 4, 15),
                Ticker = "TXG", Fund = "ARKK", Company = "10X GENOMICS INC-CLASS A", Cusip = "88025U109",
                Shares = 7091963, MarketValue = 175880682.4m, WeightPercentage = 2.63m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("76307d70-1af7-45ac-97fc-7d79002a7870"), Date = new DateOnly(2026, 4, 15),
                Ticker = "CRWV", Fund = "ARKK", Company = "COREWEAVE INC-CL A", Cusip = "21873S108", Shares = 1596023,
                MarketValue = 187053895.6m, WeightPercentage = 2.8m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("7b1c662f-c47e-47a3-8be5-200839472f03"), Date = new DateOnly(2026, 4, 15),
                Ticker = "NTRA", Fund = "ARKK", Company = "NATERA INC", Cusip = "632307104", Shares = 385959,
                MarketValue = 82842239.76m, WeightPercentage = 1.24m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("7c42e6d3-80ce-483f-84f3-9fa39a367074"), Date = new DateOnly(2026, 4, 15),
                Ticker = "RBLX", Fund = "ARKK", Company = "ROBLOX CORP -CLASS A", Cusip = "771049103", Shares = 3769187,
                MarketValue = 219743602.1m, WeightPercentage = 3.28m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("816f1bd2-9e82-4cae-91e2-a836f2cef2a6"), Date = new DateOnly(2026, 4, 15),
                Ticker = "NVDA", Fund = "ARKK", Company = "NVIDIA CORP", Cusip = "67066G104", Shares = 403127,
                MarketValue = 79218486.77m, WeightPercentage = 1.18m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("8c364f18-4f19-479c-80bc-d99eda640bca"), Date = new DateOnly(2026, 4, 15),
                Ticker = "AMD", Fund = "ARKK", Company = "ADVANCED MICRO DEVICES", Cusip = "7903107", Shares = 1153725,
                MarketValue = 294280635.8m, WeightPercentage = 4.4m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("933eec18-d24a-4393-b17c-455c9b2758cc"), Date = new DateOnly(2026, 4, 15),
                Ticker = "AMZN", Fund = "ARKK", Company = "AMAZON.COM INC", Cusip = "23135106", Shares = 599051,
                MarketValue = 149175680m, WeightPercentage = 2.23m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("966ee5eb-cac2-485f-8582-9b2b67fafcd5"), Date = new DateOnly(2026, 4, 15),
                Ticker = "GOOG", Fund = "ARKK", Company = "ALPHABET INC-CL C", Cusip = "02079K107", Shares = 176375,
                MarketValue = 58306047.5m, WeightPercentage = 0.87m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("9e3200d3-a35b-45fd-bdd8-3782ff10169d"), Date = new DateOnly(2026, 4, 15), Ticker = "",
                Fund = "ARKK", Company = "BRERA HOLDINGS PLC WTS", Cusip = "BREADUMMY", Shares = 4316257,
                MarketValue = 2373941.35m, WeightPercentage = 0.04m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("a02defa6-70da-418e-8c8e-c334fb907267"), Date = new DateOnly(2026, 4, 15),
                Ticker = "BABA", Fund = "ARKK", Company = "ALIBABA GROUP HOLDING-SP ADR", Cusip = "01609W102",
                Shares = 471792, MarketValue = 61969879.2m, WeightPercentage = 0.93m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("a9225ef6-5286-4783-81eb-50f01a8a1550"), Date = new DateOnly(2026, 4, 15),
                Ticker = "TER", Fund = "ARKK", Company = "TERADYNE INC", Cusip = "880770102", Shares = 385376,
                MarketValue = 140858781.8m, WeightPercentage = 2.11m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("b072f570-ef89-4c97-93d7-f39644ffae12"), Date = new DateOnly(2026, 4, 15), Ticker = "",
                Fund = "ARKK", Company = "GOLDMAN FS TRSY OBLIG INST 468", Cusip = "X9USDGSFT", Shares = 4425986,
                MarketValue = 4425986.19m, WeightPercentage = 0.07m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("b269d881-afeb-40a0-9e33-a42214d1be95"), Date = new DateOnly(2026, 4, 15),
                Ticker = "META", Fund = "ARKK", Company = "META PLATFORMS INC-CLASS A", Cusip = "30303M102",
                Shares = 38599, MarketValue = 25571451.51m, WeightPercentage = 0.38m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("b9a4167c-e67d-4426-9644-4ba770272991"), Date = new DateOnly(2026, 4, 15), Ticker = "",
                Fund = "ARKK", Company = "OpenAI Group PBC - Series C", Cusip = "PPOPENAIC", Shares = 254476,
                MarketValue = 174999811.6m, WeightPercentage = 2.62m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("bb30a7a7-d2e0-4fd5-a886-8e2c66fc26fe"), Date = new DateOnly(2026, 4, 15),
                Ticker = "SHOP", Fund = "ARKK", Company = "SHOPIFY INC - CLASS A", Cusip = "82509L107",
                Shares = 2446254, MarketValue = 287777320.6m, WeightPercentage = 4.3m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("bc9d0d95-39bc-4cd5-a4c8-b8b96c887a31"), Date = new DateOnly(2026, 4, 15),
                Ticker = "XYZ", Fund = "ARKK", Company = "BLOCK INC", Cusip = "852234103", Shares = 1474611,
                MarketValue = 97545517.65m, WeightPercentage = 1.46m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("bfd116a1-5d22-4909-869b-804f145e8df1"), Date = new DateOnly(2026, 4, 15),
                Ticker = "BEAM", Fund = "ARKK", Company = "BEAM THERAPEUTICS INC", Cusip = "07373V105",
                Shares = 8515390, MarketValue = 258016317m, WeightPercentage = 3.86m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("c523aa8a-0d79-4134-b608-4de7c17e8b0a"), Date = new DateOnly(2026, 4, 15),
                Ticker = "NTLA", Fund = "ARKK", Company = "INTELLIA THERAPEUTICS INC", Cusip = "45826J105",
                Shares = 9423801, MarketValue = 139283778.8m, WeightPercentage = 2.08m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("cb0ee9f8-ef36-4968-aa6d-01ac809335c9"), Date = new DateOnly(2026, 4, 15),
                Ticker = "TSLA", Fund = "ARKK", Company = "TESLA INC", Cusip = "88160R101", Shares = 1727958,
                MarketValue = 629322303.6m, WeightPercentage = 9.41m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("cc33c213-4202-46e1-bbe6-922020b30bd3"), Date = new DateOnly(2026, 4, 15),
                Ticker = "RXRX", Fund = "ARKK", Company = "RECURSION PHARMACEUTICALS-A", Cusip = "75629V104",
                Shares = 22711489, MarketValue = 80398671.06m, WeightPercentage = 1.2m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("d5ee4bb1-c785-4e5b-bfbd-175134577b21"), Date = new DateOnly(2026, 4, 15),
                Ticker = "TEM", Fund = "ARKK", Company = "TEMPUS AI INC-CL A", Cusip = "88023B103", Shares = 6538979,
                MarketValue = 323875629.9m, WeightPercentage = 4.84m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("ed3e4a3c-644b-452e-8642-058b82ba2240"), Date = new DateOnly(2026, 4, 15),
                Ticker = "PACB", Fund = "ARKK", Company = "PACIFIC BIOSCIENCES OF CALIF", Cusip = "69404D108",
                Shares = 21082156, MarketValue = 33098984.92m, WeightPercentage = 0.49m, AdminId = null,
                DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("eda5b868-abbc-4168-ba00-14b9d67b543f"), Date = new DateOnly(2026, 4, 15),
                Ticker = "ROKU", Fund = "ARKK", Company = "ROKU INC", Cusip = "77543R102", Shares = 2516255,
                MarketValue = 268081807.7m, WeightPercentage = 4.01m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("fb9c4573-e603-43de-b878-519399ca0286"), Date = new DateOnly(2026, 4, 15),
                Ticker = "BMNR", Fund = "ARKK", Company = "BITMINE IMMERSION TECHNOLOGI", Cusip = "09175A206",
                Shares = 6418397, MarketValue = 137867167.6m, WeightPercentage = 2.06m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("032b1743-3020-4fed-b333-c2b5e9cd4627"), Date = new DateOnly(2026, 4, 16),
                Ticker = "BLSH", Fund = "ARKK", Company = "BULLISH", Cusip = "G16910120", Shares = 4179378,
                MarketValue = 174154681.3m, WeightPercentage = 2.51m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("0da43142-0312-44e6-8460-d03c7082bf1d"), Date = new DateOnly(2026, 4, 16),
                Ticker = "VCYT", Fund = "ARKK", Company = "VERACYTE INC", Cusip = "92337F107", Shares = 1843410,
                MarketValue = 62749676.4m, WeightPercentage = 0.9m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("109512a1-bf9c-4f5f-b78e-1f852f533ca0"), Date = new DateOnly(2026, 4, 16),
                Ticker = "CRCL", Fund = "ARKK", Company = "CIRCLE INTERNET GROUP INC", Cusip = "172573107",
                Shares = 2870401, MarketValue = 302884713.5m, WeightPercentage = 4.36m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("10ac21c0-3629-416a-8727-eb99844ff634"), Date = new DateOnly(2026, 4, 16),
                Ticker = "BMNR", Fund = "ARKK", Company = "BITMINE IMMERSION TECHNOLOGI", Cusip = "09175A206",
                Shares = 6432745, MarketValue = 143707523.3m, WeightPercentage = 2.07m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("11860e0b-5240-4e9b-8946-4534ed055d20"), Date = new DateOnly(2026, 4, 16),
                Ticker = "CRSP", Fund = "ARKK", Company = "CRISPR THERAPEUTICS AG", Cusip = "H17182108",
                Shares = 7913250, MarketValue = 449156070m, WeightPercentage = 6.47m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("1b5c2af5-dd98-4014-a581-64a552e3193f"), Date = new DateOnly(2026, 4, 16),
                Ticker = "SOFI", Fund = "ARKK", Company = "SOFI TECHNOLOGIES INC", Cusip = "83406F102",
                Shares = 2279428, MarketValue = 42830452.12m, WeightPercentage = 0.62m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("213e3361-4596-43e0-a241-fc2f2f505874"), Date = new DateOnly(2026, 4, 16),
                Ticker = "NTLA", Fund = "ARKK", Company = "INTELLIA THERAPEUTICS INC", Cusip = "45826J105",
                Shares = 9444869, MarketValue = 141106342.9m, WeightPercentage = 2.03m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("227bf625-2cc2-4658-8a4f-9746c8d0cd57"), Date = new DateOnly(2026, 4, 16),
                Ticker = "TEM", Fund = "ARKK", Company = "TEMPUS AI INC-CL A", Cusip = "88023B103", Shares = 6553599,
                MarketValue = 369688519.6m, WeightPercentage = 5.32m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("267f04f8-2e9a-4087-8b03-b7942e060c36"), Date = new DateOnly(2026, 4, 16),
                Ticker = "TWST", Fund = "ARKK", Company = "TWIST BIOSCIENCE CORP", Cusip = "90184D100",
                Shares = 3542639, MarketValue = 204126859.2m, WeightPercentage = 2.94m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("32205279-d803-45eb-a088-e57722caf83d"), Date = new DateOnly(2026, 4, 16),
                Ticker = "ILMN", Fund = "ARKK", Company = "ILLUMINA INC", Cusip = "452327109", Shares = 643282,
                MarketValue = 85080477.32m, WeightPercentage = 1.22m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("512b95a1-6dc3-4288-8853-a518a4a9ea03"), Date = new DateOnly(2026, 4, 16),
                Ticker = "TXG", Fund = "ARKK", Company = "10X GENOMICS INC-CLASS A", Cusip = "88025U109",
                Shares = 7107819, MarketValue = 180183211.7m, WeightPercentage = 2.59m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("5bd03ed8-08c0-4251-b649-3db899c149ed"), Date = new DateOnly(2026, 4, 16),
                Ticker = "PLTR", Fund = "ARKK", Company = "PALANTIR TECHNOLOGIES INC-A", Cusip = "69608A108",
                Shares = 1528897, MarketValue = 217332708.6m, WeightPercentage = 3.13m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("6088d77d-13ed-431f-a3f1-d63f159a8a01"), Date = new DateOnly(2026, 4, 16),
                Ticker = "META", Fund = "ARKK", Company = "META PLATFORMS INC-CLASS A", Cusip = "30303M102",
                Shares = 38683, MarketValue = 25978729.14m, WeightPercentage = 0.37m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("62745e98-7269-48c5-bc21-50d2c65fabf4"), Date = new DateOnly(2026, 4, 16),
                Ticker = "RBLX", Fund = "ARKK", Company = "ROBLOX CORP -CLASS A", Cusip = "771049103", Shares = 3777611,
                MarketValue = 225863361.7m, WeightPercentage = 3.25m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("62eb5f3f-1cb3-454d-899b-1f3347510ea0"), Date = new DateOnly(2026, 4, 16),
                Ticker = "GOOG", Fund = "ARKK", Company = "ALPHABET INC-CL C", Cusip = "02079K107", Shares = 176767,
                MarketValue = 59123258.49m, WeightPercentage = 0.85m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("71ac8de7-0ce3-440d-b2cd-c6d391facdcc"), Date = new DateOnly(2026, 4, 16),
                Ticker = "DE", Fund = "ARKK", Company = "DEERE & CO", Cusip = "244199105", Shares = 140178,
                MarketValue = 80832241.92m, WeightPercentage = 1.16m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("7b55da78-9fc2-4501-949c-0949b3835440"), Date = new DateOnly(2026, 4, 16),
                Ticker = "CERS", Fund = "ARKK", Company = "CERUS CORP", Cusip = "157085101", Shares = 10098927,
                MarketValue = 20399832.54m, WeightPercentage = 0.29m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("7c2fb67a-23d7-4f3b-a63d-87fa677a2dc9"), Date = new DateOnly(2026, 4, 16),
                Ticker = "SHOP", Fund = "ARKK", Company = "SHOPIFY INC - CLASS A", Cusip = "82509L107",
                Shares = 2451722, MarketValue = 312373900m, WeightPercentage = 4.5m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("7f2f14df-6105-4f57-be61-9dfe2e909ccd"), Date = new DateOnly(2026, 4, 16),
                Ticker = "AMD", Fund = "ARKK", Company = "ADVANCED MICRO DEVICES", Cusip = "7903107", Shares = 1156301,
                MarketValue = 298464414.1m, WeightPercentage = 4.3m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("7f91c0a3-61ed-4bed-a664-5a77ba973b81"), Date = new DateOnly(2026, 4, 16), Ticker = "",
                Fund = "ARKK", Company = "OpenAI Group PBC - Series C", Cusip = "PPOPENAIC", Shares = 254476,
                MarketValue = 174999811.6m, WeightPercentage = 2.52m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("8691a819-2406-4552-8fdb-bb53c64985f1"), Date = new DateOnly(2026, 4, 16), Ticker = "",
                Fund = "ARKK", Company = "BRERA HOLDINGS PLC WTS", Cusip = "BREADUMMY", Shares = 4316257,
                MarketValue = 2417103.92m, WeightPercentage = 0.03m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("8b452585-90cf-4bbe-9850-28654014b99d"), Date = new DateOnly(2026, 4, 16),
                Ticker = "TSM", Fund = "ARKK", Company = "TAIWAN SEMICONDUCTOR-SP ADR", Cusip = "874039100",
                Shares = 248427, MarketValue = 93184967.7m, WeightPercentage = 1.34m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("8bcab2e9-57b6-49a9-aa67-ed75d1a8a5f6"), Date = new DateOnly(2026, 4, 16),
                Ticker = "BWXT", Fund = "ARKK", Company = "BWX TECHNOLOGIES INC", Cusip = "05605H100", Shares = 314683,
                MarketValue = 75026720.86m, WeightPercentage = 1.08m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("8df70514-3382-4ff2-953e-ff6beef7b617"), Date = new DateOnly(2026, 4, 16),
                Ticker = "ROKU", Fund = "ARKK", Company = "ROKU INC", Cusip = "77543R102", Shares = 2521879,
                MarketValue = 275717031.1m, WeightPercentage = 3.97m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("8e26f2d1-fb1e-44d4-91a5-89cca0dd1d8b"), Date = new DateOnly(2026, 4, 16),
                Ticker = "WGS", Fund = "ARKK", Company = "GENEDX HOLDINGS CORP", Cusip = "81663L200", Shares = 870506,
                MarketValue = 58724334.76m, WeightPercentage = 0.85m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("900edb95-0b56-47ae-8b53-bdd5066fde68"), Date = new DateOnly(2026, 4, 16),
                Ticker = "RXRX", Fund = "ARKK", Company = "RECURSION PHARMACEUTICALS-A", Cusip = "75629V104",
                Shares = 22762269, MarketValue = 85358508.75m, WeightPercentage = 1.23m, AdminId = null,
                DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("944b74b8-be10-492e-b176-71df3627f423"), Date = new DateOnly(2026, 4, 16), Ticker = "",
                Fund = "ARKK", Company = "FIGMA INC-CL A", Cusip = "316841105", Shares = 1837012,
                MarketValue = 37364824.08m, WeightPercentage = 0.54m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("954e40fe-60b1-49be-ab07-bc5dfb39eb49"), Date = new DateOnly(2026, 4, 16),
                Ticker = "AMZN", Fund = "ARKK", Company = "AMAZON.COM INC", Cusip = "23135106", Shares = 600387,
                MarketValue = 149196169.5m, WeightPercentage = 2.15m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("95a97c9c-e237-409b-9387-5873a2c170e1"), Date = new DateOnly(2026, 4, 16),
                Ticker = "TSLA", Fund = "ARKK", Company = "TESLA INC", Cusip = "88160R101", Shares = 1731818,
                MarketValue = 678786065.1m, WeightPercentage = 9.77m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("b2f436b3-e8d5-496e-a818-d42aa0564e91"), Date = new DateOnly(2026, 4, 16),
                Ticker = "NTRA", Fund = "ARKK", Company = "NATERA INC", Cusip = "632307104", Shares = 386819,
                MarketValue = 80145028.61m, WeightPercentage = 1.15m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("b54a0bfa-49bb-405f-988d-8c12c26cc4ed"), Date = new DateOnly(2026, 4, 16),
                Ticker = "BEAM", Fund = "ARKK", Company = "BEAM THERAPEUTICS INC", Cusip = "07373V105",
                Shares = 8534426, MarketValue = 261836189.7m, WeightPercentage = 3.77m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("bc17f814-5692-4942-b0de-b5f8b173f049"), Date = new DateOnly(2026, 4, 16),
                Ticker = "AVGO", Fund = "ARKK", Company = "BROADCOM INC", Cusip = "11135F101", Shares = 189416,
                MarketValue = 75145115.52m, WeightPercentage = 1.08m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("bcc7cbf3-d6c8-409d-a3c5-f75163f3c5af"), Date = new DateOnly(2026, 4, 16),
                Ticker = "KTOS", Fund = "ARKK", Company = "KRATOS DEFENSE & SECURITY", Cusip = "50077B207",
                Shares = 837749, MarketValue = 62546340.34m, WeightPercentage = 0.9m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("bdbe7d0e-a89f-4a3c-b895-258bf7f5d4ea"), Date = new DateOnly(2026, 4, 16),
                Ticker = "COIN", Fund = "ARKK", Company = "COINBASE GLOBAL INC -CLASS A", Cusip = "19260Q107",
                Shares = 1530071, MarketValue = 299740908.9m, WeightPercentage = 4.32m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("be955f9d-12c9-44aa-b16f-5f5209de9dbe"), Date = new DateOnly(2026, 4, 16),
                Ticker = "XYZ", Fund = "ARKK", Company = "BLOCK INC", Cusip = "852234103", Shares = 1477907,
                MarketValue = 100453338.8m, WeightPercentage = 1.45m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("c2295709-5773-4989-879e-29726ecedb11"), Date = new DateOnly(2026, 4, 16),
                Ticker = "BABA", Fund = "ARKK", Company = "ALIBABA GROUP HOLDING-SP ADR", Cusip = "01609W102",
                Shares = 472844, MarketValue = 63020648.32m, WeightPercentage = 0.91m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("cc64203c-8d34-446a-98b2-7a70c7506901"), Date = new DateOnly(2026, 4, 16),
                Ticker = "TER", Fund = "ARKK", Company = "TERADYNE INC", Cusip = "880770102", Shares = 386236,
                MarketValue = 140960690.6m, WeightPercentage = 2.03m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("d4aa97e4-6016-42a2-acf7-d63ad5dabae6"), Date = new DateOnly(2026, 4, 16),
                Ticker = "SLMT", Fund = "ARKK", Company = "BRERA HOLDINGS PLC-CL B", Cusip = "G13311116",
                Shares = 6666362, MarketValue = 5733071.32m, WeightPercentage = 0.08m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("e149a541-ba89-41ff-97a6-0bbf40132ff6"), Date = new DateOnly(2026, 4, 16),
                Ticker = "CRWV", Fund = "ARKK", Company = "COREWEAVE INC-CL A", Cusip = "21873S108", Shares = 1599591,
                MarketValue = 189855455.8m, WeightPercentage = 2.73m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("f485d50a-762e-4d2b-98d1-10dd36a3c5d9"), Date = new DateOnly(2026, 4, 16),
                Ticker = "HOOD", Fund = "ARKK", Company = "ROBINHOOD MARKETS INC - A", Cusip = "770700102",
                Shares = 3870501, MarketValue = 337972147.3m, WeightPercentage = 4.87m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("f4cb92ba-c628-4576-a6a0-30bd5e779bcb"), Date = new DateOnly(2026, 4, 16),
                Ticker = "BIDU", Fund = "ARKK", Company = "BAIDU INC - SPON ADR", Cusip = "56752108", Shares = 555295,
                MarketValue = 67157377.3m, WeightPercentage = 0.97m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("f5f57c97-de7e-464b-b8c3-145b949edcba"), Date = new DateOnly(2026, 4, 16),
                Ticker = "NVDA", Fund = "ARKK", Company = "NVIDIA CORP", Cusip = "67066G104", Shares = 404027,
                MarketValue = 80348849.49m, WeightPercentage = 1.16m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("f739e1ca-706f-402a-970f-da7ad06b2c75"), Date = new DateOnly(2026, 4, 16),
                Ticker = "PACB", Fund = "ARKK", Company = "PACIFIC BIOSCIENCES OF CALIF", Cusip = "69404D108",
                Shares = 21129292, MarketValue = 33806867.2m, WeightPercentage = 0.49m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("fac54e9a-b2a1-47cd-b56e-534444c3e169"), Date = new DateOnly(2026, 4, 16), Ticker = "",
                Fund = "ARKK", Company = "GOLDMAN FS TRSY OBLIG INST 468", Cusip = "X9USDGSFT", Shares = 9419425,
                MarketValue = 9419424.62m, WeightPercentage = 0.14m, AdminId = null, DeletedAt = null
            },

            new FundPosition
            {
                Id = Guid.Parse("fcfcaadc-680a-4010-ad61-de339bd88ad8"), Date = new DateOnly(2026, 4, 16),
                Ticker = "ACHR", Fund = "ARKK", Company = "ARCHER AVIATION INC-A", Cusip = "03945R102",
                Shares = 18374687, MarketValue = 111350603.2m, WeightPercentage = 1.6m, AdminId = null, DeletedAt = null
            }
        ];
    }
}