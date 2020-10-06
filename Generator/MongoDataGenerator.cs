using PrisonDatabase.MongoDbModel;
using System;
using System.Collections.Generic;

namespace PrisonDatabase.Generator
{
    public class MongoDataGenerator
    {
        private readonly Random _random = new Random();
        int datasetMultipler = 1;

        public List<Prison> GenerateData(int multipler)
        {
            datasetMultipler = multipler;
            var equipment = GenerateEquipment(datasetMultipler * 600);
            var prisonerActions = GeneratePrisonerActions(datasetMultipler * 66000);
            var prisonsers = GeneratePrisoners(datasetMultipler * 33000, prisonerActions);
            var prisonBlocks = GeneratePrisonBlocks(datasetMultipler * 300, prisonsers);
            var prisons = GeneratePrisons(datasetMultipler * 100, prisonBlocks, equipment);

            return prisons;
        }

        private List<Prison> GeneratePrisons(int count, List<PrisonBlock> prisonBlocks, List<Equipment> equipments)
        {
            var prisonsList = new List<Prison>();
            for (int i = 0; i < count; i++)
            {
                var selectedBlocks = prisonBlocks.FindAll(x => x.Id < (i + 1) * 3 * datasetMultipler && x.Id >= i * 3 * datasetMultipler);
                var selectedEquipment = equipments.FindAll(x => x.Id < (i + 1) * 6 * datasetMultipler && x.Id >= i * 6 * datasetMultipler);
                var prison = new Prison
                {
                    Id = i,
                    Name = "Prison " + DataGeneratorBaseStrings.firstNames[_random.Next(0, DataGeneratorBaseStrings.firstNames.Count - 1)],
                    City = DataGeneratorBaseStrings.cities[_random.Next(0, DataGeneratorBaseStrings.cities.Count - 1)].ToString(),
                    Address = DataGeneratorBaseStrings.streets[_random.Next(0, DataGeneratorBaseStrings.streets.Count - 1)].ToString(),
                    PrisonBlocks = selectedBlocks,
                    Equipments = selectedEquipment
                };
                prisonsList.Add(prison);
            }
            return prisonsList;
        }

        private List<Equipment> GenerateEquipment(int count)
        {
            var equipmentList = new List<Equipment>();
            for (int i = 0; i < count; i++)
            {
                var equipment = new Equipment
                {
                    Id = i,
                    FreeSlots = _random.Next(0, 40),
                    Name = DataGeneratorBaseStrings.equipmentNames[_random.Next(0, DataGeneratorBaseStrings.equipmentNames.Count - 1)].ToString()
                };
                equipmentList.Add(equipment);
            }
            return equipmentList;
        }

        private List<PrisonBlock> GeneratePrisonBlocks(int count, List<Prisoner> prisonersList)
        {
            var prisonBlockList = new List<PrisonBlock>();

            for (int i = 0; i < count; i++)
            {
                var selectedPrisoners = prisonersList.FindAll(x => x.Id < (i + 1) * 33 * datasetMultipler && x.Id >= i * 33 * datasetMultipler);
                var prisonBlock = new PrisonBlock
                {
                    Id = i,
                    Name = "Block " + DataGeneratorBaseStrings.surnames[_random.Next(0, DataGeneratorBaseStrings.surnames.Count - 1)],
                    Prisoners = selectedPrisoners
                };
                prisonBlockList.Add(prisonBlock);
            }
            return prisonBlockList;
        }

        private List<Prisoner> GeneratePrisoners(int count, List<PrisonerAction> prisonerActions)
        {
            var prisonersList = new List<Prisoner>();
            for (int i = 0; i < count; i++)
            {
                var selectedPrisionerActions = prisonerActions.FindAll(x => x.Id < (i + 1) * 2 * datasetMultipler && x.Id >= i * 2 * datasetMultipler);
                var prisoner = new Prisoner
                {
                    Id = i,
                    FirstName = DataGeneratorBaseStrings.firstNames[_random.Next(0, DataGeneratorBaseStrings.firstNames.Count - 1)].ToString(),
                    Surname = DataGeneratorBaseStrings.surnames[_random.Next(0, DataGeneratorBaseStrings.surnames.Count - 1)].ToString(),
                    CrimeType = (Utils.CrimeType)_random.Next(0, 5),
                    PrisonerActions = selectedPrisionerActions
                };
                prisonersList.Add(prisoner);
            }
            return prisonersList;
        }

        private List<PrisonerAction> GeneratePrisonerActions(int count)
        {
            var prisonerActionsList = new List<PrisonerAction>();
            for (int i = 0; i < count; i++)
            {
                var prisonerAction = new PrisonerAction
                {
                    Id = i,
                    Description = DataGeneratorBaseStrings.prisonerActions[_random.Next(0, DataGeneratorBaseStrings.prisonerActions.Count - 1)].ToString(),
                    Value = _random.NextDouble() >= 0.5
                };
                prisonerActionsList.Add(prisonerAction);
            }
            return prisonerActionsList;
        }
    }
}
