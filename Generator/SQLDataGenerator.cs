using PrisonDatabase.Model;
using System;
using System.Collections.Generic;

namespace PrisonDatabase.Generator
{
    public class SQLDataGenerator
    {
        private readonly Random _random = new Random();
        int datasetMultipler = 1;
        public List<object> GenerateData(int multipler)
        {
            datasetMultipler = multipler;

            var prisons = GeneratePrisons(datasetMultipler * 1000);
            var equipment = GenerateEquipment(datasetMultipler * 600, prisons);
            var prisonBlocks = GeneratePrisonBlocks(datasetMultipler * 3000, prisons);
            var prisonsers = GeneratePrisoners(datasetMultipler * 33000, prisonBlocks);
            var prisonerActions = GeneratePrisonerActions(datasetMultipler * 66000, prisonsers);

            var data = new List<object>
            {
                prisons,
                equipment,
                prisonBlocks,
                prisonsers,
                prisonerActions
            };
            return data;
        }

        private List<Prison> GeneratePrisons(int count)
        {
            var prisonsList = new List<Prison>();
            for (int i = 0; i < count; i++)
            {
                var prison = new Prison
                {
                    Id = i,
                    Name = "Prison " + DataGeneratorBaseStrings.firstNames[_random.Next(0, DataGeneratorBaseStrings.firstNames.Count - 1)],
                    City = DataGeneratorBaseStrings.cities[_random.Next(0, DataGeneratorBaseStrings.cities.Count - 1)].ToString(),
                    Address = DataGeneratorBaseStrings.streets[_random.Next(0, DataGeneratorBaseStrings.streets.Count - 1)].ToString()
                };
                prisonsList.Add(prison);
            }
            return prisonsList;
        }

        private List<Equipment> GenerateEquipment(int count, List<Prison> prisonsList)
        {
            var equipmentList = new List<Equipment>();
            for (int i = 0; i < count; i++)
            {
                var equipment = new Equipment
                {
                    Id = i,
                    FreeSlots = _random.Next(0, 40),
                    Name = DataGeneratorBaseStrings.equipmentNames[_random.Next(0, DataGeneratorBaseStrings.equipmentNames.Count - 1)].ToString(),
                    Prison = prisonsList[_random.Next(0, prisonsList.Count - 1)]
                };
                equipmentList.Add(equipment);
            }
            return equipmentList;
        }

        private List<PrisonBlock> GeneratePrisonBlocks(int count, List<Prison> prisonsList)
        {
            var prisonBlockList = new List<PrisonBlock>();
            for (int i = 0; i < count; i++)
            {
                var prisonBlock = new PrisonBlock
                {
                    Id = i,
                    Name = "Block " + DataGeneratorBaseStrings.surnames[_random.Next(0, DataGeneratorBaseStrings.surnames.Count - 1)],
                    Prison = prisonsList[_random.Next(0, prisonsList.Count - 1)]
                };
                prisonBlockList.Add(prisonBlock);
            }
            return prisonBlockList;
        }

        private List<Prisoner> GeneratePrisoners(int count, List<PrisonBlock> prisonsBlockList)
        {
            var prisonersList = new List<Prisoner>();
            for (int i = 0; i < count; i++)
            {
                var prisoner = new Prisoner
                {
                    Id = i,
                    FirstName = DataGeneratorBaseStrings.firstNames[_random.Next(0, DataGeneratorBaseStrings.firstNames.Count - 1)].ToString(),
                    Surname = DataGeneratorBaseStrings.surnames[_random.Next(0, DataGeneratorBaseStrings.surnames.Count - 1)].ToString(),
                    CrimeType = (Utils.CrimeType)_random.Next(0, 5),
                    PrisonBlock = prisonsBlockList[_random.Next(0, prisonsBlockList.Count - 1)]
                };
                prisonersList.Add(prisoner);
            }
            return prisonersList;
        }

        private List<PrisonerAction> GeneratePrisonerActions(int count, List<Prisoner> prisionersList)
        {
            var prisonerActionsList = new List<PrisonerAction>();
            for (int i = 0; i < count; i++)
            {
                var prisonerAction = new PrisonerAction
                {
                    Id = i,
                    Description = DataGeneratorBaseStrings.prisonerActions[_random.Next(0,
                    DataGeneratorBaseStrings.prisonerActions.Count - 1)].ToString(),
                    Prisoner = prisionersList[_random.Next(0, prisionersList.Count - 1)],
                    Value = _random.NextDouble() >= 0.5
                };
                prisonerActionsList.Add(prisonerAction);
            }
            return prisonerActionsList;
        }
    }
}
